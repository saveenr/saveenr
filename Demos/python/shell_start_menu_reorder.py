import os
import sys

import win32registry
        
def reorder_start_menu_programs( ) :

    regpath = r"HKCU\Software\Microsoft\Windows\CurrentVersion\Explorer\MenuOrder"
    win32registry.DeleteKeyRecursive( regpath )
    
if (__name__=='__main__') :

    reorder_start_menu_programs()
