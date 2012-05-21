
from __future__ import generators

import string


from OpenGL.GL import *
from OpenGL.GLUT import *
from OpenGL.GLU import *
import sys

# Some api in the chain is translating the keystrokes to this octal string
# so instead of saying: ESCAPE = 27, we use the following.
ESCAPE = '\033'

# Number of the glut window.
window = 0


# Rotation angle for the quadrilateral.
rquad = 0.0

# A general OpenGL initialization function.  Sets all of the initial parameters. 
def InitGL(Width, Height):				# We call this right after our OpenGL window is created.
    glClearColor(0.0, 0.0, 0.0, 0.0)	# This Will Clear The Background Color To Black
    glClearDepth(1.0)					# Enables Clearing Of The Depth Buffer
    glDepthFunc(GL_LESS)				# The Type Of Depth Test To Do
    glEnable(GL_DEPTH_TEST)				# Enables Depth Testing
    glShadeModel(GL_SMOOTH)				# Enables Smooth Color Shading
	
    glMatrixMode(GL_PROJECTION)
    glLoadIdentity()					# Reset The Projection Matrix
										# Calculate The Aspect Ratio Of The Window
    gluPerspective(45.0, float(Width)/float(Height), 0.1, 100.0)

    glMatrixMode(GL_MODELVIEW)

# The function called when our window is resized (which shouldn't happen if you enable fullscreen, below)
def ReSizeGLScene(Width, Height):
    if Height == 0:						# Prevent A Divide By Zero If The Window Is Too Small 
	    Height = 1

    glViewport(0, 0, Width, Height)		# Reset The Current Viewport And Perspective Transformation
    glMatrixMode(GL_PROJECTION)
    glLoadIdentity()
    gluPerspective(45.0, float(Width)/float(Height), 0.1, 100.0)
    glMatrixMode(GL_MODELVIEW)

# The main drawing function. 

front_bottom  = (0.2,0.2,0.7)
back_bottom  = (0.1,0.1,0.3)

front_top = (1.0,1.0,1.0)
back_top = (0.6,0.6,0.8)


def jiggle( c ) :
	import random
	c0= (c[0]+ random.random()) / 2.0
	c1= (c[1]+ random.random()) / 2.0
	c2= (c[2]+ random.random()) / 2.0
	return ( c0,c1,c2 )

colors = [ front_top ,  front_top, front_bottom, front_bottom, back_top ,  back_top, back_bottom , back_bottom]
colors = map( jiggle, colors )
colors[ 0 ] = (1.0,1.0,1.0)

def DrawGLScene():
	global rquad

	glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);	# Clear The Screen And The Depth Buffer
	glLoadIdentity();					# Reset The View
	glTranslatef(-3,0.0,-6.0);				# Move Left And Into The Screen


	glLoadIdentity();
	glTranslatef(1.5,0.0,-10.0);		# Move Right And Into The Screen
	glRotatef(rquad,0.0,0.5,0.0);		# Rotate The Cube On X, Y & Z

	def draw_face( points , colors ) :
		for p,c in zip( points, colors ) :
			glColor3f( *c )	
			glVertex3f( *p )


	def draw_quad_box( (f1,f2,f3,f4,b1,b2,b3,b4) , (fc1,fc2,fc3,fc4,bc1,bc2,bc3,bc4) ) :

		glBegin(GL_QUADS);			# Start Drawing The Cube
		draw_face( (f1,f2,f3,f4) , (fc1,fc2,fc3,fc4))
		draw_face( (f3,f4,b4,b3) , (fc3,fc4,bc4,bc3))
		draw_face( (b1,b2,b3,b4) , (bc1,bc2,bc3,bc4))
		draw_face( (f2,f3,b3,b2) , (fc2,fc3,bc3,bc2))
		draw_face( (f1,f4,b4,b1) , (fc1,fc4,bc4,bc1))
		draw_face( (f1,f2,b2,b1) , (fc1,fc2,bc2,bc1))
		glEnd();				# Done Drawing The Quad


	def draw_cube( (x,y,z), (w,h,d) ) :

		points = [ None ] * 8
		points[0] = ( x+w, y+h, z)
		points[1] = ( x, y+h, z)
		points[2] = ( x,y, z)
		points[3] = ( x+w,y, z)
		points[4] = ( x+w, y+h, z+d)
		points[5] = ( x, y+h, z+d)
		points[6] = ( x,y, z+d)
		points[7] = ( x+w,y, z+d)

		global colors
		
		draw_quad_box( points , colors )

		#bottom = [ points[2], points[3], points[7], points[6] ]
		#bottom = map( 


	def enum( seq ) :
		count =0
		for i in seq :
			yield i, count
			count += 1
			
	x0 = 0.0
	values = [1,2,5,6,3,2]
	for v,index in enum(values):

		draw_cube( (x0 - (index*1.0),-1.0,1.0), (0.5,0.5 * v,-1.0) )
	



	rquad = rquad - 0.15                 # Decrease The Rotation Variable For The Quad




	#  since this is double buffered, swap the buffers to display what just got drawn. 
	glutSwapBuffers()

# The function called whenever a key is pressed. Note the use of Python tuples to pass in: (key, x, y)  
def keyPressed(*args):
	# If escape is pressed, kill everything.
    if args[0] == ESCAPE:
	    sys.exit()

def main():
	global window
	glutInit(sys.argv)

	# Select type of Display mode:   
	#  Double buffer 
	#  RGBA color
	# Alpha components supported 
	# Depth buffer
	glutInitDisplayMode(GLUT_RGBA | GLUT_DOUBLE | GLUT_ALPHA | GLUT_DEPTH)
	
	# get a 640 x 480 window 
	glutInitWindowSize(640, 480)
	
	# the window starts at the upper left corner of the screen 
	glutInitWindowPosition(0, 0)
	
	# Okay, like the C version we retain the window id to use when closing, but for those of you new
	# to Python (like myself), remember this assignment would make the variable local and not global
	# if it weren't for the global declaration at the start of main.
	window = glutCreateWindow("Jeff Molofee's GL Code Tutorial ... NeHe '99")

   	# Register the drawing function with glut, BUT in Python land, at least using PyOpenGL, we need to
	# set the function pointer and invoke a function to actually register the callback, otherwise it
	# would be very much like the C version of the code.	
	glutDisplayFunc(DrawGLScene)
	
	# Uncomment this line to get full screen.
	# glutFullScreen()

	# When we are doing nothing, redraw the scene.
	glutIdleFunc(DrawGLScene)
	
	# Register the function called when our window is resized.
	glutReshapeFunc(ReSizeGLScene)
	
	# Register the function called when the keyboard is pressed.  
	glutKeyboardFunc(keyPressed)

	# Initialize our window. 
	InitGL(640, 480)

	# Start Event Processing Engine	
	glutMainLoop()

# Print message to console, and kick off the main to get it rolling.
print "Hit ESC key to quit."
main()
    	
