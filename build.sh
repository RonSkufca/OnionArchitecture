#!/bin/bash
echo Calling powershell to run privatebuild.ps1
pwsh -NoProfile -ExecutionPolicy Bypass -Command "& { .\privatebuild.ps1; if ($lastexitcode -ne 0) {write-host "ERROR: $lastexitcode" -fore RED; exit $lastexitcode} }" 
