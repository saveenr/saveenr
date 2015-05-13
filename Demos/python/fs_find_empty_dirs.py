
import os
import sys

tree=[]
dic = {}

def calc ( p , LIST ) :
    print ">", p
    items = os.listdir( p )
    direct_file_count =0
    direct_dir_count = 0
    direct_other_count =0
    recursive_file_count =0
    recursive_dir_count = 0
    recursive_other_count =0

    SUBLIST = [ ]
    data_item = ( p, SUBLIST )
    child_dirs = []    

    for item in items:
        item = os.path.join( p, item )
        assert ( os.path.exists( item ) ) 
        if ( os.path.isdir( item ) ) :
            child_dirs.append( item )
            direct_dir_count += 1
        elif ( os.path.isfile( item ) ) :
            direct_file_count += 1
        else :
            assert( 0 )

    for child_dir in child_dirs :
        info = calc( child_dir , SUBLIST )
    
    dic[ p ] = ( ( direct_file_count, direct_dir_count), (recursive_file_count, recursive_dir_count) )
    LIST.append(  dic[p] )
    
start_path = sys.argv[1]
start_path = os.path.abspath( start_path)
calc(start_path, tree)

for k in dic:
    print k, dic[k]



