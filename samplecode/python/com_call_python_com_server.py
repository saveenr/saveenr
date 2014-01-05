import win32com.client

"""

This example shows how to call the COM object created in python.

NOTE: Actually this is no different than using python to use any other COM object

History
2004-12-29 Added better docstring
2002-11-18 Initial version

""" 
 
the_object = win32com.client.Dispatch("Python.PyTestCOMServer")
n = 12
n_squared = the_object.Square( n )
assert( n_squared == n * n )

print "value of %s squared is %s" %( n, n_squared )

print "Creator:", the_object.Creator

# If you try to set Creator as in the line below an exception will be thrown
# because the Creator object is marked as read-only
#
# the_object.Creator = "Me" 

# Now let's change the description attribute which is not marked as read-only
print "Description:", the_object.Description
the_object.Description = "Hello World"
print "New Description:", the_object.Description
