import os
import sys
import win32con
import win32api

"""
Shows how to test whether files have their readonly flag set and how to set and unset that flag
"""
def is_readonly( fname ) :
    attrib = win32api.GetFileAttributes( fname )
    return ( attrib & win32con.FILE_ATTRIBUTE_READONLY )

def is_readwrite( fname ) :
    attrib = win32api.GetFileAttributes( fname )
    return ( (attrib & win32con.FILE_ATTRIBUTE_READONLY) ==0 )

def set_readonly_flag( fname) :
    attrib = win32api.GetFileAttributes( fname )
    win32api.SetFileAttributes( fname , attrib | (win32con.FILE_ATTRIBUTE_READONLY) )

def remove_readonly_flag( fname) :
    attrib = win32api.GetFileAttributes( fname )
    win32api.SetFileAttributes( fname , attrib & (~win32con.FILE_ATTRIBUTE_READONLY) )
    
if (__name__=='__main__') :

    temp_fname = "C:\\test.txt"
    fp = file( temp_fname , "w" )
    fp.write("Hello World" )
    fp.close()

    assert( is_readwrite( temp_fname ) )
    set_readonly_flag( temp_fname)
    assert( is_readonly( temp_fname ) )
    remove_readonly_flag( temp_fname)
    assert( is_readwrite( temp_fname ) )
