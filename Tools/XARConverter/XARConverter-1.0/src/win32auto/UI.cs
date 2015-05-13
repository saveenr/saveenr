#define TRACE

using System;
using System.Collections;
using System.Runtime.InteropServices ;
using System.Text;

namespace WindowsAutomation
{

	public class UI
	{

		
		public static string Window_GetWindowText( int h)
		{

			int len = win32.GetWindowTextLength(  h );

			int buflen = len+1;
			StringBuilder sb = new StringBuilder(len+1);

			win32.GetWindowText( h, sb, buflen );

			return sb.ToString() ;
		}



		public static int ComboBox_GetCount( int hwnd )
		{
			int v = win32.SendMessage( hwnd, (int) win32.constants.CB_GETCOUNT, 0, 0 );
			return v;
		}


		public static string ComboBox_GetItemText( int hwnd , int n)
		{
			int len = win32.SendMessage( hwnd, (int) win32.constants.CB_GETLBTEXTLEN , n, 0 )+2;
			StringBuilder sb = new StringBuilder(len+1);
			int lResult = win32.SendMessage( hwnd, (int) win32.constants.CB_GETLBTEXT, n, sb );
			if (lResult==(int) win32.constants.CB_ERR)
			{
				throw new Errors.AutomationError("FAILED");
			}
			return sb.ToString();
		}


		public static int Window_GetRoot ( int hwnd )
		{
			int owner_hwnd = win32.GetAncestor( hwnd, (int) win32.constants.GA_ROOT );
			return owner_hwnd;
		}


		public static int Window_GetRootOwner( int hwnd )
		{
			int owner_hwnd = win32.GetAncestor( hwnd, (int) win32.constants.GA_ROOTOWNER );
			return owner_hwnd;
		}

		public static void Window_ActivateNormal( int hwnd )
		{
			win32.SetForegroundWindow( hwnd );
			win32.ShowWindow( hwnd, (int) win32.constants.SW_SHOWNORMAL );
		}

		public static void Window_ShowMaximized( int hwnd )
		{
			win32.SetForegroundWindow( hwnd );
			win32.ShowWindow( hwnd, (int) win32.constants.SW_SHOWMAXIMIZED  );
		}


		
		public static void Window_ShowRestore( int hwnd )
		{
			win32.ShowWindow( hwnd, (int) win32.constants.SW_RESTORE );
		}


		public static int ComboBox_SelectItem( int hwnd , int control_id, int index)
		{
			int result;
			result = win32.SendMessage(	hwnd,        // handle to destination window 
				(int) win32.constants.CB_SETCURSEL,       // message to send
				index,    // item index
				0 // not used; must be zero
				);

			uint lparam = (uint) win32.constants.CBN_SELENDOK <<16 | (uint) control_id ;
			result = win32.SendMessage( win32.GetParent( hwnd),        // handle to destination window 
				(int) win32.constants.WM_COMMAND,       // message to send
				(int) lparam ,    // command, item index
				hwnd // not used; must be zero
				);


			if (result==(int) win32.constants.CB_ERR)
			{
				throw new Errors.AutomationError("FAILED2");
			}
			return result;
		}



		public static  int GetControlID( int hwnd )
		{
			//convert to C#
			int val = win32.GetWindowLong( hwnd, (int) win32.constants.GWL_ID );
			if (val==0)
			{
				// throw some error;
			}
			return val;

		}

		public static void Window_SetTextRaw( int hwnd, string desired_text )
		{

			win32.SendMessage(hwnd, (int) win32.constants.WM_SETTEXT, 0, desired_text);

		}
		public static string Window_GetTextRaw( int hwnd )
		{
			int len = win32.SendMessage( hwnd, (int) win32.constants.WM_GETTEXTLENGTH, 0, 0);
			StringBuilder sb = new StringBuilder(len+1);
			win32.SendMessage( hwnd, (int) win32.constants.WM_GETTEXT, len+1, sb);

			return sb.ToString() ;
		}

		public static int Window_FindChildWindow( int parent , string wind_class, string title )
		{
			int h = (int) win32.FindWindowEx( parent, 0, wind_class, title );
			return h;
		}

		public static string Menu_GetMenuItemString(int h , int pos)
		{

			int len = win32.GetMenuString( h, pos, 0 , 0 , (int) win32.constants.MF_BYPOSITION )+1;

			StringBuilder sb = new StringBuilder( len+1 );

			int result = win32.GetMenuString( h, pos, sb , len, (int) win32.constants.MF_BYPOSITION );
			if (result==0)
			{
				// QUESTION: When I throw an exception here the app doesn't work
				//throw new Errors.AutomationError("FAILED3");
			}

			return sb.ToString() ;
		}

