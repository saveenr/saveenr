using System.Security.Principal;

namespace Isotope.Security
{
    public class AdminAccountUtil
    {
        private const int LOGON32_LOGON_INTERACTIVE = 2;
        private const int LOGON32_PROVIDER_DEFAULT = 0;

        private System.Security.Principal.WindowsImpersonationContext m_impersonation_context;

        public enum IsAdminstratorResult
        {
            ACCOUNT_INVALID,
            ACCOUNT_VALID_IS_ADMIN,
            ACCOUNT_VALID_IS_NOT_ADMIN
        } ;

        public static bool IsAdministrator()
        {
            System.AppDomain.CurrentDomain.SetPrincipalPolicy(System.Security.Principal.PrincipalPolicy.WindowsPrincipal);
            var principal =
                (System.Security.Principal.WindowsPrincipal) System.Threading.Thread.CurrentPrincipal;
            //  not used: System.Security.Principal.WindowsIdentity identity = (System.Security.Principal.WindowsIdentity) principal.Identity;
            bool isadmin = principal.IsInRole(System.Security.Principal.WindowsBuiltInRole.Administrator);
            return isadmin;
        }

        public IsAdminstratorResult IsAdministrator(string domain, string username, string password)
        {
            if (impersonate_valid_user(username, domain, password))
            {
                bool is_admin;
                var the_identity = WindowsIdentity.GetCurrent();
                //using ( the_identity )
                //{
                var the_principal = new WindowsPrincipal(the_identity);
                is_admin = the_principal.IsInRole(WindowsBuiltInRole.Administrator);
                //}

                undo_impersonation();
                if (is_admin)
                {
                    return IsAdminstratorResult.ACCOUNT_VALID_IS_ADMIN;
                }
                else
                {
                    return IsAdminstratorResult.ACCOUNT_VALID_IS_NOT_ADMIN;
                }
            }
            else
            {
                return IsAdminstratorResult.ACCOUNT_INVALID;
            }
        }

        protected bool impersonate_valid_user(string userName, string domain, string password)
        {
            WindowsIdentity tempWindowsIdentity;
            System.IntPtr token = System.IntPtr.Zero;
            System.IntPtr tokenDuplicate = System.IntPtr.Zero;

            if (NativeMethods.RevertToSelf())
            {
                if (NativeMethods.LogonUserA(userName,
                                             domain,
                                             password,
                                             LOGON32_LOGON_INTERACTIVE,
                                             LOGON32_PROVIDER_DEFAULT,
                                             ref token
                        )
                    !=
                    0)
                {
                    if (NativeMethods.DuplicateToken(token, 2, ref tokenDuplicate) != 0)
                    {
                        tempWindowsIdentity = new WindowsIdentity(tokenDuplicate);
                        m_impersonation_context = tempWindowsIdentity.Impersonate();
                        if (m_impersonation_context != null)
                        {
                            NativeMethods.CloseHandle(token);
                            NativeMethods.CloseHandle(tokenDuplicate);
                            return true;
                        }
                    }
                }
            }
            if (token != System.IntPtr.Zero)
            {
                NativeMethods.CloseHandle(token);
            }
            if (tokenDuplicate != System.IntPtr.Zero)
            {
                NativeMethods.CloseHandle(tokenDuplicate);
            }
            return false;
        }

        protected void undo_impersonation()
        {
            m_impersonation_context.Undo();
        }
    }
}