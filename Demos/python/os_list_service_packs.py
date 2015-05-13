import os
import sys

import wmi
import win32com.client
        

if ( __name__ == '__main__' ) :
        
    c = wmi.WMI ()

    print
    print "Service Pack Info"
    for item in c.Win32_OperatingSystem():
        print "Caption:", item.Caption
        print "Service Pack Version: %s.%s" %( item.ServicePackMajorVersion,item.ServicePackMinorVersion )

    print
    print "Service Pack Hot Fixes"
    for item in c.Win32_QuickFixEngineering():
        print "Hotfix %s %s" % ( item.HotFixID, item.Description )

    print
    print "Windows Update Info"
    wu = win32com.client.Dispatch( "Microsoft.Update.Session" )
    searcher = wu.CreateUpdateSearcher()
    col = searcher.QueryHistory(1, searcher.GetTotalHistoryCount() )
    for item in col :
        print "Title:", item.Title

