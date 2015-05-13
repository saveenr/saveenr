from __future__ import nested_scopes

import os
import sys
import zipfile
from commonpy import libfs


def having_extension( fname, list ) :
    return( libfs.get_ext( fname ) in list )

files = libfs.listdir_files( "c:\\" , 1)
files = map( os.path.normcase, files )
files = filter( lambda x : having_extension( x, [ ".jpg" ] ), files )

fp = zipfile.ZipFile( "foo.zip", "w" , zipfile.ZIP_DEFLATED )
for file in files :
    print file
    fp.write( file )

fp.close()






