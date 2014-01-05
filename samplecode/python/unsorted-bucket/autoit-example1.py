
import os
import sys
import win32com.client


def test1() :
    shell.Run( "calc.exe", 1, False )


    autoit.WinWaitActive( "Calculator", "" )

    autoit.Send( "2*2=" )
    autoit.Send( "{ALT}E" )
    autoit.Sleep( 500 )
    autoit.Send( "C" )
    autoit.Sleep( 500 )
    autoit.Send( "42*4=" )
    autoit.Sleep( 500 )
    autoit.Send( "{ALT}E" )
    autoit.Sleep( 500 )
    autoit.Send( "P" )
    autoit.Sleep( 500 )
    
    autoit.WinClose( "Calc", "" )
    autoit.WinWaitClose( "Calc", "" )


if (__name__=="__main__") :
    test1()


