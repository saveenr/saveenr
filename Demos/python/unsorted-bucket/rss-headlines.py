
import os
import sys
import string
import urllib
import win32com.client
import pprint
import time

#feed_filename = sys.argv[1]
#dest_folder = sys.argv[2]

feed_filename = r"c:\pkgs\scripts\feeds.txt"
dest_folder = r"d:\inetpub\wwwroot\headlines"

fp = file( feed_filename , "r" )
lines = fp.readlines()
fp.close()


lines = map ( string.strip , lines )
lines = [ line for line in lines if len(line) ]


feed_urls = lines


def select_nodes( node, query ) :

    """
    Returns a python list that contains the results of the selectNodes query

    Example:
    nodes = select_nodes( node, "foo/bar" )
    
    """

    List = []
    nodes = node.selectNodes( query )

    child  = nodes.nextNode()
    while ( child ) :
        List.append( child )
        child = nodes.nextNode()

    return List

def get_query_text( node, query ) :

    """
    Finds the child identified by query and returns the text value
    """

    result = node.selectSingleNode( query )
    if (result!=None) :return result.text
    else :
        return None


def get_data_from_feed( feed_url ) :
    L = []
    dom = win32com.client.Dispatch( "Microsoft.XMLDOM" )
    dom.async = 0
    dom.load( feed_url  )
    if (dom.documentElement==None) :
        return None, None
    
    if ( dom.documentElement.nodeName == "rss" ) :
        items = select_nodes( dom, "//item" )
    elif ( dom.documentElement.nodeName[:3] == "rdf" ) :
        items = select_nodes( dom, "/*/item" )
        
    for item in items:
        link = get_query_text( item, "link" )
        title = get_query_text( item, "title" )
        description = get_query_text( item, "description" )
        
        L.append( (link,title,description) )
    channel_title = get_query_text( dom, "*/channel/title" )
    return channel_title, L

def get_feed_folder( channel_title , root_path ) :
    t = channel_title
    t = fix_chars( t )
    p = os.path.join( root_path, t )
    return p

def fix_chars( fn ) :    
    fn = string.replace( fn, "\"", "'" )
    fn = string.replace( fn, ":", "-" )
    fn = string.replace( fn, "/", "-" )
    fn = string.replace( fn, "\\", "-" )
    fn = string.replace( fn, "*", "-" )
    fn = string.replace( fn, "?", "-" )

    return fn


css = "http://pixel/default.css"

xfp = file( os.path.join( dest_folder , "headlines.html" ), "w" )
xfp.write( "<html>")         
xfp.write( "<head>")         
xfp.write( '<link rel="stylesheet" type="text/css" href="%s" />' % css )
xfp.write( "</head>")         
xfp.write( "<body>")         

for feed_url in feed_urls :
        
    channel_title, data, = get_data_from_feed(feed_url)

    if ( data == None ) :
        channel_title = "Failed to get data from " + feed_url
        data = []
        
    print channel_title 
    xfp.write( "<h3>%s</h3>" % (channel_title,) )
    xfp.write( "<p><a href=\"%s\">xml feed</a></p>" % (feed_url,) )         
    xfp.write( "<ul>")         
    
    for datum in data :
        link,title,description = datum
        xfp.write( "<li> <a href=\"%s\">%s</a>" % (link,title) )         
        xfp.write( "<br/>")         
    xfp.write( "</ul>")         


time_generated = time.strftime( "%A %Y-%m-%d %I:%M %p", time.localtime( time.time() ) )

xfp.write( "<p>Generated on %s</p>" % (time_generated,) )         

        
xfp.write( "</body>")         
xfp.write( "</html>")         
xfp.close()

    
    


    

