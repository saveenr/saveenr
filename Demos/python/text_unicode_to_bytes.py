import os
import sys
import string
   
def unicode_to_bytes( us ) :
  us = [ (ord(c)&0x00ff,ord(c)&0xff00>>16) for c in us ]
  data = []
  for c in us :
    data.append(c[0])
    data.append(c[1])
  data = [ chr(c) for c in data ]
  data = string.joinfields( data, "" )
  return data

if (__name__=='__main__')  :


    unicode_string = unicode( "Hello World" )

    bytes = unicode_to_bytes( unicode_string )
    for i, b in enumerate( bytes ):
        print "[%d] 0x%02x %s" % ( i, ord(b), b )
    