		public static menuinfo Menu_GetMenuItem( int h, int n )
		{
			//convert to C#
			menuinfo mi = new menuinfo();
			mi.text = Menu_GetMenuItemString( h, n );
			mi.id = win32.GetMenuItemID( h, n );
			return mi;

		}

		public static void Mouse_LeftClick( int hwnd )
		{
			// Originally we used SendMessage instead of PostMessage
			// but it seemed that PostMessage was more reliable
			// WORKITEM: Was this just due to timing between the calls?
			win32.PostMessage( hwnd,	(int) win32.constants.WM_LBUTTONDOWN, 0, 0);
			win32.PostMessage( hwnd,	(int) win32.constants.WM_LBUTTONUP, 0, 0) ;
            System.Threading.Thread.Sleep(250);
        }


        public static System.Collections.Generic.List<int> Window_GetTopLevelWindows()
        {
			WindowEnumCollector collector = new WindowEnumCollector(); 
			WindowsAutomation.win32.EnumWindowCallBack myCallBack = new WindowsAutomation.win32.EnumWindowCallBack( collector.WindowEnumCallback );
			WindowsAutomation.win32.EnumWindows( myCallBack, 0 );
			return ( collector.hwnds ) ;
		}


        public static System.Collections.Generic.List<int> Window_GetDirectChildWindows(int parent_hwnd)
        {
			WindowEnumCollectorWithParent collector = new WindowEnumCollectorWithParent( parent_hwnd ); 
			WindowsAutomation.win32.EnumWindowCallBack myCallBack = new WindowsAutomation.win32.EnumWindowCallBack( collector.WindowEnumCallback );
			WindowsAutomation.win32.EnumChildWindows( parent_hwnd,  myCallBack , 0);
			return ( collector.hwnds ) ;
		}

        public static System.Collections.Generic.List<int> Window_GetChildWindows(int parent_hwnd)
        {
			WindowEnumCollector collector = new WindowEnumCollector(); 
			WindowsAutomation.win32.EnumWindowCallBack myCallBack = new WindowsAutomation.win32.EnumWindowCallBack( collector.WindowEnumCallback );
			WindowsAutomation.win32.EnumChildWindows( parent_hwnd,  myCallBack , 0);
			return ( collector.hwnds ) ;
		}


		public static System.Collections.Generic.List<int> Window_FindChildWindowsEx(int parent, int owner_id, string wind_class, string wind_text, string wind_text_raw, int control_id)
		{
			WindowCollectorEx collector = new WindowCollectorEx(parent, 0, wind_class, wind_text, wind_text_raw, 0);
			WindowsAutomation.win32.EnumWindowCallBack myCallBack = new WindowsAutomation.win32.EnumWindowCallBack(collector.WindowEnumCallback);
			WindowsAutomation.win32.EnumChildWindows(parent, myCallBack, 0);

			return collector.hwnds;

		}



		public static int Window_FindChildWindowEx( int parent , int owner_id, string wind_class, string wind_text, string wind_text_raw, int control_id )
		{
			WindowCollectorEx collector = new WindowCollectorEx( parent, 0, wind_class, wind_text, wind_text_raw, 0 ); 
			WindowsAutomation.win32.EnumWindowCallBack myCallBack = new WindowsAutomation.win32.EnumWindowCallBack( collector.WindowEnumCallback );
			WindowsAutomation.win32.EnumChildWindows( parent, myCallBack , 0);

			if ( collector.hwnds.Count>0 )
			{
				return (int) collector.hwnds[0];
			}
			else
			{
				return 0;
			}
		}

		public static int Window_GetChild( int hwnd, string wind_class, string wind_text )
		{
			return Window_GetChildEx( hwnd, 0, wind_class, wind_text, null , 0);
		}

		public static int Window_GetChildEx( int hwnd, int owner_id, string wind_class, string wind_text, string wind_text_raw, int control_id )
		{
			int child = Window_FindChildWindowEx( hwnd, owner_id, wind_class, wind_text, wind_text_raw, control_id );
			if (child==0)
			{
                throw new Errors.FindError("Could not find child");
			}
			return child ;
		}


		public static string Window_GetWindowClass( int hwnd )
		{
			System.Text.StringBuilder sb = new System.Text.StringBuilder(256);
			win32.RealGetWindowClass( hwnd, sb, (uint) sb.Capacity-1 );
			string wc = sb.ToString();

			return wc;
		}

