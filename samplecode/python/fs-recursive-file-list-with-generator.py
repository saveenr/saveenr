
import os
import sys

def recurse_listfolders_depth_first( path ) :
    names = os.listdir( path )
    
    for child in names :
        abs_child_fname = os.path.join( path, child )
        if ( os.path.isdir( abs_child_fname ) ) :
            for fname in recurse_listfolders_depth_first( abs_child_fname ) :
                yield abs_child_fname
            yield abs_child_fname

def walk_files( path ) :
    for dir,links,files in os.walk( path ) :
        for file in files :
            yield ( os.path.join( dir, file ) )
            
def walk_files_with_extensions( path , exts ) :
    exts = [ os.path.normcase( s ) for s in exts ]
    
    for dir,links,files in os.walk( path ) :
        for file in files :
            a,b = os.path.splitext( file )
            b = os.path.normcase( b )
            if (b in exts) :
                abs_fname = os.path.join( dir, file )
                yield ( abs_fname )


