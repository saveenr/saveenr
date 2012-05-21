
import os
import sys


print "Loads an image and saves thumbnail"


import Image

in_fname = "\\foo.jpg"
out_fname = "\\foo-thumb.jpg"
thumbnail_size = (100,100)

img = Image.open( in_fname )
img.thumbnail( thumbnail_size )
img.save( out_fname ) 



