

import os
import pprint
import sys
from commonpy import libfs

class SizeD :

    def __init__( self , sizes ) :
        self.dic = {}
        for size in sizes :
            self.dic[ size ] = 0
            
        self.min = min(sizes)
        self.max = max(sizes)

    def GetKeyForItem( self, item ) :
        if ( item < self.min ) : return "UNDER"
        if ( item > self.max) : return "OVER"
        else: return item

    def Add( self, item ) :
        k = self.GetKeyForItem( item )
        val = self.dic.get( item , 0 )
        val += 1
        self.dic[ item ] = val
        
files,dirs,links = libfs.listdir_ex( "c:\\", 1 , 0 )

names = files + dirs

print "Number of entries", len(names)
print "ratio of files to directories", len(files) / len(dirs)




sd = SizeD( range(256) )
maxname=0
total_name_length=0
for i in files + dirs :
    total_name_length += len(i)
    sd.Add( len(i) )
    if (len(i) > maxname) : manname = len(i)
    
print "Total characters used in names", total_name_length
print "Total K used in names", total_name_length/1024
print "Average No. characters in name", total_name_length/ (len(files) + len(dirs))
print "MaxName", maxname



pprint.pprint( sd.dic )




cum_per = 0
cum=0
fp = open("data.csv","w")
fp.write( "size" + "," + "count" + "\n")

keys = sd.dic.keys()
keys.sort()

for k in keys:
    v = sd.dic[ k ]
    cum += v 
    cum_per = cum * 1.0 / len(names) 
    fp.write( str(k) + "," + str(v)  + "," + str(cum_per) + "\n" )
fp.close()
