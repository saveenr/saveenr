import os
import sys

import win32registry


def clean_media_player_mru( ) :
    win32registry.DeleteKeyRecursive( r"HKCU\Software\Microsoft\MediaPlayer\Player\RecentFileList" )
    
if (__name__=='__main__') :

    clean_media_player_mru()
