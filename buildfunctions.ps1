# Taken from psake https://github.com/psake/psake

<#
.SYNOPSIS
  This is a helper function that runs a scriptblock and checks the PS variable $lastexitcode
  to see if an error occcured. If an error is detected then an exception is thrown.
  This function allows you to run command-line programs without having to
  explicitly check the $lastexitcode variable.
.EXAMPLE
  exec { svn info $repository_trunk } "Error executing SVN. Please verify SVN command-line client is installed"
#>
function Exec
{
    [CmdletBinding()]
    param(
        [Parameter(Position=0,Mandatory=1)][scriptblock]$cmd,
        [Parameter(Position=1,Mandatory=0)][string]$errorMessage = ($msgs.error_bad_command -f $cmd)
    )
    & $cmd
    if($lastexitcode -ne 0) {
        throw ("Exec: " + $errorMessage)
    }
}

Function Poke-Xml($filePath, $xpath, $value) {
    [xml] $fileXml = Get-Content $filePath
    $node = $fileXml.SelectSingleNode($xpath)
    
    if ($node.NodeType -eq "Element") {
        $node.InnerText = $value
    }
    else {
        $node.Value = $value
    }

    $fileXml.Save($filePath) 
}

Function Init {
    Write-Host "deleting everything from $build_dir" -ForegroundColor Red
    Remove-Item -Recurse -Force $build_dir -ErrorAction Ignore
    mkdir $build_dir > $null

    exec {
        & dotnet clean $source_dir\$projectName.sln -nologo -v $verbosity
    }
    exec {
        & dotnet restore $source_dir\$projectName.sln -nologo --interactive -v $verbosity
    }

    Write-Output $projectConfig
    Write-Output $version
}


Function Compile{
    Write-Host "Compiling..." -ForegroundColor Blue
    exec {
        & dotnet build $source_dir\$projectName.sln -nologo --no-restore -v `
			$verbosity -maxcpucount --configuration $projectConfig --no-incremental `
			/p:TreatWarningsAsErrors="true" `
			/p:Version=$version /p:Authors="Programming with Palermo" `
			/p:Product="Church Bulletin"
    }
}

Function UnitTests{
    Write-Host "Running unit tests from:  $unitTestProjectPath" -ForegroundColor Blue
    Push-Location -Path $unitTestProjectPath

    try {
        exec {
            & dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=cobertura -nologo -v $verbosity --logger:trx `
			--results-directory $test_dir --no-build `
			--no-restore --configuration $projectConfig `
			--collect:"Code Coverage"
        }
    }
    finally {
        Pop-Location
    }
}

Function IntegrationTest{
    Push-Location -Path $integrationTestProjectPath

    try {
        exec {
            & dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=cobertura -nologo -v $verbosity --logger:trx `
			--results-directory $test_dir --no-build `
			--no-restore --configuration $projectConfig `
			--collect:"Code Coverage"
        }
    }
    finally {
        Pop-Location
    }
}

Function AcceptanceTest{
    $serverProcess = Start-Process dotnet.exe "run --project $source_dir\UI\Server\UI.Server.csproj --configuration $projectConfig -nologo --no-restore --no-build -v $verbosity" -PassThru
    Start-Sleep 1 #let the server process spin up for 1 second

    Push-Location -Path $acceptanceTestProjectPath

    try {
        exec {
            & dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=cobertura -nologo -v $verbosity --logger:trx `
			--results-directory $test_dir --no-build `
			--no-restore --configuration $projectConfig `
			--collect:"Code Coverage"
        }
    }
    finally {
        Pop-Location
        Stop-Process -id $serverProcess.Id
    }
}

Function MigrateDatabaseLocal {
    exec{
        & $aliaSql $databaseAction $script:databaseServer $databaseName $databaseScripts
    }
}

Function PackageUI {
    exec{
        & dotnet publish $uiProjectPath -nologo --no-restore --no-build -v $verbosity --configuration $projectConfig
    }
    exec{
        & dotnet-octo pack --id "$projectName.UI" --version $version --basePath $uiProjectPath\bin\$projectConfig\$framework\publish --outFolder $build_dir --overwrite
    }
}

Function PackageDatabase {
    exec{
        & dotnet-octo pack --id "$projectName.Database" --version $version --basePath $databaseProjectPath --outFolder $build_dir --overwrite
    }
}

Function PackageAcceptanceTests {
    # Use Debug configuration so full symbols are available to display better error messages in test failures
    exec{
        & dotnet publish $acceptanceTestProjectPath -nologo --no-restore -v $verbosity --configuration Debug
    }
    exec{
        & dotnet-octo pack --id "$projectName.AcceptanceTests" --version $version --basePath $acceptanceTestProjectPath\bin\Debug\$framework\publish --outFolder $build_dir --overwrite
    }
}

Function Package{
    Write-Output "Packaging nuget packages"
    dotnet tool install --global Octopus.DotNet.Cli | Write-Output $_ -ErrorAction SilentlyContinue #prevents red color is already installed
    PackageUI
    PackageDatabase
    PackageAcceptanceTests
}

Function PrivateBuild{
    $projectConfig = "Debug"
    $sw = [Diagnostics.Stopwatch]::StartNew()
    Init
    Compile
    UnitTests
    MigrateDatabaseLocal
    #IntegrationTest
    #AcceptanceTest
    $sw.Stop()
    write-host "Build time: " $sw.Elapsed.ToString()
}

Function CIBuild{
    $sw = [Diagnostics.Stopwatch]::StartNew()
    Init
    Compile
    UnitTests
    MigrateDatabaseLocal
    IntegrationTest
    Package
    $sw.Stop()
    write-host "Build time: " $sw.Elapsed.ToString()
}
