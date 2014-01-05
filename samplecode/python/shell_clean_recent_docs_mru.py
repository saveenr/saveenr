import os
import sys

import win32registry


def clean_recent_docs_mru( ) :
    win32registry.DeleteKeyRecursive( r"HKCU\Software\Microsoft\Windows\CurrentVersion\Explorer\RecentDocs" )
    
if (__name__=='__main__') :

    clean_recent_docs_mru()
