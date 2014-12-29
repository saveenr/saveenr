Set-StrictMode -Version 2
$ErrorActionPreference = "Stop"

$path = "D:\music"

$files = dir $path -File -Recurse -Filter "*.mp3"



$script_path = $myinvocation.mycommand.path
$script_folder = Split-Path $script_path -Parent
$taglib = Join-Path $script_folder "taglibsharp/taglib-sharp.dll"

$taglib_asm = [system.reflection.assembly]::loadfile($taglib)


$i = 0

$fp = [System.IO.File]::CreateText("d:\catalog3.htm")
$fp.AutoFlush = $true


$fp.WriteLine("<html>")
$fp.WriteLine("<body>")
$fp.WriteLine("<table>")

$fp.WriteLine("<tr>")


$fp.WriteLine("<th>" + "Link"+ "</th>")
$fp.WriteLine("<th>" + "Title"+ "</th>")
$fp.WriteLine("<th>" + "Artists" + "</th>")
$fp.WriteLine("<th>" + "Album" + "</th>")
$fp.WriteLine("<th>" + "Year" + "</th>")
$fp.WriteLine("<th>" + "Bitrate" + "</th>")
$fp.WriteLine("<th>" + "Duration" + "</th>")
$fp.WriteLine("<th>" + "CreateTime" + "</th>")
$fp.WriteLine("<th>" + "ModifiedTime" + "</th>")
$fp.WriteLine("<th>" + "Length" + "</th>")
$fp.WriteLine("</tr>" )

foreach ($file in $files)
{
    $fp.WriteLine("<tr>")
   
    $m = $i % 100
    Write-Host "." -NoNewline
    if ($m -eq 0)
    {
        Write-Host $i
    }

    $media = [taglib.file]::create($file.FullName)

    $title = $media.Tag.Title

    $fn = $file.FullName

    $fp.WriteLine( "<td><a href=`"$fn`"> " + "link" + "</a></td>"   )
    $fp.WriteLine(  "<td>" + $title + "</td>"  )
    $fp.WriteLine(   "<td>" + $media.Tag.JoinedArtists + "</td>"  )
    $fp.WriteLine(   "<td>" + $media.Tag.Album + "</td>" )
    $fp.WriteLine( "<td>" + $media.Tag.Year + "</td>"   )
    $fp.WriteLine( "<td>" + $media.Properties.AudioBitrate + "</td>"   )
    $fp.WriteLine(  "<td>" + $media.Properties.Duration.Seconds + "</td>"  )
    $fp.WriteLine(  "<td>" + $file.CreationTime + "</td>"  )
    $fp.WriteLine(  "<td>" + $file.LastWriteTime + "</td>"  )
    $fp.WriteLine(  "<td>" + $file.Length + "</td>"  )

    $i=$i+1
    $fp.WriteLine("</tr>")
}

$fp.WriteLine("</table>")
$fp.WriteLine("</body>")
$fp.WriteLine("</html>")
$fp.Close()


