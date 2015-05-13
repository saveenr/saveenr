import os
import sys

import win32registry


def office_clean_mrus( ) :


    regpath=r"HKEY_CURRENT_USER\Software\Microsoft\Office"
    if ( not win32registry.KeyExists( regpath ) ) :
        print "Office Key does not exist"
        print "Nothng to clean."
        return

    delete_count = 0            

    value_name = 'value' 
    mru_reg_keys = win32registry.FindSubKeysRecursive( regpath, 'file name mru' )
    mru_reg_values = [ win32registry.join( s, value_name ) for s in mru_reg_keys ]
    
    for mru_reg_key in mru_reg_keys :
        
        values = win32registry.GetValues( mru_reg_key )
        for vn, valuevalue, valuetype in values :
            if ( vn.lower() == value_name ) :
                valuepath = win32registry.join( mru_reg_key, value_name )
                win32registry.DeleteValue( valuepath )

    mru_reg_keys = [ ]
    mru_reg_keys.extend( win32registry.FindSubKeysRecursive( regpath, 'recent file list' ) )
    mru_reg_keys.extend( win32registry.FindSubKeysRecursive( regpath, 'recenttemplatelist' ) )
    mru_reg_keys.extend( win32registry.FindSubKeysRecursive( regpath, 'recentfolderlist' ) )
    mru_reg_keys.extend( win32registry.FindSubKeysRecursive( regpath, 'recent typeface list' ) )

    for mru_reg_key in mru_reg_keys :
        win32registry.DeleteKeyRecursive( mru_reg_key )

    win32registry.DeleteKeyRecursive( 'HKEY_CURRENT_USER\Software\Microsoft\Office\11.0\Common\Internet\Server Cache' )

    regpath=r"HKEY_CURRENT_USER\Software\Microsoft\Office\11.0\Outlook\Preferences"
    win32registry.DeleteKeyValue( regpath, 'LocationMRU' )


    
if (__name__=='__main__') :

    office_clean_mrus()
