import os
import sys

import wmi


def find_drive_letter_with_name( desired_volume_name ) :
    desired_volume_name = desired_volume_name.lower()
        
    c = wmi.WMI ()

    for drive in c.Win32_LogicalDisk():

        vn = drive.VolumeName
        if ( vn==None) :
            continue

        vn = drive.VolumeName.lower()
        
        if ( vn == desired_volume_name ) :
            return drive.Name
        
    return None
        

if ( __name__ == '__main__' ) :

    volume_name = 'Foobar'
    print "searching for drive with volume name \"%s\"" % volume_name
    drive_letter = find_drive_letter_with_name( volume_name )
    if ( drive_letter == None ) :
        print "Didn't find any"
    else :
        print "Found drive", drive_letter

    print
    volume_name = ""
    print "searching for a drive with no volume name \"%s\"" % volume_name
    drive_letter = find_drive_letter_with_name( volume_name )
    if ( drive_letter == None ) :
        print "Didn't find any"
    else :
        print "Found drive", drive_letter
        
