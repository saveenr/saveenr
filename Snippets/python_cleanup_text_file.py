import os
import sys
input_filename = r"D:\github\VisioAutomation\Reference\visio2007-masters.txt"
output_dir = r"D:\\"
output_filename = os.path.join(output_dir, os.path.split(input_filename)[1])

output_filename

sep = "|"

def condense( s, c) :
	c2 = c + c
	while ( c2 in s ) :
		s = s.replace( c2, c )
	return s

replace_list = [
				( "- ", "-"),
				( " -", "-"),
				( " /", "/"),
				( "/ ", "/") ] 

with open( output_filename, "w") as output_stream:
	with open(input_filename, "r") as input_stream:
		lines = ( s for s in input_stream )
		lines = ( s.strip() for s in lines )
		lines = ( s for s in lines if len(s)>0 )


		for line in lines:

			tokens = line.split( sep )
			tokens = [ t.strip() for t in tokens ]
			tokens = [ condense(t," ") for t in tokens ]
			new_line = sep.join( tokens )

			for (old,new) in replace_list:
				new_line = new_line.replace( old , new )

			new_line = "###PREFIX###" + sep + new_line

			output_stream.write( new_line + "\r\n" )