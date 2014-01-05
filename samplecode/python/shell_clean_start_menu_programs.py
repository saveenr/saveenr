import os
import sys

import win32registry

        
def clean_start_menu_programs( ) :

    """
    In order to see this work you'll either have to log off and then back on or
    simply kill explorer.exe and start it again from task manager
    
    """
    regpath = r"HKCU\Software\Microsoft\Windows\CurrentVersion\Explorer\UserAssist\{5E6AB780-7743-11CF-A12B-00AA004AE837}\Count"
    win32registry.DeleteKeyRecursive( regpath )

    regpath = r"HKCU\Software\Microsoft\Windows\CurrentVersion\Explorer\UserAssist\{75048700-EF1F-11D0-9888-006097DEACF9}\Count"
    win32registry.DeleteKeyRecursive( regpath )

    
if (__name__=='__main__') :

    clean_start_menu_programs()
