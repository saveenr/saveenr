from __future__ import nested_scopes
import os
import sys
import math

rho = 1

def gauss(x,y) :
        a = 1.0 / ( 2 * math.pi  * rho  * rho )
        c = -1.0 * ( x*x + y*y ) / (2 * rho * rho)
        b = math.pow( math.e, c )
        return a * b


xradius = 2
yradius = 2

def radiusrange( r ) :
        return range( -r, r + 1 )


values = []
items = []

for x in radiusrange(xradius) :
        for y in radiusrange(yradius) :
                value = gauss(x,y)
                items.append( (x,y,value) )
                values.append( value )
                print x,y,value,
        print

min_value = min(values)
print ">>", min_value

scale = 1.0/min_value
print scale

items2=[]
for x,y,v in items :
        items2.append( (x,y,v*scale) )

print items2

