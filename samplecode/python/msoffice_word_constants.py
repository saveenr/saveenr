import os
import win32com.client

import sys

def enum_win32com_constants( ) :
	for dic in win32com.client.constants.__dicts__ :
		for k,v in dic.iteritems() :
			yield k,v
			
if (__name__=='__main__') :

	win32com.client.gencache.EnsureDispatch("Word.Application")


	wd_constants = [ (k,v) for (k,v) in enum_win32com_constants() if k.startswith("wd" ) ]
	wd_constants.sort(lambda x, y: cmp(x[0],y[0]) )

	for name,value in wd_constants :
		print str(value).rjust(12),name
	