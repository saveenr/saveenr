
import os
import sys
import string
import pprint
import time
import nntplib
import rfc822
import StringIO

def walk_articles_with_xhdr( s, group, header, func ) :
    response, count, first, last, name = s.group( group )

    delta = 100
    first = int(first)
    last = int(last)
    cur_last = int(last) 
    cur_first = cur_last - delta

    matching_articles = []

    now = time.time()
    continue_processing = 1
    while (continue_processing) :

        response, article_info_list = s.xhdr( header , str(cur_first) + "-" + str(cur_last) )


        for article_number, article_data in article_info_list :
            continue_processing = func( s, group, article_number, header, article_data )
            if (not continue_processing) : break

        if ( cur_first< first) :
            break

        cur_last = cur_first - 1
        cur_first = cur_last - delta
        
    return matching_articles




def get_articles_younger_than( s , group , timespan_in_seconds ) :

    matching_articles = []

    def my_func( s, group, article_number, header_name, header_data ) :
        the_time = rfc822.parsedate( header_data )
        time_in_seconds = time.mktime( the_time )
        dif = my_func.now - time_in_seconds
        if ( dif < timespan_in_seconds ) :
            x_response, x_number, article_id = s.stat( article_number )
            matching_articles.append( (article_number, article_id)  )
            return 1
        else :
            return 0

    my_func.now = time.time()

    walk_articles_with_xhdr( s, group, "date" , my_func )
        
    return matching_articles

def get_google_url_for_messageid( message_id ) :
    url = "http://groups.google.com/groups?oi=djq&as_umsgid=" + message_id
    return url

def get_headers_for_messageid( server, message_id ) :
    response, number, id, list = server.head( message_id )
    headers = string.join( list, "\n" ) + "\n\n"
    msg = rfc822.Message( StringIO.StringIO( headers ) )
    return msg    


def test() :
    s = nntplib.NNTP('newsvr')
    the_newsgroup = "comp.lang.python"
    now = time.time()
    two_days = 2 * 24 * 60 * 60
    article_info = get_articles_younger_than( s, the_newsgroup, two_days)

    for i in article_info     :
        #pprint.pprint( article_info )
        #pprint.pprint( s.head( article_info[0][0] ) )
        print get_google_url_for_messageid( i[1])
    
    s.quit()




