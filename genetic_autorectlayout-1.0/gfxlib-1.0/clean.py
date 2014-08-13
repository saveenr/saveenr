import os
import sys
import glob

def get_output_folders( p ) :
    LIST = []
    for path, folders, files in os.walk( p , True ) :
        n = path.find( '\\bin\\' )
        if ( n<0 ) :
            n = path.find( '\\obj' )
        if (n>0) :
            LIST.append( (path,folders,files) )
    return LIST

def safedelete( p ) :
    try :
        os.unlink( p)
        return True
    except :
        print "\tFailed To Delete...", p
    return False
    
def main() :
    extensions=['.exe','.pdb','.projdata','.dll']
    root_path = os.path.dirname( sys.argv[0] )
    assert( os.path.isdir( root_path ) )

    output_paths = get_output_folders( root_path )
    output_paths.sort(lambda x, y: cmp(x[0],y[0]))
    output_paths.reverse()

    for path, folders, files in output_paths :
        for file in files :
            abs_filename = os.path.join( path, file )
            abs_filename = os.path.normcase( abs_filename )
            print abs_filename
            dummy, ext = os.path.splitext( abs_filename )
            if ( ext in abs_filename ) :
                safedelete( abs_filename )
            else :
                print 'UNKNOWN FILE', abs_filename
        safedelete( path )

if ( __name__=='__main__' ) :
    main()






