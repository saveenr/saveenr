import os
import sys

import wmi


        

if ( __name__ == '__main__' ) :

    computer = wmi.WMI ()

    for entity in computer.Win32_DiskDrive():
        if ( entity.PNPDeviceID.startswith("USBSTOR") ) :
            print entity
        