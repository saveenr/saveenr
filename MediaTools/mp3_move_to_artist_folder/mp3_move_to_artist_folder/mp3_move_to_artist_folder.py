# fill find all MP3s at root of folder and then put them in subfolders - each subfolder will be the name of the artist

import os
import sys
import clr
import System
clr.AddReferenceToFileAndPath(r"taglibsharp\taglib-sharp.dll")
import TagLib

folder = r"X:\music"
files= System.IO.Directory.GetFiles( folder, "*mp3" )
        
def fixname(s) :
    s = s.replace("\"","-")
    return s

for f in files :
    print f
    with TagLib.File.Create(f) as tagfile:
        artists = tagfile.Tag.AlbumArtists
        if (len(artists)<1) : continue
        artist=artists[0]
        print artist
        artist_folder = System.IO.Path.Combine(folder, fixname(artist) )
        if (not System.IO.Directory.Exists(artist_folder)) :
            System.IO.Directory.CreateDirectory(artist_folder)
        dest_filename = System.IO.Path.Combine(artist_folder, System.IO.Path.GetFileName(f))
        print dest_filename
        System.IO.File.Move( f, dest_filename )
        


