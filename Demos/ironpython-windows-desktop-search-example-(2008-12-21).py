# use with IronPython 2.0
#
# Based on these sources
#
#   Microsoft TechNet: "Seek and Ye Shall Find : Scripting Windows Desktop Search 3."0
#     http://www.microsoft.com/technet/scriptcenter/topics/desktop/wdsearch.mspx
#
#   Microsoft Windows Search 3.x SDK
#     http://www.microsoft.com/downloads/details.aspx?FamilyID=645300ae-5e7a-4ce7-95f0-49793f8f76e8&DisplayLang=en

import clr
import System

clr.AddReference("System.Data")
import System.Data

wds_connection_string = "Provider=Search.CollatorDSO;Extended Properties='Application=Windows';"
wds_query = "SELECT System.FileName FROM SYSTEMINDEX"
wds_connection = System.Data.OleDb.OleDbConnection( wds_connection_string )
wds_connection.Open( )
wds_command =  System.Data.OleDb.OleDbCommand( wds_query , wds_connection)
wds_results = wds_command.ExecuteReader()

max_results_to_show=20
n=0
while ( wds_results.Read() ) :
    print wds_results.GetString(0)
    n +=1
    if ( n>= max_results_to_show) : break

wds_results.Close()
wds_connection.Close()


