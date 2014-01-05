import os
import sys
import win32con
import win32api
import win32security
import string
import pywintypes
import unittest
import _winreg

"""
win32registry.py

This module provides simple utility functions for manipulating the windows registry

History
-------
2004-12-29 Updated module with docstring
2003-04-10 Initial version

"""
reg_sep = "\\"

reg_root_map = {
    "hkcr" : win32con.HKEY_CLASSES_ROOT,
    "hkey_classes_root" : win32con.HKEY_CLASSES_ROOT,
    "hkcu" : win32con.HKEY_CURRENT_USER,
    "hkey_current_user" : win32con.HKEY_CURRENT_USER,
    "hklm" : win32con.HKEY_LOCAL_MACHINE,
    "hkey_local_machine" : win32con.HKEY_LOCAL_MACHINE}

reg_type_string_map ={
    win32con.REG_BINARY :"REG_BINARY",
    win32con.REG_DWORD:"REG_DWORD",
    win32con.REG_DWORD_BIG_ENDIAN:"REG_DWORD_BIG_ENDIAN",
    win32con.REG_DWORD_LITTLE_ENDIAN:"REG_DWORD_LITTLE_ENDIAN",
    win32con.REG_EXPAND_SZ:"REG_EXPAND_SZ",
    win32con.REG_FULL_RESOURCE_DESCRIPTOR:"REG_FULL_RESOURCE_DESCRIPTOR",
    win32con.REG_LINK:"REG_LINK",
    win32con.REG_MULTI_SZ:"REG_MULTI_SZ",
    win32con.REG_NONE:"REG_NONE",
    win32con.REG_SZ:"REG_SZ",
    win32con.REG_RESOURCE_LIST:"REG_RESOURCE_LIST",
    win32con.REG_RESOURCE_REQUIREMENTS_LIST:"REG_RESOURCE_REQUIREMENTS_LIST",
    11:"UNKNOWN"
    
    }

def join( *l ) :
    s = string.joinfields( l, reg_sep )
    return s

def splitroot( p) :
    L = p.split( reg_sep )
    S1 = L[0].lower( )
    S2 = reg_sep.join( L[1:] )
    return (S1,S2)

def split( p) :
    L = p.split( reg_sep )
    S1 = reg_sep.join( L[:-1] )
    S2 = L[-1]
    return (S1,S2)

def parse_regpath( regpath ) :
    root_str , path_str = splitroot( regpath )
    root_str = root_str.lower()
    root_key = reg_root_map[ root_str ]
    return root_key, path_str

def parse_valuepath( valuepath ) :
    regpath , valuename = split( valuepath )
    root_key , path_str = parse_regpath( regpath )
    return root_key, path_str, valuename


def DeleteKey( regpath ) :
    root_key , path = parse_regpath( regpath )
    _winreg.DeleteKey( root_key, path )
    assert( not KeyExists( regpath ) )


def DeleteKeyRecursive( regpath ) :
    if ( KeyExists(regpath) ) :
        subkeys = GetSubKeys( regpath )
        for sk in subkeys :
            sk = join( regpath, sk )
            DeleteKeyRecursive( sk )
        DeleteKey( regpath )
    assert( not KeyExists(regpath) )
    
def GetSubKeys( regpath ) :
    root_key , path = parse_regpath( regpath )
    handle = _winreg.OpenKey( root_key, path )
    num_subkeys, num_subvalues, lastmod = _winreg.QueryInfoKey( handle )
    subkeys = []
    for i in xrange(num_subkeys) :
        subkeys.append( _winreg.EnumKey( handle, i ) )
    return subkeys

def KeyExists( regpath ) :
    root_key , path_str = parse_regpath( regpath )
    try :
        handle = _winreg.OpenKey( root_key, path_str )
    except :
        return False
    return True
    del handle

def ParentKeyExists( regpath ) :
    parent_regpath, name= regpath_split( regpath )
    return KeyExists( parent_regpath )

def FindSubKeysRecursive( regpath , name = None ) :
    if (name!=None) : name = name.lower()
    LIST = []
    subkeys = GetSubKeys( regpath )
    for subkey in subkeys :
        if ( (name==None) or (subkey.lower() == name) ) :
            LIST.append( join( regpath, subkey ) )
    for subkey in subkeys :
        L = FindSubKeysRecursive( join( regpath, subkey ) , name )
        LIST.extend( L )
    return LIST        

def FindValuesRecursive( regpath , name ) :
    LIST = []
    keys = FindSubKeysRecursive( regpath )
    if ( name != None ) :
        name = name.lower()
    for k in keys :
        for vn,vv,vt in GetValues( k ) :
            if ( (name==None) or (vn.lower() == name) ):
                LIST.append( join( k, vn ) )
    return LIST


def CreateKey( regpath ) :

    root_key , path_str = parse_regpath( regpath )
    parent_regpath, name= split( regpath )
    if ( not KeyExists( parent_regpath ) ) :
        CreateKey( parent_regpath )

    assert( KeyExists( parent_regpath ))

    if ( not KeyExists( regpath ) ) :
        reserved=0
        access = win32con.KEY_ALL_ACCESS
        parent_path, name= split( path_str )
        hk = win32api.RegOpenKey( root_key, parent_path, reserved , access)
        subhk = win32api.RegCreateKey( hk,name )
        win32api.RegCloseKey( subhk )
        win32api.RegCloseKey( hk )

    assert( KeyExists( regpath ) )

