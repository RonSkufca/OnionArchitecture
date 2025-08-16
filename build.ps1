. .\buildfunctions.ps1
$DebugPreference = "Continue"
$startTime = Get-Date -Format "yyyy-MM-dd HH:mm:ss"  # For date and time
$projectName = "ChurchBulletin"
$base_dir = resolve-path .\
$source_dir = "$base_dir/src"
$unitTestProjectPath = "$source_dir/UnitTests"
$integrationTestProjectPath = "$source_dir/IntegrationTests"
$acceptanceTestProjectPath = "$source_dir/AcceptanceTests"
$uiProjectPath = "$source_dir/UI/Server"
$databaseProjectPath = "$source_dir/Database"
$projectConfig = $env:BuildConfiguration
$framework = "net9.0"
$version = $env:Version
$verbosity = "m"

$build_dir = "$base_dir/build"
$test_dir = "$build_dir/test"

$aliaSql = "$source_dir\Database\scripts\AliaSql.exe"
$databaseAction = $env:DatabaseAction

if ([string]::IsNullOrEmpty($databaseAction)) { $databaseAction = "Rebuild"}
    $databaseName = $env:DatabaseName

if ([string]::IsNullOrEmpty($databaseName)) { $databaseName = $projectName}
    $script:databaseServer = $env:DatabaseServer

if ([string]::IsNullOrEmpty($script:databaseServer)) { $script:databaseServer = "(LocalDb)\MSSQLLocalDB"}
    $databaseScripts = "$source_dir\Database\scripts"

if ([string]::IsNullOrEmpty($version)) { $version = "1.0.0.0"}
if ([string]::IsNullOrEmpty($projectConfig)) {$projectConfig = "Release"}

Write-Host "starting at: $startTime" -ForegroundColor Green
Write-Host "base_dir: $base_dir" -ForegroundColor Green
Write-Host "source_dir: $source_dir" -ForegroundColor Green
Write-Host "build_dir: $build_dir" -ForegroundColor Green
Write-Host "test_dir: $test_dir" -ForegroundColor Green