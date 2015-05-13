"""


"""

import re

html_source = """
<html>
<head>
</head>
<body>
    <h1>
    Hello World
    </h1>
</body>
</html>

"""

def get_html_body_contents( data ) :
    pat1 = re.compile( "\<body", re.IGNORECASE )
    pat12 = re.compile( "\>", re.IGNORECASE )

    x = pat1.search( data )
    assert(x!=None)
    x = pat12.search( data, x.start() )
    assert(x!=None)

    pat2 = re.compile( "\<\/body", re.IGNORECASE )
    y = pat2.search( data )
    body = data[ x.end() : y.start() ]
    return body

if ( __name__=='__main__' ) :
    body_contents = get_html_body_contents( html_source )
    print body_contents
    