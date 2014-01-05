import os
import sys
import pprint
   
"""
get_dir_tree( path )
Generates a recursive list of tuples of form ( path, [children] )
that represents a directory tree

"""

def get_dir_tree( p, name=None ) :
    if ( name == None) : name = p
    child_dirs = []
    dirinfo = ( name, child_dirs )
    items = [ os.path.join(p,i) for i in os.listdir(p) ]
    dirs = [ i for i in items if os.path.isdir( i ) ]
    child_dirs.extend([ get_dir_tree(i, os.path.basename( i ) ) for i in items if os.path.isdir( i )])
    return dirinfo

def print_dir_tree( node , depth = 0) :
    indentstring = depth * "\t"
    name = node[0]
    print indentstring + name + "\\"
    for child_dir in node[1] :
        print_dir_tree( child_dir , depth + 1)

if (__name__=='__main__')  :


    the_path = os.getcwd()
    if ( len(sys.argv)>=2 ) :the_path = sys.argv[1]

    dirtree = get_dir_tree( the_path )
    print_dir_tree( dirtree  )
