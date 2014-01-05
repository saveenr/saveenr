"""

"""
import win32com.client


dom = win32com.client.Dispatch("Microsoft.XMLDOM")
root_el = dom.createElement( "foo" )
dom.appendChild( root_el )

assert( dom.documentElement.nodeName=="foo" )

bar_el = dom.createElement( "bar1" )
root_el.appendChild( bar_el )
bar_el.setAttribute( "beer" , "baz" )

comment = dom.createComment( "Hello World!" )
bar_el.appendChild( comment )

print dom.xml