		public static void Window_SetAsForegroundWindow( int hwnd )
		{
			WindowsAutomation.win32.SetForegroundWindow(hwnd);

			int timeout = 3 * 1000;
			bool err=true;
			Timing.StopWatch w1 = new Timing.StopWatch();
			w1.Start();
			while (w1.ElapsedMiliseconds < timeout )
			{
				if ( WindowsAutomation.win32.GetForegroundWindow() == hwnd )
				{
					err=false;
					break;
				}
				System.Threading.Thread.Sleep(250);
			}

			if (err)
			{
					throw new Errors.AutomationError( "Did not set FG window");
			}
		}

		public static void ComboBox_SelectItem(int hwnd, int index)
		{
			int m_control_id = WindowsAutomation.UI.GetControlID( hwnd );
			WindowsAutomation.UI.ComboBox_SelectItem( hwnd , m_control_id , index );
		}

		public static void SetTextRaw( int hwnd, string new_caption)
		{
			
			WindowsAutomation.UI.Window_SetTextRaw( hwnd , new_caption);

			TestWindowText wait_client = new TestWindowText( hwnd , new_caption);
			int timeout = 10 * 1000 ; // 10 seconds
			int interval = 500; // .5 second;
			int elapsed;
			bool success = WindowsAutomation.Timing.WaitObject.WaitForCondition( wait_client, timeout, interval, out elapsed);
			if (!success)
			{
				throw new Errors.AutomationError( "Timed out setting text");
			}
		}



		public static string normalizetext( string s )
		{
			s = s.ToLower();
			s = s.Replace( "&", "" );
			return s;
		}

		public static menuinfo Menu_FindMenuItemID( int hwnd, string m1, string m2 )
		{

			int pmenu = WindowsAutomation.win32.GetMenu( hwnd  );
			Tracing.WriteLine( "pmenu = {0}", pmenu );

			int pmenucount = WindowsAutomation.win32.GetMenuItemCount(pmenu);

			for (int i=0;i<pmenucount;i++)
			{
				string mt = WindowsAutomation.UI.Menu_GetMenuItemString( pmenu, i );
				mt = normalizetext( mt );
				m1 = normalizetext( m1 );

				if ( mt == m1 )
				{
					int submenu = WindowsAutomation.win32.GetSubMenu( pmenu, i );
					int sub_menucount = WindowsAutomation.win32.GetMenuItemCount( submenu );
		
					for ( int j=0;j<sub_menucount;j++ )
					{
						WindowsAutomation.menuinfo mi =WindowsAutomation.UI.Menu_GetMenuItem( submenu, j );
						mi.text = normalizetext( mi.text );
						if ( mi.text.StartsWith( normalizetext(m2) ) )
						{
							return mi;
						}
					}
				}

			}
			return null;

		}

		public static void Menu_Select( int hwnd, string m1, string m2 )
		{
			Tracing.WriteLine( "Menu_Select \"{0}/{1}\"", m1, m2 );
			menuinfo mi = WindowsAutomation.UI.Menu_FindMenuItemID( hwnd , m1, m2 );
			if ( mi == null)
			{
                throw new Errors.FindError( string.Format( "Couldn't find {0} {1}",m1,m2 ) );
			}
			Tracing.WriteLine( "Menu_Select menu id = {0}", mi.id );

			win32.PostMessage(  hwnd, (int) win32.constants.WM_COMMAND,  mi.id, 0 );

		}

		public static void ComboBox_SelectItem( int the_combobox, string s)
		{
			Tracing.WriteLine( "ComboBox_SelectItem \"{0}\"", s);
			
			int format_selection_index = -1;


			int num_formats = WindowsAutomation.UI.ComboBox_GetCount( the_combobox );
			Tracing.WriteLine( "Num items in combobox: {0}", num_formats );
			for (int i=0;i<num_formats;i++)
			{
				string cbt = WindowsAutomation.UI.ComboBox_GetItemText( the_combobox, i );
				cbt = cbt.ToLower();
				if (cbt.LastIndexOf(s)>=0)
				{
					Tracing.WriteLine( "ComboBox_SelectItem found \"{0}\" in \"{1}\"at index {1}", s, cbt, i);
					format_selection_index = i;
					break;
				}
			}


			if (format_selection_index <0)
			{
				throw new WindowsAutomation.Errors.AutomationError("Not a valid selection index");
			}
			UI.ComboBox_SelectItem( the_combobox, format_selection_index );

		}

	}


				
	class TestWindowText: WindowsAutomation.Timing.IWaitClient
	{
		string m_text;
		int m_hwnd;

		public TestWindowText( int hwnd, string text )
		{
			this.m_hwnd =hwnd;
			this.m_text= text;
		}

		public bool StopWaiting( )
		{
			return( this.m_text == WindowsAutomation.UI.Window_GetTextRaw( this.m_hwnd ) );
		}

		public void WaitCallback( WindowsAutomation.Timing.WaitObject.WaitState state, int elapsed )
		{
			// do nothing
		}
	}

}
