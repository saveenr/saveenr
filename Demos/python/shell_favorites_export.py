import os
import sys
import win32com.client
import fnmatch
import ConfigParser

def cmd_favorites_export( out_filename ) :

        sh = win32com.client.Dispatch("WScript.Shell")
        path = sh.SpecialFolders("Favorites")

        if ( not os.path.exists( path ) ) :
                print "Favorites path %s does not exist" % path
                sys.exit()

        assert( os.path.exists(path) )


        """
        Get the list of .url files
        """
        favs = []
        for (path,dirs,files) in os.walk( path ) :
                files = [ os.path.join( path, f ) for f in files if fnmatch.fnmatch( f , "*.url" ) ]
                favs.extend( files )


        """
        Double-check that these are files
        """

        for fname in favs:
                assert ( os.path.isfile( fname ) )

        print "Found %d Favorites" % len(favs)
        
        """
        Get the data needed for each file
        """
        
        fav_info = []
        for fav in favs :
                config = ConfigParser.ConfigParser()
                config.readfp( open( fav ) )
                url = config.get( "InternetShortcut" , 'url')
                name = os.path.basename( fav )
                name = os.path.splitext( name )[0]
                fav_info.append( ( fav , name, url) )

        assert( len(favs) == len(fav_info) )

        """
        Start putting the data into theoutput filename
        """

        print "Writing to %s" % out_filename

        fp = file( out_filename, 'w' )

        import xml.sax.saxutils

        g = xml.sax.saxutils.XMLGenerator( fp , 'utf-8')
        #g.startDocument() Commented out to avoid IE complaining about this being "XML"
        g.startElement( 'html' , {} )
        g.characters( "\n")

        g.startElement( 'head' , {} )
        g.endElement( 'head' )
        g.characters( "\n")

        g.startElement( 'body' , {} )
        g.characters( "\n\t")

        for fi in fav_info :
                g.characters( "\n\t")
                g.startElement( 'a' , { 'href': fi[2]} )
                g.characters( fi[1] )
                g.endElement( 'a' )
                g.characters( "\n\t")
                g.startElement( 'br' , {} )
                g.endElement( 'br' )
        g.characters( "\n")

        g.endElement( 'body' )
        g.characters( "\n")


        g.endElement( 'html' )
        #g.endDocument()

        fp.close()


        assert( os.path.isfile( out_filename ) )        

        print "Done."

def show_syntax_error() :
        syntax = """
        Syntax:
        favorites_export.py <filename>
        """
        print syntax
        sys.exit(-1)

def main() :
        """
        Handle the command-line stuff
        """

        if ( len(sys.argv) <2 ) : show_syntax_error()
        filename = sys.argv[1]
        cmd_favorites_export( filename )


if (__name__=='__main__') :

        main()
