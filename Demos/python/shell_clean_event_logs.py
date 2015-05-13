import os
import sys

import win32evtlog

        
def clean_event_logs ( ) :
    lognames = [ 'Application', 'Security', 'System' ]
    machine = None
    for logname in lognames :
        h = win32evtlog.OpenEventLog( machine, logname )
        win32evtlog.ClearEventLog(h, None )
        win32evtlog.CloseEventLog( h )


    
if (__name__=='__main__') :

    clean_event_logs()
