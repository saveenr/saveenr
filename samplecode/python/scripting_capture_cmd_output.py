import os
import sys
import pprint

"""

Shows how to capture the output of a command line application as a series of text lines

History
-------
2004-12-29 Initial version

"""

def capture_lines_from_cmdline( cmdline ) :
    assert( cmdline != None )
    errcode =os.system( cmdline )
    fp = os.popen( cmdline )
    lines = fp.readlines()
    fp.close()
    return lines

if ( __name__ == '__main__' ) :
    cmdline = "dir"
    lines = capture_lines_from_cmdline( cmdline )
    pprint.pprint(lines)

