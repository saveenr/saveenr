from __future__ import generators

def iterate_with_index( L ) :
    index = 0
    for item in L :
        yield index, item
        index+=1
 
for (index,item) in [ 100, 200, 300 ] :
    print index,item
