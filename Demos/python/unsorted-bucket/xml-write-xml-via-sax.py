import os
import sys

import xml.sax.saxutils

"""
Writing XML file using SAX
-         use XMLGenerator class in xml.sax.saxutils

-         you’ll need to do charset encoding (if needed) before you output the characters

 
""" 
 
o = xml.sax.saxutils.XMLGenerator( sys.stdout ,"utf-8")
o.startDocument()
o.startElement( "foo", {} )
o.startElement( "bar", { 'A1':'1234' } )
o.characters("Hello World")
o.characters(u"N with accent on top: \xf1".encode("utf-8"))
o.endElement( "bar" )
o.endElement( "foo" )
o.endDocument()
