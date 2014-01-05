import sys
import os
import clr
import System
clr.AddReferenceByPartialName('System.Xml')
import System.Xml
import re
import pprint
import optparse
import time

seps2 = System.Array[System.Char](" \t\n\r")
def split_hlinks(line) :
    line = line.ToLower()
    words = line.Split(seps2)
    hlinks = []
    nonhlinks = []
    for w in words :
        if ( w.startswith("http:" ) ) : hlinks.append(w)
        else: nonhlinks.append(w)
    return (hlinks, nonhlinks )
        


stopwords = set(
    ['1', 'before', 'these', 'on', 'him', '2', 'being', 'they', 'only', 'himself', '3', 
    'between', 'this', 'or', 'his', '4', 'both', 'those', 'other', 'how', '5', 'but',
    'through', 'our', 'if', '6', 'by', 'to', 'out', 'in', '7', 'came', 'too',
    'over', 'into', '8', 'can', 'under', 're', 'is', '9', 'come', 'up', 'said', 'it',
    '0', 'could', 'use', 'same', 'its', 'about', 'did', 'very', 'see', 'just',
    'after', 'do', 'want', 'should', 'like', 'all', 'does', 'was', 'since', 'make',
    'also', 'each', 'way', 'so', 'many', 'an', 'else', 'we', 'some', 'me', 'and', 'for' ,'well', 'still', 'might', 'another', 'from', 'were', 'such', 'more', 'any', 'get',
    'what', 'take', 'most', 'are', 'got', 'when', 'than', 'much', 'as', 'has',
    'where', 'that', 'must', 'at', 'had', 'which', 'the', 'my', 'be', 'he', 'while',
    'their', 'never', '$', 'have', 'who', 'them', 'no', 'because', 'her', 'will',
    'then', 'now', 'been', 'here', 'with', 'there', 'of', 'would', 'you', 'your'] )


def get_tweets( term, resultsperpage, page , lang ) :
    query = r"http://search.twitter.com/search.atom?q=%s&rpp=%s&page=%s" % (term,resultsperpage,page)
    if ( lang != None ) :
        query += "&lang=%s" % lang
    print "Q",query

    content = System.Net.WebClient().DownloadString(query)

    dom= System.Xml.XmlDocument()
    nsmgr = System.Xml.XmlNamespaceManager( dom.NameTable)
    nsmgr.AddNamespace( "atom", "http://www.w3.org/2005/Atom") ; 

    dom.LoadXml( content )
    root = dom.DocumentElement

    entry_nodes = [ n for n in root.SelectNodes( "atom:entry" ,nsmgr) ]
    
    def get_prop( n , q) :
        return n.SelectSingleNode(q,nsmgr).InnerText
    
    tweets= [ ( get_prop(n,"atom:updated"),
               get_prop(n,"atom:author/atom:name") ,
               get_prop(n,"atom:title") )
                 for n in entry_nodes]
    return tweets

def save_tweets_to_text_file( term, numtweets, filename, pause, lang) :
    rpp = 100
    num_pages, xrem = divmod(numtweets,rpp )
    if (xrem>0) : num_pages+= 1
    #print num_pages, xrem

    count = 0;
    fp = System.IO.File.CreateText(filename)
    for p in xrange(num_pages) :
        if ( ( count >0 ) and (pause>0) ):
            print "pausing for %s seconds" % pause
            time.sleep(pause)
        print 'asking for %s items from page %s' % ( rpp, p+1 )
        tweets =  [ t[2] for t in get_tweets( term, rpp, p+1 , lang) ]
        print 'Number of tweets retrieved:', len(tweets)
        if (len(tweets)<rpp) : break
        for tweet in tweets:
            fp.WriteLine(tweet)
        count+=1
    fp.Close()
    
def main () :
    parser = optparse.OptionParser()
    parser.add_option('-t','--term', type="string", help="term to search for")
    parser.add_option('-c','--count', type="int", help="number of tweets you want")
    parser.add_option('-f','--force', action="store_true" , help="don't cache, always requery twitter")
    parser.add_option('-p','--pause', type="int" , help="seconds to pause before each page query")
    parser.add_option('-l','--language', type="string" , help="language code (en,de,...)")
    (options, args) = parser.parse_args()
    print options
    print args

    term=options.term
    numtweets = int( options.count )
    force = options.force
    
    print "Term to search for:", term
    print "Num tweets:", numtweets

    if (options.language=="*") : options.language = None
    
    outfilename = term +'-wordle.txt'

    seps = System.Array[System.Char](" \t\n\r-,.;!?+*~\"\'()[]&<>:#\\/$=")
    excludewords= set( )
    excludewords.add( term )
    excludewords.add( 'microsoft')

    filename = term +'-tweets.txt'
    
    if ( (not os.path.exists(filename)) or force ) :
        print "downloading from twitter"
        save_tweets_to_text_file( term, numtweets , filename , options.pause , options.language)

    fp = System.IO.File.OpenText(filename)

    allwords = []
    
    while (True):
        line = fp.ReadLine()
        if (line==None) : break

        line = line.ToLower()
        hlinks , words = split_hlinks( line )
        line = " ".join( words )
        words = line.Split( seps )
        #print hlinks, words
        words = [ w for w in words if len(w)>1] # exclude zero or single char strings
        words = [ w for w in words if (w not in excludewords ) ]
        words = [ w for w in words if (w not in stopwords ) ]
        words = [ w for w in words if w[0]!=u'@' ]

        allwords.extend( words  )
    text = u" ".join( allwords )

    print "Creating", outfilename
    fp = System.IO.File.CreateText(outfilename)
    for word in allwords:
        fp.WriteLine(word)
    fp.Close()


main()