def SetValue( valuepath, v, t ) :
    parent_regpath, valuename = split( valuepath )
    SetKeyValue( parent_regpath, valuename , v, t)

def SetKeyValue( regpath, valuename, v, t ) :
    root_key , path = parse_regpath( regpath )
    handle = _winreg.OpenKey( root_key, path , 0, _winreg.KEY_ALL_ACCESS )
    _winreg.SetValueEx( handle, valuename , 0, t, v )
    handle.Close()

def GetValues( regpath ) :

    root_key , path = parse_regpath( regpath )
    handle = _winreg.OpenKey( root_key, path )
    num_subkeys, num_subvalues, lastmod = _winreg.QueryInfoKey( handle )
    subvalues= []
    for i in xrange(num_subvalues) :
        subvalues.append( _winreg.EnumValue( handle, i ) )
    return subvalues


def GetValue( valuepath ) :
    regpath, valuename = split( valuepath )
    return GetKeyValue( regpath, valuename )

def GetKeyValue( regpath, valuename ) :
    root_key , path = parse_regpath( regpath )
    handle = _winreg.OpenKey( root_key, path )
    value = _winreg.QueryValueEx( handle, valuename )
    handle.Close()
    return value

def DeleteKeyValue( regpath, valuename) :
    root_key , regpath = parse_regpath( regpath )
    handle = _winreg.OpenKey( root_key, regpath, 0, _winreg.KEY_WRITE )
    _winreg.DeleteValue( handle, valuename )

def DeleteValue( valuepath ) :
    regpath, valuename = split( valuepath )
    DeleteKeyValue( regpath, valuename )

def KeyHasValue( regpath, valuename ) :
    root_key , regpath = parse_regpath( regpath )
    handle = _winreg.OpenKey( root_key, regpath, 0, _winreg.KEY_READ )
    num_subkeys, num_subvalues, lastmod = _winreg.QueryInfoKey( handle )
    valuename = valuename.lower()
    for i in xrange(num_subvalues) :
        vn,vv,vt = _winreg.EnumValue( handle, i )
        vn = vn.lower()
        if ( vn == valuename ) : return True
    return False
        
    
    _winreg.DeleteValue( handle, valuename )

"""
SaveKey( regpath , filename )

Examples:

    regpath  = r"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\WindowsFirewall"
    filename = "test-output.out" 
    SaveKey( regpath , filename )
    
"""
def SaveKey( regpath , filename ) :
    assert KeyExists( regpath )
    if ( os.path.exists( filename ) ) :
        os.unlink( filename )
    assert( not os.path.exists( filename ) )

    current_process = win32api.GetCurrentProcess()
    handle_token = win32security.OpenProcessToken( current_process, win32con.TOKEN_ADJUST_PRIVILEGES | win32con.TOKEN_QUERY )
    luid = win32security.LookupPrivilegeValue(None, "SeBackupPrivilege")
    new_privs = ( (luid, win32con.SE_PRIVILEGE_ENABLED), )
    win32security.AdjustTokenPrivileges( handle_token , False, new_privs)

        
    root_key , path = parse_regpath( regpath )
    handle = _winreg.OpenKey( root_key, path )

    try:
        _winreg.SaveKey( handle, filename )
    finally:
        win32security.AdjustTokenPrivileges( handle_token, True, new_privs )
        handle.Close()

    assert( os.path.exists( filename ) )
    
class TestCase1(unittest.TestCase) :

    def setUp(self) :
        self.test_regpath = "hkcu\\software\\temp\\pythonregistrytest"
        self.test_keys = []
        for p in [ "", "\\testkey1" , "\\testkey2" , "\\testkey3" , "\\testkey3" ] :
            p = join( self.test_regpath + p )
            self.test_keys.append( p )

        for regpath in self.test_keys :
            CreateKey( regpath )
        
    def tearDown(self) :
        DeleteKeyRecursive( self.test_regpath )
        self.assert_( not KeyExists( self.test_regpath ) )
        

    def test2(self) :
        """
        Verifies that KeyExists actually finds keys that exists and doesn't find keys that don't
        """
        path = self.test_regpath
        subkeys = GetSubKeys( path )
        self.assert_( len(subkeys) > 0 )
        subkeys = [ join(path,sk) for sk in subkeys ]
        for sk in subkeys :
            self.assert_( KeyExists( sk ) )
            sk_dummy = sk + "_does_not_exist"
            self.assert_( not KeyExists( sk_dummy ) )
        
    def test3(self) :
        data = []
        data.append( ('v1', 100, win32con.REG_DWORD ) )

        regpath = join( self.test_regpath , "testkey3" )
        for valuename, valuevalue, valuetype in data :

            valuepath = join( regpath , valuename )
            SetValue( valuepath, valuevalue, valuetype )
            v,t = GetValue( valuepath )
            assert( v == valuevalue )
            assert( t == valuetype )

        allvalues = GetValues( regpath )
        assert( len(allvalues) == len(data) )


    def test4(self) :
        regpath = self.test_keys[1]
        SetKeyValue( regpath, "foo", 1, win32con.REG_DWORD )
        v,t = GetKeyValue( regpath, "foo" )

    def test5_save_key(self) :
        regpath  = r"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\WindowsFirewall"
        filename = "test-output.out" 
        SaveKey( regpath , filename )
    
if (__name__=='__main__') :
    print "Running test cases"
    unittest.main()
    