# Unit Test for CodePackage

Set-StrictMode -Version 2
$ErrorActionPreference = "Stop"

Import-Module ./CodePackage.psm1

$results = Export-PowerShellModuleInstaller `
    -InputFolder (Resolve-Path (Join-Path $MyInvocation.MyCommand.Path "../TestInput/MyPSModule")) `
    -OutputFolder (join-Path $env:USERPROFILE Documents)`
    -WIXBinFolder (Resolve-Path( join-path $MyInvocation.MyCommand.Path "../../wix36-binaries")) `
	-InstallType "PowerShellUserModule" `
    -ProductNameLong "MyPSModule Powershell Module" `
    -ProductNameShort "MyPSModule" `
    -ProductVersion ("1.0.0." + (Get-Date -format yyyyMMdd)) `
    -Manufacturer "ManufacterName" `
    -HelpLink "http://viziblr.codeplex.com/helplink" `
    -AboutLink  "http://viziblr.codeplex.com/avoutlink" `
    -ProductID "BEFBF8EA-0172-4B1A-BD40-CD2D7E20B607" `
    -UpgradeCode  "6236AE8E-FAC4-491F-B3CF-7E78A29AB214" `
    -UpgradeID  "68F23FA9-9C4D-429C-9549-26125A6E7F17" `
	-ProgramFilesSubFolder $null `
	-Verbose

Write-Host $results