
import os
import sys
import mx.DateTime

def is_today_a_weekday( ) :
    """
    turns true right now is a weekday
    """
    the_time = time.localtime()
    daynum = the_time[ 6 ]
    return ( (daynum==5) or (daynum==6) )


def get_pretty_duration_string( duration ) :
    """
    Returns a nice string that describes the duration.
    duration is an mxDatTimeDelta object

    examples:
    "1 hour 23 min 5 sec"
    "4 days 5 min"    

    """
    L = []
    spacing = ' '
    if ( duration.day > 0 ) :
        s = 'day'
        if ( duration.day > 1 ) : s = 'days'
        L.append( str(int(duration.day)) + spacing + s )

    if ( duration.hour > 0 ) :
        s = 'hour'
        if ( duration.hour > 1 ) : s = 'hours'
        L.append( str(int(duration.hour)) + spacing + s )

    if ( duration.minute> 0 ) :
        s = 'min'
        L.append( str(int(duration.minute)) + spacing + s)

    if ( duration.second > 0 ) :
        s = 'sec'
        L.append( str(int(duration.second)) + spacing + s )

    sep = ' '
    duration_s = sep.join( L )
    
    return duration_s

