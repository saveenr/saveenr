import os
import sys

import win32registry


def clean_icon_cache( ) :
    fnames = []

    path = os.path.join( os.environ[ 'APPDATA' ] , "IconCache.db" )
    fnames.append(path )

    path = os.path.join( os.environ[ 'USERPROFILE' ] , "Local Settings", "Application Data", "IconCache.db" )
    fnames.append(path )

    fnames = filter( os.path.isfile, fnames )

    assert(0)
    
if (__name__=='__main__') :

    clean_icon_cache()
