$iisExpressExe = '"c:\Program Files (x86)\IIS Express\iisexpress.exe"'
$path = (Resolve-path "D:\github\saveenr\MarkDownHandler\website")
Write-Host $path
Write-host "Starting site on port: $port"
$params = " /path:$path "
$command = "$iisExpressExe $params"
cmd /c start cmd /k "$command"
Start-Sleep -m 1000
Write-Host "Site started"