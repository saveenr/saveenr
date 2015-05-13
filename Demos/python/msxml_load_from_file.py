"""
This example demonstrates how to use MSXML to load transform XML using XSLT


"""
import win32com.client



def load_xml_file( filename ) :
    dom = win32com.client.Dispatch("Microsoft.XMLDOM")
    dom.validateOnParse = 0
    dom.load( filename )
    if ( dom.parseError() != 0 ) :
        err = Exception( "XML Parse Error" )
        raise err
    return dom


if ( __name__ == '__main__' ) :	

    dom = load_xml_file( input_xml )
    print dom.xml









