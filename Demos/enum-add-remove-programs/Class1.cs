using System;
using System.Collections;
using con=System.Console;

namespace enumarp
{
	class Class1
	{

		public static void enumapps()
		{
			System.DateTime now = System.DateTime.Now;

			con.WriteLine( "");
			con.WriteLine( "");
			con.WriteLine( "Installed Applications (ARP) at {0}",now);

			string p1 = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall" ;

			Microsoft.Win32.RegistryKey hk_uninstall = Microsoft.Win32.Registry.LocalMachine.OpenSubKey( p1 );

			System.Collections.ArrayList apps = new ArrayList();

			string [] subkeys = hk_uninstall.GetSubKeyNames();

			foreach (string subkey in subkeys)
			{
				con.WriteLine( "");
				con.WriteLine( "{0}",subkey);
				Microsoft.Win32.RegistryKey hk_app = hk_uninstall.OpenSubKey( subkey );
				string [] value_names = hk_app.GetValueNames();
				foreach (string value_name in value_names)
				{
					object val = hk_app.GetValue( value_name, null );
					if ( val==null ) { val="<no value>"; }
					string type_string = val.GetType().Name;
					if ( val.GetType()==typeof(string) ) { type_string="string";}
					else if ( val.GetType()==typeof(System.Int32) ) { type_string="int32";}

					con.WriteLine( "  {0} = ({1}) \"{2}\"",value_name,type_string,val);
				}
				hk_app.Close();

			}

			hk_uninstall.Close();


			con.WriteLine( "");
			con.WriteLine( "");
			con.WriteLine( "Environment Variables at {0}",now);

			System.Collections.IDictionary env = System.Environment.GetEnvironmentVariables();
			foreach (System.Collections.DictionaryEntry entry in env)
			{
				Console.WriteLine("  {0} = \"{1}\"", entry.Key, entry.Value);

			}

			
		}
		[STAThread]
		static void Main(string[] args)
		{

			enumapps();

		}
	}
}
