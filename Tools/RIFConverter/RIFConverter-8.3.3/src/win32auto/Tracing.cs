#define TRACE

using System;
using System.Text;
using System.Runtime.InteropServices;
using System.Diagnostics;


namespace WindowsAutomation
{
	public class Tracing
	{
		public static StackFrame GetStackFrame( int index )
		{
			StackTrace st = new StackTrace();
			StackFrame psf = st.GetFrame( index  );
			return psf;
		}

		public static void StartMethodTrace()
		{
			StackFrame psf = GetStackFrame( 2 );
			string method_name = GetMethodDisplayName( psf );

			Trace.WriteLine( method_name + "()" );
			Trace.WriteLine( "{" );
			Trace.Indent();
		}

		public static string GetMethodName(int index)
		{
			StackFrame psf = GetStackFrame( index+2  );
			string method_name = GetMethodDisplayName( psf );
			return method_name;
		}

			
		public static string GetMethodDisplayName( System.Diagnostics.StackFrame psf )
		{
			string method_name = psf.GetMethod().DeclaringType.Name  + "." + psf.GetMethod().Name;
			return method_name;
		}

		public static void EndMethodTrace()
		{
			Trace.Unindent();
			Trace.WriteLine( "}" );
		}

		public static void DumpWindow( int h )
		{
			StartMethodTrace();

			int root = WindowsAutomation.UI.Window_GetRoot( h);
			int rootowner = WindowsAutomation.UI.Window_GetRoot( h);

			Trace.WriteLine( "Handle: 0x"+ h.ToString("x8") );
			Trace.WriteLine( "Title: "+ WindowsAutomation.UI.Window_GetWindowText(h)  );
			Trace.WriteLine( "Class: "+ WindowsAutomation.UI.Window_GetWindowClass(h)  );
			Trace.WriteLine( "Parent: "+ WindowsAutomation.win32.GetParent(h)  );
			Trace.WriteLine( string.Format( "Root: 0x{0} \"{1}\" \"{2}\"", root.ToString("x8") , WindowsAutomation.UI.Window_GetWindowText(root), WindowsAutomation.UI.Window_GetWindowClass(root)  ) );
			Trace.WriteLine( string.Format( "RootOwner: 0x{0} \"{1}\" \"{2}\"", rootowner.ToString("x8"), WindowsAutomation.UI.Window_GetWindowText(rootowner), WindowsAutomation.UI.Window_GetWindowClass(rootowner)  ) );

			EndMethodTrace();
		}

		public static void DumpWindows(System.Collections.ArrayList windows)
		{
			StartMethodTrace();

			foreach (int hwnd in windows)
			{
				DumpWindow(hwnd);
			}

			EndMethodTrace();

		}

		public static void WriteLine(string s)
		{
			Trace.WriteLine( s );
		}

		public static void WriteLine(string s, params object [] items)
		{
			string msg = string.Format( s, items );
			Tracing.WriteLine( msg );

		}


	}
}

