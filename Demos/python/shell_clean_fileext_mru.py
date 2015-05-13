import os
import sys

import win32registry


def clean_fileext_mru( ) :
    win32registry.DeleteKeyRecursive( r"HKCU\Software\Microsoft\Windows\CurrentVersion\Explorer\FileExts" )
    
if (__name__=='__main__') :

    clean_fileext_mru()
