import os
import sys
import string
import win32con
import win32com.client

"""

Python has great COM integration. This example shows how to create a COM object in python that other COM clients can call

All you need to do is
    - create a class
    - fill in those COM-specific atttributes 
        _reg_clsid_ 
        _reg_desc_ 
        _reg_progid_ 
        _public_methods_ 
        _public_attrs_ 
        _readonly_attrs_ 

History
2004-12-29 Added better docstring
2002-11-18 Initial version

""" 

class PyTestCOMServer:
 
    _reg_clsid_ = "{6C7BF07E-DA23-4d59-8145-CCC150EED36C}"
    _reg_desc_ = "PyTestCOMServer"
    _reg_progid_ = "Python.PyTestCOMServer"
    _public_methods_ = ['Square']
    _public_attrs_ = ['Creator', 'Description']
    _readonly_attrs_ = ['Creator']
 
    def __init__(self):
        self.Creator = "Microsoft User"
        self.Description = "No Description"
 
    def Square(self, n ):
        return n * n

if ( __name__=='__main__' ) :

    # If this is being run as a script (instead of being imported as a library)
    # then register the object in the registry

    import win32com.server.register 
    win32com.server.register.UseCommandLine(PyTestCOMServer)
 
