import os
import win32com.client

import sys

if (__name__=='__main__') :


	doc_filename = "c:\\test.doc"
	txt_filename = "c:\\test.txt"

	# Launch Word	
	#word = win32com.client.Dispatch( "Word.Application" )
	word = win32com.client.gencache.EnsureDispatch("Word.Application")
	#word = win32com.client.dynamic.Dispatch("Word.Application")
	word.Visible = 1

	# Activate the Word window
	word.Activate()


	# Create a new empty Document 
	doc = word.Documents.Add( Template="Normal", NewTemplate =False, DocumentType=0 )


	# Save the file
	doc.SaveAs( FileName = doc_filename )

	# Close the document
	doc.Close()

	# Reload the document
	doc = word.Documents.Open( doc_filename )	

	doc.SaveAs( txt_filename, FileFormat=win32com.client.constants.wdFormatTextLineBreaks)

	constant_names = [ s for s in dir(win32com.client.constants) if s.startswith("wd") ]
	for cn in constant_names :
		print cn