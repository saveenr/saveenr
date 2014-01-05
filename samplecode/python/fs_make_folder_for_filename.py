def makedirs_for_file( f ) :
    p = os.path.abspath( f )
    p = os.path.dirname( p )
    if ( not os.path.exists(p ) ) :
        os.makedirs(p)

        