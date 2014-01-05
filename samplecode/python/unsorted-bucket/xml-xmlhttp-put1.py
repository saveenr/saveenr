"""

This simple example shows how to create an HTML file on an IIS server

"""

import os
import sys
import win32com.client

def main() :
	body = "<HTML><BODY><H1>HelloWorld</H1></BODY></HTML>"
	xmlhttp = win32com.client.Dispatch("Microsoft.XMLHTTP")
	xmlhttp.Open( "PUT", "http://icecool/users/saveenr/test.htm", 0 )
	xmlhttp.setRequestHeader( "Translate", "f" )	
	xmlhttp.setRequestHeader( "Content-Type", "text/html" )	
	xmlhttp.setRequestHeader( "Content-Length", len(body) )	
	xmlhttp.Send( body  )
	print "STATUS: ", xmlhttp.status
	del xmlhttp

main()
