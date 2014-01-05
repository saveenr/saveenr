import os
import sys

import win32registry


def pythonwin_clean_mru( ) :

    regpath= r"HKEY_CURRENT_USER\Software\Python 2.3"

    if ( not win32registry.KeyExists( regpath ) ) :
        print "Python23 MRU not present"
        return 

    mru_reg_keys = win32registry.FindSubKeysRecursive( regpath, 'Recent File List' )
    
    for mru_reg_key in mru_reg_keys :
        values = win32registry.GetValues( mru_reg_key )
        for vn, valuevalue, valuetype in values :
            valuepath = win32registry.join( mru_reg_key, vn )
            win32registry.DeleteValue( valuepath )
    
if (__name__=='__main__') :

    pythonwin_clean_mru()
