import sys
import win32security
import win32con
import win32api

"""

From this example:

    http://aspn.activestate.com/ASPN/docs/ActivePython/2.4/pywin32/Windows_NT_Security_.2d.2d_Impersonation.html

"""
class Impersonate:

    def __init__(self,domain,login,password):
        self.domain=domain
        self.login=login
        self.password=password
        self.is_impersonating = False

    def logon(self):

        self.handle=win32security.LogonUser(
            self.login,
            self.domain,
            self.password,
            win32con.LOGON32_LOGON_INTERACTIVE,
            win32con.LOGON32_PROVIDER_DEFAULT)
        win32security.ImpersonateLoggedOnUser(self.handle)

        self.is_impersonating = True

    def logoff(self):
        if (self.is_impersonating) :
            win32security.RevertToSelf() #terminates impersonation
            self.handle.Close() #guarantees cleanup

    def attemptlogon(self) :
        try :        
            self.logon() #become the user
            self.logoff() #return to normal
            return True
        except :
            return False

        
def tryuser(a,b,c) :
    print a,b,c
    a=Impersonate(a,b,c)
    result = a.attemptlogon()
    print ">", result
    
if ( __name__ == '__main__' ) :

    print win32api.GetUserName() #show you're someone else
    tryuser('domainX','AdministratorX','pass')
