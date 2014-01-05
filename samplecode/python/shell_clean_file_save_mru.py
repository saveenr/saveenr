import os
import sys

import win32registry


def clean_file_save_mru( ) :
    # File Save Dialog
    win32registry.DeleteKeyRecursive( r"HKCU\Software\Microsoft\Windows\CurrentVersion\Explorer\ComDlg32\OpenSaveMRU" )


    
if (__name__=='__main__') :

    clean_file_save_mru()
