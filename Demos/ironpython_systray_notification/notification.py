#
# from: http://andrzejonsoftware.blogspot.com/2007/01/iron-python-on-balloons.html
#

import clr

clr.AddReference("System.Windows.Forms")
clr.AddReference("System.Drawing")
from System.Drawing import Icon
from System.Windows.Forms import (Application, ContextMenu, 
                                  MenuItem, NotifyIcon, Timer)


class Main(object):

    def __init__(self):
        self.initNotifyIcon()
        timer = Timer()
        timer.Interval = 6000
        timer.Tick += self.onTick
        timer.Start()
    

    def initNotifyIcon(self):
        self.notifyIcon = NotifyIcon()
        self.notifyIcon.Icon = Icon("test.ico")
        self.notifyIcon.Visible = True
        self.notifyIcon.ContextMenu = self.initContextMenu()

    
    def onTick(self, sender, event):
        self.notifyIcon.BalloonTipTitle = "Hello, I'm IronPython"
        self.notifyIcon.BalloonTipText = "Who are you?"
        self.notifyIcon.ShowBalloonTip(1000)

        
    def initContextMenu(self):
        contextMenu = ContextMenu()
        exitMenuItem = MenuItem("Exit")
        exitMenuItem.Click += self.onExit
        contextMenu.MenuItems.Add(exitMenuItem)
        return contextMenu
        

    def onExit(self, sender, event):
        self.notifyIcon.Visible = False
        Application.Exit()     
   
        
if __name__ == "__main__":
    main = Main()
    Application.Run()
