import os
import sys

import win32registry


def clean_file_open_mru( ) :
    # File Open Dialog
    win32registry.DeleteKeyRecursive( r"HKCU\Software\Microsoft\Windows\CurrentVersion\Explorer\ComDlg32\LastVisitedMRU" )

    
if (__name__=='__main__') :

    clean_file_open_mru()
