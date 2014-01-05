using System.Runtime.InteropServices;

namespace Isotope.Security
{
    public static class NativeMethods
    {
        [DllImport("advapi32.dll")]
        public static extern int LogonUserA(string lpszUserName,
                                            string lpszDomain,
                                            string lpszPassword,
                                            int dwLogonType,
                                            int dwLogonProvider,
                                            ref System.IntPtr phToken
            );

        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int DuplicateToken(System.IntPtr hToken,
                                                int impersonationLevel,
                                                ref System.IntPtr hNewToken
            );

        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool RevertToSelf();

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern bool CloseHandle(System.IntPtr handle);
    }
}