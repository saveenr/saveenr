# ------------------------------
# SCRIPT: list_installed_apps.py
#
# EXAMPLE:
#  list_installed_apps.py
#
# NOTE: requires win32 extensions for python
#

import os
import sys
import _winreg
import shutil
               
def get_installed_apps() :
    hk = _winreg.OpenKey( _winreg.HKEY_LOCAL_MACHINE, r"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall" )
    num_subkeys , num_subvalues, lastmod = _winreg.QueryInfoKey( hk )
    appids = []
    for i in xrange(num_subkeys) :
        sk = _winreg.EnumKey(hk,i)
        appids.append( sk )
    _winreg.CloseKey( hk )

    app_list = []
    for appid in appids:
        app_info = {}
        hk = _winreg.OpenKey( _winreg.HKEY_LOCAL_MACHINE, r"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall" + "\\" + appid )
        num_subkeys , num_subvalues, lastmod = _winreg.QueryInfoKey( hk )
        for i in xrange(num_subvalues) :
            sv_name, sv_data, sv_type = _winreg.EnumValue(hk,i )
            app_info[ sv_name.lower() ] = sv_data
        app_list.append((appid,  app_info) )
            
        _winreg.CloseKey( hk )
    return app_list

if ( __name__ == '__main__' ) :
    app_list = get_installed_apps()
    
    for appid, appinfo in app_list :
        display_name = appinfo.get( "displayname" , "<no name>" )
        print appid, display_name
