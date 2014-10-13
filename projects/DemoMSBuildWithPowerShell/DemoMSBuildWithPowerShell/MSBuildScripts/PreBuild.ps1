Set-StrictMode -Version 2
$ErrorActionPreference = "Stop"
$scriptfilename = $MyInvocation.MyCommand.Name
$basename = [System.IO.Path]::GetFileNameWithoutExtension( $scriptfilename )
$datestring = Get-Date -Format "yyyy_MM_dd_hh_mm_ss"
$outpath = "D:\"
$outfilename = Join-Path $outpath ( $basename + $datestring + ".txt")
"TEST $scriptfilename" | Out-File $outfilename