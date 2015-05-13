"""

This example lists all the shell CDISDL constants

"""

from win32com.shell import shell
from win32com.shell import shellcon

if ( __name__ == "__main__" ) :

    constant_names = [ s for s in dir(shellcon) if s.startswith("CSIDL") ] 
    for constant_name in constant_names :
        constant_value = getattr( shellcon, constant_name )
        print "%s = %s" % (constant_name, constant_value )
