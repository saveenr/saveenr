import os
import sys

import xml.sax.saxutils

"""
Writing HTML file using SAX
-         use XMLGenerator class in xml.sax.saxutils
""" 

fp = file( "\\out.html" , "w" )

o = xml.sax.saxutils.XMLGenerator( fp,"utf-8")
#o.startDocument() Don't do this to avoid IE complaining about opening XML
o.startElement( "html", {} )
o.startElement( "head", {} )
o.endElement( "head" )
o.startElement( "body", {} )
o.startElement( "h1", {} )
o.characters("Hello World")
o.endElement( "h1" )
o.endElement( "body" )
o.endElement( "html" )
#o.endDocument() Don't do this to avoid IE complaining about opening XML

fp.close()
