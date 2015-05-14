using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DemoURLProtocolHandler
{
    class Program
    {
        // http://msdn.microsoft.com/en-us/library/aa767914(VS.85).aspx


        // to register or unregister - start cmd.exe as admin and use...
        // C:\> DemoUrlProtocolHandler.exe -register
        // C:\> DemoUrlProtocolHandler.exe -unregister

        // once registered, from the command line you can try the handler
        // C:\> start myprotocol:foo

        // or try it by clicking on a link in IE
        // via IE> myprotocol:foo%2Dbar

        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                string msg = string.Format("{0} No Args", MyURLProtocolHandler.Description);
                System.Windows.Forms.MessageBox.Show(msg);
                System.Environment.Exit(0);
            }


            if (args[0].Contains(":"))
            {
                string urlinput = args[0].Substring(MyURLProtocolHandler.ProtocolName.Length + 1);
                System.Windows.Forms.MessageBox.Show(urlinput);
                System.Windows.Forms.MessageBox.Show(string.Format("URL {0}",urlinput));
            }
            else
            {
                string cmd = args[0].ToLower();
                // probably not issued via url)
                if (cmd == "-register")
                {
                    ProtocolHandlerUtil.RegisterURLProtocol(MyURLProtocolHandler.ProtocolName, MyURLProtocolHandler.Description, typeof(Program).Assembly.Location);
                }
                else if (cmd == "-unregister")
                {
                    ProtocolHandlerUtil.UnregisterURLProtocol(MyURLProtocolHandler.ProtocolName);
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("Unknown command");
                }
            }
 
        }
    }


    public static class ProtocolHandlerUtil
    {
        public static void RegisterURLProtocol(string protocolname, string description, string binarypath)
        {
            var hkcr = Microsoft.Win32.Registry.ClassesRoot;
            
            var protocol_key = hkcr.CreateSubKey(protocolname);
            protocol_key.SetValue("", description);
            protocol_key.SetValue("URL Protocol", "");
            
            var command_key = protocol_key.CreateSubKey("shell\\open\\command");
            string cmdtext = string.Format("\"{0}\" \"%1\"", binarypath);
            command_key.SetValue("", cmdtext);
            protocol_key.Close();

            protocol_key.Close();
        }

        public static void UnregisterURLProtocol(string protocolname)
        {
            var hkcr = Microsoft.Win32.Registry.ClassesRoot;
            var k = hkcr.CreateSubKey(protocolname);
            if (k != null)
            {
                hkcr.DeleteSubKeyTree(protocolname);
            }
            else
            {
                // key already does not exist
            }
        }

    }

    public class MyURLProtocolHandler
    {
        public static string ProtocolName = "myprotocol";
        public static string Description = "My Protocol Handler";
    }

}
