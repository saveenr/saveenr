"""

Returns the usable region of a desktop (area not occluded by appbars)

"""

import win32gui

def list_hwnds() :

    def callback(hwnd, resultList):
        resultList.append((hwnd))

    def getparent( hwnd ) :
        parenthwnd = 0
        try :
            parenthwnd = win32gui.GetParent( hwnd )
        except :
            pass
        return parenthwnd


    def getowner( hwnd ) :
        owner = 0
        try :
            owner = win32gui.GetWindow( hwnd , win32con.GW_OWNER )
        except :
            pass
        return owner

    block_classes = [ 'Static' , 'tooltips_class32' , 'ToolbarWindow32' , 'WorkerW' ]

    show_owned_windows_only_under_parent = True
    show_non_visible_windows = False

    hwnds = []
    win32gui.EnumChildWindows(win32gui.GetDesktopWindow(), callback, hwnds)

    dic = {}
    for hwnd in hwnds:
        parenthwnd = getparent( hwnd )
        if ( parenthwnd not in dic ) : dic[ parenthwnd ] = []
        dic[ parenthwnd ].append( hwnd )

    def dump( window, depth ) :
        if ( not show_non_visible_windows ) :
            visible = win32gui.IsWindowVisible(hwnd)
            if ( not visible ) : return

        spacing = '  '
        t = spacing * depth
        classname = win32gui.GetClassName( window )
        wt = win32gui.GetWindowText( window )

        owner = getowner( window )

        if ( classname in block_classes ) :
            return

        if ( show_owned_windows_only_under_parent ) :
            if ( (depth == 0) and ( owner!=0 ) ) :
                return 

        print '%s %08x "%s" "%s" %08x ' % (t, window ,classname, wt, owner )
        if ( window in dic ) :
            for childhwnd in dic[window] :
                dump( childhwnd, depth+1 )

    topwindows = []
    win32gui.EnumWindows( callback, topwindows)
    for hwnd in topwindows :
        dump( hwnd , 0 )


if (__name__=='__main__') :

	list_hwnds()