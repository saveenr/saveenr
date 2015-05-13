#define TRACE

using System;
using System.Text;
using System.Runtime.InteropServices;
using System.Diagnostics;

using win32api;

namespace PainterAutomation
{

	public class PainterUtil
	{
		public class MenuCommandEnum
		{
			public static readonly MenuCommand cmd_file_save_as = new MenuCommand("File/Save As",265,0);
			public static readonly MenuCommand cmd_file_open = new MenuCommand("File/Open",257,0);
			public static readonly MenuCommand cmd_file_close = new MenuCommand("File/CLose",259,0);
		}

		public class WindowTextEnum
		{
			public const string Painter7WindowText= "Painter 7";
			public const string ColorSetWindowText = "Color Set";
			public const string SelectImageWindowText = "Select Image";
			public const string SaveAsWindowText = "Save Image As";
			public const string OpenButtonWindowText = "&Open";
			public const string SaveButtonWindowText = "&Save";
					 
		}

		public class WindowClassEnum
		{
			public const string MDIClientWindowClass = "MDIClient";
			public const string DialogWindowClass = "#32770";
			public const string ASITHREEDWindowClass = "_ASI_THREED_";
			public const string EditFieldWindowClass = "Edit";
			public const string ButtonWindowClass = "Button";

		}


		public static int FindPainter7Window()
		{
			return libshell.FindDesktopWindowWithName( WindowTextEnum.Painter7WindowText );
		}

		
		public static void SubtractPainterWindowLeft( int hwnd, int pixels )
		{
			// TODO: Maximize the P7 window

			// Figure out how big the desktop is
			System.Drawing.Rectangle dr= libshell.GetDesktopDimensions( );

			libshell.MoveWindow( hwnd , pixels, 0 , (dr.Right - dr.Left) - pixels, (dr.Bottom-dr.Top), true );
		}
		

		public static System.Collections.ArrayList GetPainterDocumentWindows( int painter_hwnd )
		{
			System.Collections.ArrayList docs= new System.Collections.ArrayList();

			int mdiclient_hwnd = libshell.FindChildWindow( painter_hwnd, WindowClassEnum.MDIClientWindowClass, "" );
			foreach (int hwnd in libshell.GetDirectChildWindows(mdiclient_hwnd))
			{
				docs.Add( hwnd );
			}
			return docs;
		}

		public static System.Collections.ArrayList GetPainterDialogs( int painter_hwnd )
		{
			System.Collections.ArrayList dialogs = new System.Collections.ArrayList();

			foreach (int hwnd in libshell.GetChildWindows( libshell.GetDesktopWindow() ) )
			{
				string wclass = libshell.GetWindowClass( hwnd );
				string wtitle = libshell.GetWindowTextW( hwnd );
				if ( wclass == WindowClassEnum.ASITHREEDWindowClass )
				{
					if ( libshell.GetRootOwner(hwnd) == painter_hwnd )
					{
						// The Color Set shows up in this enumeration
						// But we don't consider it a dialog so
						// we ignore it

						if (wtitle!= WindowTextEnum.ColorSetWindowText  )
						{
							dialogs.Add( hwnd );
						}
					}
				}
				else if (wclass == WindowClassEnum.DialogWindowClass)
				{
					if ( libshell.GetRootOwner(hwnd) == painter_hwnd )
					{
						dialogs.Add( hwnd );
					}

				}

			}
			return dialogs;
		}

		class WaitOnDocumentCount : win32auto.IWaitClient
		{
			int m_hwnd;
			int m_desired_count;
			System.Collections.ArrayList docs;

			public WaitOnDocumentCount( int hwnd, int desired_count )
			{
				this.m_hwnd =hwnd;
				this.m_desired_count=desired_count;
				this.docs=null;
			}

			public bool StopWaiting( )
			{
				docs = PainterUtil.GetPainterDocumentWindows( this.m_hwnd  );
				return ( docs.Count == this.m_desired_count );
			}

			public void WaitCallback( win32auto.WaitObject.WaitState state, int elapsed )
			{
				// do nothing
			}
		}

		public static bool WaitForDocumentCount( int painter_hwnd, int desired_count )
		{
			WaitOnDocumentCount wait_client = new WaitOnDocumentCount ( painter_hwnd, desired_count );
			int timeout = 30 * 1000 ; // 30 seconds
			int interval = 500; // .5 second
			int elapsed;
			bool success;
			success = win32auto.WaitObject.WaitForCondition( wait_client, timeout, interval, out elapsed );
			return success;
		}

	}

}
