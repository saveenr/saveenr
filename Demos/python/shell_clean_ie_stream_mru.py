import os
import sys

import win32registry

def clean_ie_stream_mru( ) :
    win32registry.DeleteKeyRecursive( r"HKCU\Software\Microsoft\Windows\CurrentVersion\Explorer\StreamMRU" )

    
if (__name__=='__main__') :

    clean_ie_stream_mru()
