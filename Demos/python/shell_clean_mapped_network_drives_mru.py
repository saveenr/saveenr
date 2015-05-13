import os
import sys

import win32registry


def clean_mapped_network_drives_mru(  ) :

    # Mapped Network Drives
    win32registry.DeleteKeyRecursive( r"HKCU\Software\Microsoft\Windows\CurrentVersion\Explorer\Map Network Drive MRU" )
    
if (__name__=='__main__') :

    clean_mapped_network_drives_mru()
