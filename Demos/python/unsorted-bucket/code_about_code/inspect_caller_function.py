import os
import sys
import string
import types
import inspect

"""
This example demostrates how to create a debugging function that prints out the name of the caller function 
"""

def method_begin() :

    cf  = inspect.currentframe()
    ofs = inspect.getouterframes ( cf )
    caller_frame = ofs[1]

    fi = inspect.getframeinfo( cf )
    print "method begin: ", caller_frame[3]

def method_end() :

    cf  = inspect.currentframe()
    ofs = inspect.getouterframes ( cf )
    caller_frame = ofs[1]

    fi = inspect.getframeinfo( cf )
    print "method end: ", caller_frame[3]


def foo( ) :
    method_begin()
    bar( ) 
    method_end()


def bar( ) :
    method_begin()
    print"Hello World"
    method_end()


foo()
