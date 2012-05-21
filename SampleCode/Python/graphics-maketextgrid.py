import string
import random

def numeric_grid( cols, rows) :
    letters = string.digits

    L = []
    for y in xrange(rows) :
        row = []
        for x in xrange(cols) :
            row.append( letters [ random.randint( 0, len(letters)-1 ) ] )
        row = string.joinfields( row, "" )
        L.append( row )

    s = string.joinfields( L, "\n" )
    
    return s

def main() :

    s = numeric_grid(20,5)    

    fp = open("output.txt","w")
    fp.write(s)
    fp.close()
    




main()
