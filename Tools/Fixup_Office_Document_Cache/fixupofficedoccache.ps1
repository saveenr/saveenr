#http://www.7tutorials.com/fix-problems-microsoft-office-document-cache-being-corrupted


$programName = "MSOSYNC"
$isRunning = (Get-Process | Where-Object { $_.Name -eq $programName }).Count -gt 0
if ($isRunning)
{
    Stop-Process -Name MSOSYNC
}


$p = JOin-Path $env:APPDATA "..\Local\Microsoft\Office\15.0"

Resolve-Path $p
$folders = dir $p
$folders = $folders | ? { $_.Name -like "OfficeFileCache*" }

foreach ($f in $folders)
{
    Remove-Item -Recurse -Force $f.FullName
}




