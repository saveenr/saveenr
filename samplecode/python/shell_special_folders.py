from win32com.shell import shell
from win32com.shell import shellcon

constant_names = [ s for s in dir(shellcon) if s.startswith("CSIDL") ] 
for constant_name in constant_names:
    print constant_name
    constant_value = getattr( shellcon, constant_name )
    folder = "ERROR"
    try :
        folder = shell.SHGetFolderPath( 0, constant_value , 0, 0 )
    except :
        pass

    print "\t", folder
    print
    