using System.Management.Automation;

namespace ImagingPS.Internal.Extensions
{
    public static class CmdletExtensions
    {
        public static string WriteVerbose(this Cmdlet cmdlet, string fmt, params object [] items)
        {
            string s = string.Format(fmt, items);
            cmdlet.WriteVerbose(s);
            return s;
        }
        
    }
}