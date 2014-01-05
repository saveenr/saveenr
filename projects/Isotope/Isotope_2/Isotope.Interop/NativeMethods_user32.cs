using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Isotope.Interop
{
    public delegate bool Win32Callback(IntPtr hwnd, IntPtr lParam);

    public delegate bool EnumWindowsProc(IntPtr hwnd, IntPtr lParam);

    public static partial class NativeMethods
    {
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool EnumWindows(EnumWindowsProc lpEnumFunc, IntPtr lParam);

        [DllImport("user32.dll", SetLastError = false)]
        public static extern IntPtr GetDesktopWindow();

        [DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Auto)]
        public static extern IntPtr GetParent(IntPtr hwnd);

        public static IntPtr GetParentSafe(IntPtr handle)
        {
            IntPtr result = GetParent(handle);
            if (result == IntPtr.Zero)
            {
                // An error occured
                throw new System.ComponentModel.Win32Exception(Marshal.GetLastWin32Error());
            }
            return result;
        }

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool EnumChildWindows(IntPtr hwndParent, EnumWindowsProc lpEnumFunc, IntPtr lParam);

        [DllImport("user32.Dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool EnumChildWindows(IntPtr parentHandle, Win32Callback callback, IntPtr lParam);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern int GetWindowTextLength(IntPtr hWnd);

        //[DllImport("user32.dll", SetLastError=true, CharSet=CharSet.Auto)]
        //static extern int GetWindowText(IntPtr hWnd, [Out] StringBuilder lpString, int nMaxCount);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int GetWindowText(IntPtr hWnd, System.Text.StringBuilder lpString, int nMaxCount);

        public static string GetWindowText(IntPtr hWnd)
        {
            // Allocate correct string length first
            int length = GetWindowTextLength(hWnd);
            var sb = new System.Text.StringBuilder(length + 1);
            GetWindowText(hWnd, sb, sb.Capacity);
            return sb.ToString();
        }

        [DllImport("user32.dll")]
        public static extern uint RealGetWindowClass(IntPtr hwnd, [Out] System.Text.StringBuilder pszType,
                                                     uint cchType);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool IsWindowVisible(IntPtr hWnd);

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool PostMessage(HandleRef hwnd, uint Msg, IntPtr wParam,
                                              IntPtr lParam);

        public static bool PostMessageSafe(HandleRef hWnd, uint msg, IntPtr wParam, IntPtr lParam)
        {
            bool returnValue = PostMessage(hWnd, msg, wParam, lParam);

            if (returnValue == false)
            {
                // An error occured
                throw new System.ComponentModel.Win32Exception(Marshal.GetLastWin32Error());
            }

            return returnValue;
        }

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetWindowRect(IntPtr hwnd, out RECT lpRect);

        public static RECT GetWindowRect(IntPtr hwnd)
        {
            var r = new RECT();
            GetWindowRect(hwnd, out r);
            return r;
        }

        [DllImport("user32.dll")]
        public static extern bool ShowWindow(IntPtr hwnd, int nCmdShow);

        public static string RealGetWindowClass(IntPtr hwnd)
        {
            var sb = new System.Text.StringBuilder(256);
            RealGetWindowClass(hwnd, sb, (uint) sb.Capacity - 1);
            string wc = sb.ToString();

            return wc;
        }

        public static IList<IntPtr> GetWindows()
        {
            var result = new List<IntPtr>();

            EnumWindows((h, lp) =>
                            {
                                result.Add(h);
                                return true;
                            }, IntPtr.Zero);
            return result;
        }

        public static IList<IntPtr> GetChildWindows(IntPtr parent)
        {
            var result = new List<IntPtr>();

            EnumWindowsProc callback = (h, lp) =>
                                           {
                                               result.Add(h);
                                               return true;
                                           };

            EnumChildWindows(parent, callback, IntPtr.Zero);
            return result;
        }
    }
}