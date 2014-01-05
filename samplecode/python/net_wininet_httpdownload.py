import os
import sys
import win32api
import ctypes

"""

Calls wininet APIs to download a a url to a local file.

Requires the ctypes library.

History
-------
2004-12-29 Added docstring
2004-12-22 Initial Version

"""

syntax = """
syntax:
    wininet_httpdownload.py
    wininet_httpdownload.py <url> <fname>
"""

class wininet_error :
    def __init__(self,s) :
        self.message = s
    
def syntax_error( ) :
    print syntax
    sys.exit(-1)

def download( url, fname ) :
    assert( url != None )
    assert( fname != None)
    
    try :
        wininet = ctypes.windll.wininet

        INTERNET_OPEN_TYPE_PRECONFIG_WITH_NO_AUTOPROXY = 0x04
        
        open_agent = None
        open_access = None
        open_proxy = INTERNET_OPEN_TYPE_PRECONFIG_WITH_NO_AUTOPROXY
        open_bypass = None
        open_flags = 0
        handle_inet = 0
        handle_inet =wininet.InternetOpenW( open_agent, open_access, open_proxy, open_bypass, open_flags )
        if ( handle_inet == 0 ) :
            raise wininet_error("Failed wininet.InternetOpenW")

        fp = None
        fp = open( fname, "wb" )


        openurl_headers_length = 0
        openurl_headers = None
        openurl_url = ctypes.c_wchar_p( unicode(url) )
        openurl_flags = 0
        openurl_context = None

        handle_url = 0
        handle_url = wininet.InternetOpenUrlW( handle_inet,
                                               openurl_url ,
                                               openurl_headers,
                                               openurl_headers_length,
                                               openurl_flags,
                                               openurl_context )
        if ( handle_url == 0 ) :
            raise wininet_error("Failed wininet.InternetOpenUrlW")
        
        bufsize = 8192
        buffer = ctypes.c_buffer("",bufsize)
        bytes_read = ctypes.c_long(0)
        total = 0
        while ( True ) :
            download_status = wininet.InternetReadFile( handle_url,
                                                        buffer,
                                                        bufsize,
                                                        ctypes.byref( bytes_read ) )
            if ( not download_status ) :
                raise wininet_error("Failed wininet.InternetReadFile" )
            total += bytes_read.value
            print total, "bytes"
            if ( bytes_read.value > 0 ) :
                b = buffer.raw[:bytes_read.value]
                fp.write( b )
            else :
                break
                
    except wininet_error,e:
        print "WININET ERROR",e.message

    if ( not handle_url) :
        wininet.InternetCloseHandle( handle_url )

    if ( fp != None ) :
        fp.close()
        
    if ( not handle_inet ) :
        wininet.InternetCloseHandle( open_agent, open_access, open_proxy, open_bypass, open_flags )

if ( __name__ == '__main__' ) :
    
    if ( len(sys.argv) < 3 ) :
        syntax_error()

    url = sys.argv[1].strip()
    fname = sys.argv[2].strip()
        
    download( url, fname )
