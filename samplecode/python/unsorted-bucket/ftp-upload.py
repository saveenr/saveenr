import os
import sys
import string
import pprint
 
def ftp_dir( ftp  ) :
 
    class Callback :
        def __init__( self ) :
            self.L = []
 
        def __call__( self, line ) :
            self.L.append( line )
 
    cb = Callback()
    ftp.dir( cb  )
    return cb.L
 
import ftplib
 
ftp = ftplib.FTP( "ftp.foo.org", "username", "password" )
 
X = ftp_dir( ftp )
pprint.pprint( X )
 
ftp.storbinary( "STOR x.jpg", open( "c:\\x.jpg", "rb" ) )
