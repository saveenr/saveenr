import os
import sys
import win32con
import win32registry

"""
Sets Internet Explorer's homepage to the specified url

History
-------
2004-12-29 Added docstring
2004-12-28 Added examples
2004-10-01 Initial version

"""

def ie_set_homepage( url ) :
    p=r"HKEY_CURRENT_USER\Software\Microsoft\Internet Explorer\Main"
    win32registry.SetKeyValue( p, "Start Page" , url , win32con.REG_SZ )
    
syntax = """
Syntax:
    ie_set_homepage.py <url>

Examples:
    ie_set_homepage.py http://www.python.org
    ie_set_homepage.py http://localhost
    
    """

def syntax_error() :
        print syntax
        sys.exit(-1)

if (__name__=='__main__') :

    if ( len(sys.argv) <2 ) :
        syntax_error()

    url = sys.argv[1]
    ie_set_homepage( url )
