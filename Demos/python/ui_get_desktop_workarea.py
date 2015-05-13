"""

Returns the usable region of a desktop (area not occluded by appbars)

"""
def get_desktop_workarea() :

    import ctypes

    class RECT(ctypes.Structure):
        _fields_ = [('left',ctypes.c_ulong),
            ('top',ctypes.c_ulong),
            ('right',ctypes.c_ulong),
            ('bottom',ctypes.c_ulong)]
    user32 = ctypes.windll.user32
    desktop = RECT()
    SPI_GETWORKAREA=48
    user32.SystemParametersInfoA(SPI_GETWORKAREA,0,ctypes.byref(desktop),0)
    workarea= [desktop.left, desktop.top, desktop.right, desktop.bottom]
    return workarea


if (__name__=='__main__') :

	workarea = get_desktop_workarea()
	print workarea