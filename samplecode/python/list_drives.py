import os
import sys

import wmi


if ( __name__ == '__main__' ) :

    computer = wmi.WMI ()

    for drive in computer.Win32_LogicalDisk():
        print "Drive", drive.Name
        print "\t Filesystem:", drive.FileSystem
        print "\t       Size:", drive.Size
        print "\t  Freespace:", drive.FreeSpace
        print "\t  Mediatype:", drive.MediaType
        print "\t  DriveType:", drive.DriveType

        if ( drive.FileSystem != None ) :
            print "\tPercentFull: %0.1f%%" %  (100.0 * (float(drive.Size) - float(drive.FreeSpace))/float(drive.Size))
        print
