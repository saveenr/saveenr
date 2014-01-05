import os
import sys

import xml.sax.saxutils

"""
Writing XML file using SAX
-         use XMLGenerator class in xml.sax.saxutils
""" 

fp = file( "\\out.xml" , "w" )

o = xml.sax.saxutils.XMLGenerator( fp,"utf-8")
o.startDocument()
o.startElement( "foo", {} )
o.startElement( "bar", { 'A1':'1234' } )
o.characters("Hello World")
o.endElement( "bar" )
o.endElement( "foo" )
o.endDocument()

fp.close()
