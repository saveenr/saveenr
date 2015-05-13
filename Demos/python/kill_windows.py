"""

    #Kills explorer windows that are browsing folders (for example "C:\" )


"""

import win32gui
import win32con
import win32api

def kill_top_hwnds_with_classname( target_classname) :

    def callback(hwnd, resultList):
        resultList.append((hwnd))

    hwnds = []
    win32gui.EnumWindows(callback, hwnds)

    hwnds = filter( win32gui.IsWindowVisible, hwnds )

    for hwnd in hwnds :

        hwndtext = win32gui.GetWindowText( hwnd )
        hwndclass = win32gui.GetClassName( hwnd )
        if ( hwndclass != target_classname ) :
            continue
        print hwnd, hwndclass, hwndtext
        win32api.PostMessage(hwnd, win32con.WM_CLOSE ,0, 0)

if (__name__=='__main__') :

    kill_top_hwnds_with_classname( "CabinetWClass" )