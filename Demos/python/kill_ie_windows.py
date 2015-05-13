import os
import sys

import wmi

if ( __name__ == '__main__' ) :

    computer = wmi.WMI ()

    for process in computer.Win32_Process():
        if ( process.Name != None ) :
                process_name = process.Name.lower()
                if ( "iexplore.exe" == process_name) :
                    process.Terminate()
 