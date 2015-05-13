import os
import sys

import win32registry


def clean_start_menu_run_mru( ) :

    regpath = r"HKCU\Software\Microsoft\Windows\CurrentVersion\Explorer\RunMRU"
    win32registry.DeleteKeyRecursive( regpath )

    
if (__name__=='__main__') :

    clean_start_menu_run_mru()
