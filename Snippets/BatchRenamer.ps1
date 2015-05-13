Set-StrictMode -Version 2
$ErrorActionPreference = "Stop"


function GetNewName( $item )
{
    $basename = $item.BaseName
    $ext = $item.Extension

    write-host $basename
    $basename = $basename.Replace("--", "-")
    $basename = $basename.Replace("--", "-")
    $basename = $basename.Replace("--", "-")

    $tokens = $basename.Split("-")
    $newname = $tokens[1].Trim() + " - " + $tokens[0].Trim() 
    $newname + $ext
}


$items = dir "D:\music\Artist_Title" -File

foreach ($item in $items)
{
    $path = $item.DirectoryName
    $newname = GetNewName $item

    Write-Host "-----"
    Write-Host "FROMx: " $item.Name
    Write-Host "TO:   " $newname
    $keyinfo = [System.Console]::ReadKey()
    $key = $keyinfo.Key
    if ( ($key -eq "q") -or ($key -eq "Q"))
    {
        Write-Host "Stopping"
        break
    }
    if ( ($key -eq "r") -or ($key -eq "R"))
    {

        Write-Host "Renaming"
        Rename-Item $item.FullName -NewName (Join-Path $path $newname)
    } 
    else
    {
        Write-Host "Skipping"
    }
}