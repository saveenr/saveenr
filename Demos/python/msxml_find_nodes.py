"""
This example demonstrates how to use MSXML to load transform XML using XSLT


"""
import win32com.client

input_xml = """
<foo>
    <bar>
        <x/>
        <beer/>
        <x/>
    </bar>
    <x/>
    <beer/>
    <x/>
</foo>

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


    
    n1 = [ n for n in dom.selectNodes( "//x" ) ]
    print n1








