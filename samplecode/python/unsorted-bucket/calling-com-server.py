import win32com.client
 
o = win32com.client.Dispatch("Python.TestServer")
x = o.Hello( "FOO" )
print x
