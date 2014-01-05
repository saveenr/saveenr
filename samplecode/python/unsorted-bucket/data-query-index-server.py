from __future__ import generators

import os
import sys

import win32com.client

def get_connection_string( catalog ) :
    connection_string = "Provider=MSIDXS;Data Source=" + catalog
    return connection_string
    
def do_it( ) :
    connection_string = get_connection_string( 'website' )
    print connection_string
    
    con=win32com.client.Dispatch('ADODB.Connection')
    rs=win32com.client.Dispatch('ADODB.recordset')
    con.Open( connection_string )
    sql="""SELECT Filename FROM SCOPE(' DEEP TRAVERSAL OF  "g:\\public\\indexed-docs" ') """

    rs.Open( sql, con )

    for x in range(rs.Fields.Count):
        print rs.Fields.Item(x).Name

    rs.MoveFirst()
    count = 0
    while 1:
        if rs.EOF: break
        for x in range(rs.Fields.Count):
            print rs.Fields.Item(x).Value , " " ,
        print
        count += 1
        rs.MoveNext()
    
    con.Close()
    
    

    

if (__name__=='__main__') :
    do_it()
    
