
import os
import sys
import string
import win32com.client
import datetime

def quote(s) :
    return "\"" +s + "\""

def backup( bk_path , bkf_filename , jobname ) :
    assert( os.path.isdir( bk_path ) )    

    cmdline = """ntbackup backup %s /j %s /f %s""" % ( quote(bk_path) , quote(jobname), quote(bkf_filename) )
    err = os.system(cmdline)
    assert( os.path.exists( bkf_filename ) )


def main() :

    sh = win32com.client.Dispatch("WScript.Shell")
    src_path = unicode( sh.SpecialFolders("MyDocuments") )
    today = datetime.datetime.today()
    fname = os.environ[ "username" ] + "_on_" + os.environ[ "computername" ] + "_" + today.strftime("%Y_%m_%d_%H%M%S") + ".bkf"
    dest_filename = os.path.join( os.path.splitdrive( src_path )[0]+os.sep , fname )
    description = "Backup for " + os.environ[ "username" ] + " on " + os.environ[ "computername" ] + " at " + str(today)

    print "From \"My Documents\" at:", src_path
    print "To file \"%s\"" % dest_filename
        
    backup( src_path, dest_filename, description )
                

if ( __name__ == '__main__' ) :
    print "Backup Mydocs"
    main()