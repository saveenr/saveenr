import os
import sys

import win32evtlog
        
def clean_temp_files( self ) :

    paths = []
    paths.append( os.environ["temp"] )
    paths.append( os.environ["tmp"] )


    files = []
    for path in paths :
        if ( os.path.isdir( path ) ):
                files.extend( [ f for f in walk_files( path ) ] )

if (__name__=='__main__') :

    clean_temp_files()
