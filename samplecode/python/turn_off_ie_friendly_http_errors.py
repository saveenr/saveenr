import os
import sys
import win32com.client

"""
----------------------------------------

HISTORY
-------


2006-06-10
initial version

----------------------------------------
"""

def turn_off_ie_friendly_http_errors() :

  wsh = win32com.client.Dispatch("WScript.Shell")
  regpath = r'HKEY_CURRENT_USER\Software\Microsoft\Internet Explorer\Main\Friendly http errors'
  wsh.RegWrite( regpath, 'no' , 'REG_SZ' )

if (__name__=='__main__') :

  turn_off_ie_friendly_http_errors()
