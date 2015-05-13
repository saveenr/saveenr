import os
import sys

import wmi


        

if ( __name__ == '__main__' ) :

    computer = wmi.WMI ()

    for share in computer.Win32_Share():

        print "SHARE", share.Name
        print "\tpath:", share.Path
        