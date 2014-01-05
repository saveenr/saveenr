import os
import sys
import HTMLParser
import StringIO
import htmlentitydefs

html_data = """
<html>
<body>
    <a HREF="http://msdn.microsoft.com">msdn</a>    
    <A href="http://www.microsoft.com"> &lt;&gt;&amp;  microsoft </A>    
</body>
</html>
"""


class URLFinder(HTMLParser.HTMLParser):
    
    def __init__( self ) :
        HTMLParser.HTMLParser.__init__( self )
        self.links = []
        self.a_flag = False
        self.a_text = None
        self.current_url = None

    def handle_starttag(self, tag, attrs):
        self.a_text = ""
        if ( tag == 'a' ) :
            self.a_flag = True
            for name,value in attrs:
                if ( 'href' == name ) :
                    self.current_url = value
                    break

    def handle_data(self, data ):
        if ( self.a_flag ) :
            assert( self.a_text != None )
            self.a_text += data

    def handle_charref(self, charref):
        if ( self.a_flag ) :
            assert( self.a_text != None )
            self.a_text += "&#" + charref+ ";"

    def handle_entityref(self, entityref):
        if ( self.a_flag ) :
            assert( self.a_text != None )
            self.a_text += chr ( htmlentitydefs.name2codepoint[ entityref ] )


    def handle_endtag(self, tag ):
        if ( tag == 'a' ) :
            assert( self.a_flag == True )
            self.a_flag = False
            name = self.a_text
            self.links.append ( ( self.current_url , name))
            self.a_text = None
            self.current_url = None


def get_links_from_html_stream( fp ) :
    uf = URLFinder()
    uf.feed( fp.read() )         
    fp.close()                     
    uf.close()
    return uf.links


if ( __name__ == '__main__' ) :
    links = get_links_from_html_stream( StringIO.StringIO( html_data ) )
    print "%d links found" % len(links)
    for url, name in links :
        print url, "\"" + name.strip()+"\""