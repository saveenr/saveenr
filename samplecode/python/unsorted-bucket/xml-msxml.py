
import win32com.client

class XMLError :
	
	def __init__( self, value="XML Error", pe=None  ) :
		self.value = value
		self.pe = pe

	def __str__( self ) :
			return self.value 

	def ParseError( self ) :
		return self.pe

def check_parse_error( dom , msg ) :
	pe = dom.parseError()
	if (pe != 0 ) :
		e = XMLError( msg , pe )
		raise e

def new_dom( root_element_name=None ):

	"""
	Creates a new empty dom. If a name is provided for a root element then the dom will be created with that single element.

	Example:
	dom = new_dom( "root" )

	"""
	
	dom = win32com.client.Dispatch("Microsoft.XMLDOM")
	dom.validateOnParse = 0
	if (root_element_name!=None) :
		append_root_element( dom, root_element_name )
		
	return dom


def new_dom_from_string( xml_data ) :

	"""
	Loads a file from a string and returns a dom. This is synchronous.

	Example:
	dom = new_dom_from_string( "<root><foo>bar</foo></root>" )

	"""

	dom = new_dom()
	dom.async = 0
	dom.loadXML( xml_data )
	check_parse_error( dom , "failed to parse string") 
	return dom

def new_dom_from_file( url ) :
	"""
	Loads a file from URL and returns a dom
	This is synchronous

	Example:
	dom = new_dom_from_file( "bar.xml" )
	
	"""
	dom = new_dom()
	dom.async = 0
	dom.load( url )
	check_parse_error( dom , "failed to load file") 
	return dom

def append_root_element( node, name ) :

	"""
	Adds a root element to an empty DOM
	"""

	assert( node.ownerDocument == None )
	dom = node

	new_node = dom.createElement( name )
	node.appendChild( new_node )
	return new_node

def append_new_element( node, name , value=None) :

	"""
	Creates a new child for node and puts in a text value if provided

	node - the node
	name - name of new element to add
	value - text content of new element (None means there is no text)

	"""

	assert( node.ownerDocument != None )
	dom = node.ownerDocument

	new_node = dom.createElement( name )
	node.appendChild( new_node )

	if (value!=None) :
		append_new_text( new_node, value )
	
	return new_node


def append_new_text( node, text ) :

	"""
	node - the node
	text - the text to add at the end
	Adds appends text to a node
	"""

	new_text_node = node.ownerDocument.createTextNode( text )
	node.appendChild( new_text_node )
	return new_text_node


def xsl_transform_files( in_file, xsl_file, out_file ) :

	"""
	in_file - the source data filename
	xsl_file - the xsl filename
	out_file - the output filename

	Creates an output file based on the xsl transform of the source data
	in in_file transformed by the contents of xsl_file

	"""
	in_dom = new_dom_from_file( in_file )		
	xsl_dom = new_dom_from_file( xsl_file )
	data = in_dom.transformNode( xsl_dom )
	fp = open( out_file, "wb" )
	fp.write( data )
	fp.close()
	return 1


def get_query_text( node, query ) :

	"""
	Finds the child identified by query and returns the text value
	"""

	result = node.selectSingleNode( query )
	return result.text


def select_nodes( node, query ) :

	"""
	Returns a python list that contains the results of the selectNodes query

	Example:
	nodes = select_nodes( node, "foo/bar" )
	
	"""

	List = []
	nodes = node.selectNodes( query )

	child  = nodes.nextNode()
	while ( child ) :
		List.append( child )
		child = nodes.nextNode()

	return List

