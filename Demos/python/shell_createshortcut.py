
import os
import sys
import string
import win32com.client
from win32com.shell import shell
from win32com.shell import shellcon

sh = win32com.client.Dispatch( "WScript.Shell" )
shapp = win32com.client.Dispatch( "Shell.Application" )
appname = "My Application"
target = "http://www.microsoft.com"

start_menu_app_programs_path = shell.SHGetFolderPath( 0, win32com.shell.shellcon.CSIDL_PROGRAMS , 0, 0 )
assert( os.path.isdir(start_menu_app_programs_path) )

start_menu_my_program_path = os.path.join( start_menu_app_programs_path, appname )
if ( not os.path.isdir( start_menu_my_program_path ) ) :
    os.makedirs(start_menu_my_program_path)
    
lnk = sh.CreateShortcut( os.path.join( start_menu_my_program_path, appname + ".lnk" )) 
lnk.TargetPath = target
lnk.Save()
