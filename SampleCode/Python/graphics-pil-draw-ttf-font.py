
import os
import sys


print "Loads an image and draws a line and text"


import Image
import ImageDraw
import ImageFont

in_fname = "\\foo.jpg"
out_fname = "\\foo-with-text.png" # save as png so that the modfications are clearly seen

img = Image.open( in_fname )
draw = ImageDraw.Draw(img)
draw.line(  (0,0) + img.size , fill ="#ffffff" )
font1 = ImageFont.truetype( r'c:\windows\fonts\ariblk.ttf' , 100)
draw.text( (100,100), "Hello World" , font=font1)
img.save( out_fname ) 



