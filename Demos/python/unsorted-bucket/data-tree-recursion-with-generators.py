from __future__ import generators

def recurse( node ) :
    yield node
    for child in node.children :        
        for n in recurse( child ) :
            yield n
 
for node in recurse( root_node ) :
    print node
