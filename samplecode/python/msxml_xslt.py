"""
This example demonstrates how to use MSXML to load transform XML using XSLT

It creates a DOM from from a string that contains an XML document
It creates a DOM from from a string that contains the XSL transform
It then creates an output string that represents tha transformed XML

"""
import win32com.client

input_xml = """
<foo>
    <bar>
        <beer/>
    </bar>
    <beer/>
</foo>

"""

xslt_xml = """
<?xml version='1.0'?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0">
  <xsl:template match="/">

	<a>

	<xsl:for-each select="//beer">
		bees
	</xsl:for-each>

	</a>


  </xsl:template>
</xsl:stylesheet>

"""

def load_xml_string( s ) :
    dom = win32com.client.Dispatch("Microsoft.XMLDOM")
    dom.validateOnParse = 0
    dom.loadXML( s )
    if ( dom.parseError() != 0 ) :
        err = Exception( "XML Parse Error" )
        raise err
    return dom


if ( __name__ == '__main__' ) :	

    dom = load_xml_string( input_xml )
    xslt = load_xml_string( xslt_xml )
        
    output_xml_string = dom.transformNode( xslt )

    print output_xml_string









