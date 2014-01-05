using System.Runtime.InteropServices;

namespace Isotope.Interop
{
    public static partial class NativeMethods
    {
        [DllImport("oleaut32.dll")]
        public static extern int RegisterActiveObject
            ([MarshalAs(UnmanagedType.IUnknown)] object punk,
             ref System.Guid rclsid, uint dwFlags, out int pdwRegister);
    }
}