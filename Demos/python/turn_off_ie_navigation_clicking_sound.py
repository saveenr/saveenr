import os
import sys
import win32com.client


def turn_off_app_event_sound( app, event ) :
  wsh = win32com.client.Dispatch("WScript.Shell")
  regpath = 'HKEY_CURRENT_USER\\AppEvents\\Schemes\\Apps\\%s\\%s\\.Current\\' % ( app, event )
  wsh.RegWrite( regpath, '' , 'REG_SZ' )

def turn_off_ie_navigating_sound() :

  turn_off_app_event_sound( "Explorer", "Navigating" )


if (__name__=='__main__') :

  turn_off_ie_navigating_sound()
