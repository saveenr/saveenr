import os
import sys
import win32com.client
import fnmatch

def favorites_delete( ) :
        sh = win32com.client.Dispatch("WScript.Shell")
        path = unicode( sh.SpecialFolders("Favorites") )

        if ( not os.path.exists( path ) ) :
                print "Favorites path %s does not exist" % path
                sys.exit()

        for cur_path, dirs, files in os.walk( path ) :
                url_files = fnmatch.filter( files, "*.url" )
                url_files = [ os.path.join( cur_path , s ) for s in url_files ]
                for url_file in url_files :
                        assert( os.path.exists( url_file ) )
                        try :
                                print url_file.encode('ascii','ignore')
                                os.unlink( url_file )
                        except :
                                print 'Failed to delete', url_file


if (__name__=='__main__') :

        favorites_delete()
