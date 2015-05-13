import os
import sys

import win32registry

    
def clean_ie_typed_urls( ) :
    win32registry.DeleteKeyRecursive( r"HKCU\Software\Microsoft\Internet Explorer\TypedURLs" )

    
if (__name__=='__main__') :

    clean_ie_typed_urls()
