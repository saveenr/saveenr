#
# @file: rifconv.py
# @author: Saveen Reddy
#


#
# Imports
#

import os
import sys

import win32com.client
import win32api
import win32con

import fnmatch
import time

# ----------------------------------------
#
# HISTORY
#
# 03/06/2000 - Initial Version
#
# 03/06/2000 - Fixed bug on images that had a we layer ... trying
# to "Save As" when there was wet layer adding a warning dialog which
# threw everything off
#
# ----------------------------------------

# ----------------------------------------
#
# Issues/Workitems:
#
# - Assumes the app is running. No workaround.
#
# - Assumes Painter 6 is involved. Maybe will work on P5 and P5.5 but have not
# tried. 
#
#
# ----------------------------------------



#
# Print out welcome
#

print
print "RIFCONV"
print "Saveen Reddy"
print



# ----------------------------------------
#
# listallfiles( path, pattern ) 
#
# ----------------------------------------

def listallfiles( path, pattern ) :

	def f ( arg, path , names) :

		list = arg[0]
		pattern = arg[1]
		
		for name in names :
			abs_path = os.path.join( path, name )
			
			if ( os.path.isfile( abs_path ) ) :
				if (( pattern !=None) and(fnmatch.fnmatch( name, pattern )) ) :
					list.append( abs_path )
				
	list = []		
	os.path.walk( path, f, (list,pattern) )
	return list


# ----------------------------------------
#
# This is where the real work gets done
#
# ----------------------------------------


#
# Connect to the shell object
#

sh = win32com.client.Dispatch("WScript.Shell")

#
# Must put focus on the Painter App
#
# @workitem: should make this "Metacreations Painter 6" to be safe
#

sh.AppActivate("Corel Painter")

#
# Get the input and output paths
#
# @workitem: clean up error handling if user does not provide paths on
# the command line
#

input_path = sys.argv[1]
output_path = sys.argv[2]

#
# Find all the files inthe source directory
#

print "Checking files", input_path
input_files = listallfiles( input_path , "*.rif") 

#
# Loop through each file and convert
#

i = 0;
for input_file in input_files :

	print i+1, input_file

	#
	# Figure out output filename
	#

	output_file = input_file[ len(input_path) :]
	if (output_file[0] in [ "\\" , "/" ] ) :
		# this removes the trailing separator character if there is one
		output_file = output_file[1:]
	output_file = os.path.join( output_path, output_file )

	output_base, output_ext = os.path.splitext( output_file )
	output_ext = ".jpg"
	output_file = output_base + output_ext

	#
	# Make sure there is a path ready for the file
	#

	full_output_path , dummy = os.path.split( output_file )
	if ( not os.path.exists( full_output_path ) ) :
		os.makedirs( full_output_path, 0755 )


	print"  ", output_file

	#
	# Must delete output file if it already exists
	# This will void a dialog coming up
	#

	if (os.path.exists(output_file)):
		os.unlink( output_file )

	# FILE / OPEN
	sh.SendKeys("%fo")
	# GOTO NAME FIELD
	sh.SendKeys("%n")
	# TYPE INPUT FILENAME
	sh.SendKeys(input_file)
	# HIT THE OPEN BUTTON
	sh.SendKeys("%o")
	# WAIT A SEC	
	time.sleep(1)
	# DRY THE FILE
	sh.SendKeys("%nd")
	# WAIT FOR LOAD TO FINISH
	time.sleep(7)

	print "> Saving File"

	# FILE SAVE AS
	sh.SendKeys("%fa{ENTER}")
	# GOTO NAME FIELD
	sh.SendKeys("%n")
	# TYPE OUTPUT FILENAME
	sh.SendKeys(output_file)
	# SELECT SAVE BUTTON
	sh.SendKeys("%s")
	# PRESS SAVE BUTTON
	sh.SendKeys("{ENTER}")
	# WAIT FOR SAVE TO FINISH
	time.sleep(7)
	# CLOSE THE FILE
	sh.SendKeys("%fc")
	
	i = i +1

	#
	# @debug: this stops after one file
	#if (i>=1) : break

#
# We're done
#
print 
print "Finished."
