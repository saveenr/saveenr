#
# DESCRIPTION
# -----------
#
#
# WORKITEMS
# ---------
#
# HISTORY
# -------
#
# 2005-12-20
# Initial version
#

import os
import win32com.client

def add_cmd_prompt( ) :
    regpath = r"HKEY_LOCAL_MACHINE\SOFTWARE\Classes\Folder\shell\Command Prompt"
    menu_item_text = "Open CMD.EXE here"
    cmdline = "CMD.EXE /K pushd %L"

    wsh = win32com.client.Dispatch( "WScript.Shell" )
    
    wsh.RegWrite( regpath + "\\", menu_item_text , "REG_SZ" )
    wsh.RegWrite( regpath + "\\command\\", cmdline , "REG_SZ" )

if ( __name__ == '__main__' ) :

    add_cmd_prompt( )
    
