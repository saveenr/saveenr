using System.Runtime.InteropServices;

namespace Isotope.Interop
{
    public static partial class NativeMethods
    {
        [DllImport("Ole32.Dll")]
        public static extern int CreateBindCtx(int reserved,
                                               out System.Runtime.InteropServices.ComTypes.IBindCtx bind_ctx);
    }
}