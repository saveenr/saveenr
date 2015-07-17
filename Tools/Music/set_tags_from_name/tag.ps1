$ScriptPath = $MyInvocation.MyCommand.Path
$ScriptDir  = Split-Path -Parent $ScriptPath


$ScriptDir

$musicfolder = "E:\songs"

$files = Get-ChildItem -Path $musicfolder -Filter *.mp3 -File

$taglibsharp = Join-Path $ScriptDir "taglibsharp/taglib-sharp.dll"

$asm = [Reflection.Assembly]::LoadFrom( $taglibsharp )

foreach ($file in $files)
{
    $basename = $file.BaseName
    $basename = [System.IO.Path]::GetFileNameWithoutExtension($basename)
    $basename = $basename.Trim()

    $tokens = $basename.Split("-")
    if ($tokens.Length -ne 2)
    {
        #skip
    }
    else
    {
        $left = $tokens[0].Trim()
        $right = $tokens[1].Trim()

        $artist = $left
        $title = $right
        $title = $title.Replace( "(Official)", "")
        $title = $title.Replace( "(Official Video)", "")
        $title = $title.Replace( "[Official Video]", "")
        $title = $title.Replace( "[Official Music Video]", "")
        $title = $title.Replace( "(Official Music Video)", "")
        $title = $title.Replace( "[Official Audio]", "")
        $title = $title.Replace( "(Official Lyric Video)", "")
        $title = $title.Replace( "(Official Lyrics Video)", "")
        $title = $title.Replace( "[Official Lyrics Video]", "")
        $title = $title.Replace( "(Lyric)", "")
        $title = $title.Replace( "(Lyrics)", "")
        $title = $title.Replace( "(Lyric Video)", "")
        $title = $title.Replace( "  ", " ")
        $title = $title.Replace( "  ", " ")
        $title = $title.Replace( "  ", " ")
        $title = $title.Replace( "  ", " ")

        Write-Host $artist-> $title

        $media = [TagLib.File]::Create( $file.FullName )

        $media.Tag.Title = $title
        $media.Tag.AlbumArtists = $artist
        $media.Tag.Performers = $artist
         
        $media.Save() 

    }
}