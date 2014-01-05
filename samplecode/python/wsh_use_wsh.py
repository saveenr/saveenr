import os
import sys
import win32com.client

"""
A simple example of the Windows Script Host's Shell object via Python COM interop

This example just launches notepad using WSH.

History
-------
2004-12-29 Added docstring
2004-11-21 Initial version

"""

if (__name__=='__main__') :

    wsh = win32com.client.Dispatch("WScript.Shell")
    wsh.Run( "Notepad.exe" )
    