from __future__ import nested_scopes
import os, sys, string
import urllib
import xml.dom.minidom
 
url = "http://www.showmyip.com/xml/"
(fname, headers) = urllib.urlretrieve( url )
 
print "fname",fname
print "headers", headers
 
dom1 = xml.dom.minidom.parse( fname ) 
 
def getTextOfNode( node ) :
    s = ""    
    if node.nodeType == node.TEXT_NODE:
        s += node.data
    elif node.nodeType == node.ELEMENT_NODE:
        for child in node.childNodes:
            s += getTextOfNode( child )
        
    return s
 
def getTextOfNamedNode( node , name ) :
    L = node.getElementsByTagName( name )
    return getTextOfNode( L[0] )
 
 
print getTextOfNamedNode( dom1, "host" )
 
print getTextOfNamedNode( dom1, "ip" )
 
dom1.unlink()
