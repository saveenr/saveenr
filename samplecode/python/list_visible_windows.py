"""

"""

import win32gui
import win32con

def list_visible_hwnds() :

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

    hwnds = []
    win32gui.EnumWindows(callback, hwnds )
    windows = []
    for hwnd in hwnds :
        visible = win32gui.IsWindowVisible(hwnd)
        hwndtext = win32gui.GetWindowText( hwnd )
        hwndclass = win32gui.GetClassName( hwnd )
        hwndparent = getparent( hwnd )
        hwndowner = getowner( hwnd )
        wl = win32gui.GetWindowLong( hwnd, win32con.GWL_EXSTYLE )
        
        isapp = win32con.WS_EX_APPWINDOW & wl

        if ( ( not visible ) or ( hwndtext == "" ) or ( hwndparent != 0  ) ): continue 
        if ( not isapp and ( not ( 256 & wl ) )) : continue

        print '%08x "%s" "%s"' %  (hwnd , hwndclass, hwndtext )
        



if (__name__=='__main__') :

    list_visible_hwnds()


