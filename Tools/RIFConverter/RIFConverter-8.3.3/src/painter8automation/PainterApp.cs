#define TRACE

using System;
using System.Diagnostics;

using WindowsAutomation;

namespace PainterAutomation
{

	public class PainterApp
	{

		private const string err_unexpected_dialog="Unexpected dialog";
		private const string err_window_not_found="Window not found";
		private const string err_dialogs_found="One or more Painter dialogs open";
		private const string err_dialog_not_found="Did not find dialog";
		private const string err_incorrect_document_count="Incorrect number of documents open";

		int m_painter;
		private Delegates.LogCallback m_log_cb;

		public int HWND
		{
			get
			{
				return m_painter;
			}
		}


		public PainterApp( Delegates.LogCallback logcb )
		{
			this.m_painter = PainterApp.GetPainter8Window();
			this.m_log_cb = logcb;

		}

		public void Activate()
		{
			WindowsAutomation.UI.Window_ShowMaximized( this.m_painter );
		}


		public static int FindPainter8Window()
		{
			return 	UI.Window_FindChildWindow(  win32.GetDesktopWindow() , WindowClassEnum.PainterMainWindowClass, WindowTextEnum.Painter8WindowText  );
		}

		public static int GetPainter8Window()
		{
			int h = PainterApp.FindPainter8Window();
			if (h==0)
			{
				throw new Errors.WindowNotFoundError(err_window_not_found);
			}
			return h;
		}
		
		

		public System.Collections.ArrayList GetDocumentWindows( )
		{
			System.Collections.ArrayList docs= new System.Collections.ArrayList();

			int mdiclient_hwnd = UI.Window_FindChildWindow( this.m_painter  , WindowClassEnum.MDIClientWindowClass, "" );
			foreach (int hwnd in UI.Window_GetDirectChildWindows(mdiclient_hwnd))
			{
				docs.Add( hwnd );
			}
			return docs;
		}

		public bool IsPainterDialog( int hwnd )
		{

			if ( WindowsAutomation.UI.Window_GetRootOwner(hwnd) == this.m_painter  )
			{
				string wclass = WindowsAutomation.UI.Window_GetWindowClass( hwnd );
				if ( wclass == WindowClassEnum.ASITHREEDWindowClass )
				{
					string wtext = WindowsAutomation.UI.Window_GetWindowText( hwnd ); 
					if (wtext!= WindowTextEnum.ColorSetWindowText  )
					{
						return true;
					}
				}
				else if (wclass == WindowClassEnum.DialogWindowClass)
				{
					return true;
				}
			}
			return false;
		}

		public System.Collections.ArrayList GetDialogWindows( )
		{
			Tracing.StartMethodTrace();
			System.Collections.ArrayList dialogs = new System.Collections.ArrayList();

			System.Collections.ArrayList dialogs1 = UI.Window_GetTopLevelWindows();
			System.Collections.ArrayList dialogs2 = UI.Window_GetChildWindows( this.m_painter  );
			
			foreach (int hwnd in dialogs1 )
			{
				if ( this.IsPainterDialog( hwnd ) )
				{
					string wclass = WindowsAutomation.UI.Window_GetWindowClass( hwnd );
					string title = WindowsAutomation.UI.Window_GetWindowText( hwnd );
					if ( (wclass=="_ASI_THREED_") && ( title=="" ) )
					{
						continue;
					}
					dialogs.Add( hwnd );
					Trace.WriteLine( "Found Painter Dialog" );
					Tracing.DumpWindow( hwnd );
				}
			}
			foreach (int hwnd in dialogs2 )
			{
				if ( this.IsPainterDialog( hwnd ) )
				{
					dialogs.Add( hwnd );
				}
			}
			Tracing.EndMethodTrace();
			return dialogs;
		}

		public bool WaitForDocumentCount( int desired_count )
		{
			WaitOnDocumentCount wait_client = new WaitOnDocumentCount ( desired_count , this);
			int timeout = 30 * 1000 ; // 30 seconds
			int interval = 500; // .5 second
			int elapsed;
			bool success;
			success = WindowsAutomation.Timing.WaitObject.WaitForCondition( wait_client, timeout, interval, out elapsed );
			return success;
		}


		public void VerifyAlive()
		{
			int desktop = WindowsAutomation.win32.GetDesktopWindow();
			System.Collections.ArrayList children = UI.Window_GetChildWindows( desktop );
			bool found=false;
			foreach ( int child in children )
			{
				if (child==this.HWND )
				{
					found=true;
					break;
				}
			}
			if ( !found)
			{
				throw new Errors.WindowNotFoundError( err_window_not_found );
			}

		}

		public void VerifyNoDialogs()
		{
			Tracing.StartMethodTrace();

			System.Collections.ArrayList dialogs = this.GetDialogWindows( );
			
			if ( dialogs.Count > 0 )
			{
				Tracing.DumpWindows( dialogs );
				throw new Errors.UnexpectedDialog( err_dialogs_found );
			}

			Tracing.EndMethodTrace();
		}

		public void VerifyDocumentCount( int count )
		{
			System.Collections.ArrayList docs= this.GetDocumentWindows(   );
			if ( docs.Count != count )
			{
				Tracing.DumpWindows( docs  );
				throw new Errors.PainterError( err_incorrect_document_count);
			}

		}

		public bool HandleLayerWarningDialog( )
		{
			int warning_dialog  = this.FindPainterDialog( WindowTextEnum.Painter8WindowText ); 
			if (warning_dialog!=0)
			{
				System.Collections.ArrayList children = UI.Window_GetDirectChildWindows( warning_dialog );
				int child = (int) children[2];
				if ( WindowsAutomation.UI.Window_GetWindowText(child).IndexOf( "layers" ) < 0 )
				{
					throw new Errors.UnexpectedDialog( PainterApp.err_unexpected_dialog );
				}

				int OK_button = UI.GetChild( warning_dialog, WindowClassEnum.ButtonWindowClass , WindowTextEnum.OKButtonWindowText );
				UI.LeftClick( OK_button );
				this.WaitForDialogToDisappear( warning_dialog  );

				return true;

			}
			return false;

		}


		public bool HandleExportOptions( string color_space )
		{
			int eo_dialog = this.FindPainterDialog( WindowTextEnum.ExportOptionsWindowText );
			if (eo_dialog!=0)
			{
				int rgb_button = UI.GetChild( eo_dialog, WindowClassEnum.ButtonWindowClass , WindowTextEnum.RGBButtonWindowText  );
				int cmyk_button = UI.GetChild( eo_dialog, WindowClassEnum.ButtonWindowClass , WindowTextEnum.CMYKButtonWindowText  );

				if ( color_space=="rgb")
				{
					// Do nothing since this is the defaut
				}
				else
				{
					UI.LeftClick( cmyk_button  );
					System.Threading.Thread.Sleep(250);
				}
				
				int OK_button  = UI.GetChild( eo_dialog, WindowClassEnum.ButtonWindowClass , WindowTextEnum.OKButtonWindowText  );
				UI.LeftClick( OK_button  );
				this.WaitForDialogToDisappear( eo_dialog );
	
				return true;
			}
			return false;

		}


		public int FindPainterDialog( string title )
		{
			Trace.WriteLine( string.Format( "FindPainterDialog \"{0}\"", title) );

			System.Collections.ArrayList dialogs = this.GetDialogWindows( );

			int dialog = 0;
			foreach (int hwnd in dialogs)
			{
				string this_dialogs_title = WindowsAutomation.UI.Window_GetWindowText( hwnd );

				if ( this_dialogs_title ==title)
				{
					int this_dialogs_root = WindowsAutomation.UI.Window_GetRootOwner( hwnd );
					if ( this_dialogs_root == this.m_painter  )
					{
						dialog=hwnd;
						break;
					}
				}
			}

			if ( dialog==0 )
			{
				Trace.WriteLine( string.Format( "FindPainterDialog \"{0}\" not found", title) );
			}
			return dialog;
		}



		public int GetPainterDialog( string title )
		{
			Trace.WriteLine( string.Format( "GetPainterDialog \"{0}\"", title) );


			int dialog = this.FindPainterDialog( title ) ;

			if ( dialog==0)
			{
				throw new Errors.UnexpectedDialog( PainterApp.err_unexpected_dialog );
			}

			Tracing.EndMethodTrace();
			
			return dialog;
		}

		public void FileSave( string output_file, bool save_alpha, string color_space, FormatDescription fmt)
		{
			Tracing.StartMethodTrace();

			this.VerifyAlive();
			this.VerifyNoDialogs();
			this.VerifyDocumentCount(1);

			WindowsAutomation.IO.VerifyFileDoesNotExist( output_file );

			WindowsAutomation.UI.Menu_Select( this.m_painter, "file", "save as..." );

			// We know the command succeeded if the SaveAs dialog is up

			int the_dialog = this.WaitForDialogToAppear( WindowTextEnum.SaveAsWindowText );
			this.Sleep(500);

			int the_combobox = UI.GetChildEx( the_dialog, 0, WindowClassEnum.ComboBoxWindowClass , null, WindowTextEnum.RIFFFilesWindowText  , 0) ;
			WindowsAutomation.UI.ComboBox_SelectItem( the_combobox, fmt.FilenameExtension );

			this.Sleep( 500 );

			int eb = UI.GetChild( the_dialog, WindowClassEnum.EditFieldWindowClass , null ) ;
			UI.SetTextRaw( eb, output_file );
			this.Sleep( 500 );

			if ( save_alpha == true )
			{
				int save_alpha_control = UI.GetChild( the_dialog, WindowClassEnum.ButtonWindowClass , WindowTextEnum.SaveAlphaWindowText ) ;
				System.Windows.Forms.SendKeys.SendWait( "+" ); // Use this key to always set the value regardless of its initial state
				this.Sleep( 500 );
			}

			int SAVE_button= UI.GetChild( the_dialog , WindowClassEnum.ButtonWindowClass, WindowTextEnum.SaveButtonWindowText );
			UI.LeftClick( SAVE_button );

			this.WaitForDialogToDisappear( the_dialog );

			// This section handles the Layer warning dialog and the tif export options dialog
			// The layer warning dialog only appears (when layers need to be collapsed)
			// The export options dialog appears every time

			this.Sleep(500);
			this.HandleLayerWarningDialog( );
			this.Sleep(500);
			this.HandleExportOptions( color_space );
			this.Sleep(500);
		
			this.WaitForFileAccess( output_file );
			
			this.VerifyAlive();
			this.VerifyNoDialogs();
			this.VerifyDocumentCount(1);

			Tracing.EndMethodTrace();
		}



		public void FileOpen( string input_file )
		{
			Tracing.StartMethodTrace();

		
			this.VerifyAlive();
			this.VerifyNoDialogs();
			this.VerifyDocumentCount(0);



			//this.SendMenuCommand(  MenuCommandEnum.cmd_file_open );
			WindowsAutomation.UI.Menu_Select( this.m_painter, "file", "open..." );


			int select_image_dialog = this.WaitForDialogToAppear( WindowTextEnum.SelectImageWindowText );
			this.Sleep(500);

			int eb = UI.GetChild( select_image_dialog, WindowClassEnum.EditFieldWindowClass , "" ) ;
			UI.SetTextRaw( eb , input_file );
			this.Sleep(500);

			int OPEN_button= UI.GetChild( select_image_dialog, WindowClassEnum.ButtonWindowClass, WindowTextEnum.OpenButtonWindowText );
			UI.LeftClick( OPEN_button  );

			this.WaitForDialogToDisappear( select_image_dialog );
			System.Threading.Thread.Sleep( 500 );

			this.WaitForFileAccess(  input_file );

			this.VerifyAlive();
			this.VerifyNoDialogs();

			// Wait until the document HWND becomes available (sometimes
			// it takes a while even if the I/O is done

			if ( false == this.WaitForDocumentCount( 1 ) )
			{
				string msg = string.Format( "Time-out waiting in File/Open {0}", input_file );
				throw new Errors.TimeOutError( msg );
			}
			
			this.Sleep(500);

			Tracing.EndMethodTrace();
		}


		public int WaitForFileAccess(string filename)
		{
			Tracing.StartMethodTrace();
			

			int time_waited=0;
			int callback_interval=500;
			int max_wait = 1000 * 60 * 2; // At most wait two minutes before giving up

			Tracing.WriteLine( "max time to wait: {0}", max_wait );

			bool er_status = WindowsAutomation.IO.WaitForExclusiveRead( filename, max_wait,  out time_waited , null , callback_interval);

			Tracing.WriteLine( "time elapsed: {0}", time_waited );
			Tracing.WriteLine( "err status: {0}", er_status );

			if (er_status==false)
			{
				Tracing.WriteLine( "Throwing timeout exception");
				string msg = string.Format( "Time-out in WaitForFileAccess on filename {0} ", filename );
				throw new Errors.TimeOutError( msg  );
			}
			
			Tracing.EndMethodTrace();
			return time_waited;
		}

		public int WaitForDialogToAppear(string t)
		{
			WaitForDialogToAppearObj wait_client = new WaitForDialogToAppearObj( this, t);
			int timeout = 5 * 1000 ; // 30 seconds
			int interval = 250; // .5 second
			int elapsed;
			bool success;

			
			Tracing.WriteLine( "max time to wait: {0}", timeout );

			success = WindowsAutomation.Timing.WaitObject.WaitForCondition( wait_client, timeout, interval, out elapsed );

			Tracing.WriteLine( "time elapsed: {0}", elapsed );
			Tracing.WriteLine( "success: {0}", success);

			if (!success)
			{
				Tracing.WriteLine( "Throwing timeout exception");
				string msg = string.Format( "Time-out in WaitForDialogToAppear for Dialog {0}", t );

				throw new Errors.TimeOutError( msg );
			}

			return wait_client.FoundDialog;
		}

		public void  WaitForDialogToDisappear(int dialog)
		{
			WaitForDialogToCloseObj wait_client = new WaitForDialogToCloseObj( this, dialog);
			int timeout = 5 * 1000 ; // 30 seconds
			int interval = 250; // .5 second
			int elapsed;
			bool success;

			Tracing.WriteLine( "max time to wait: {0}", timeout );
			
			success = WindowsAutomation.Timing.WaitObject.WaitForCondition( wait_client, timeout, interval, out elapsed );

			Tracing.WriteLine( "time elapsed: {0}", elapsed );
			Tracing.WriteLine( "success: {0}", success);

			if (!success)
			{
				Tracing.WriteLine( "Throwing timeout exception");
				string msg = string.Format( "Time-out in WaitForDialogToDisappear for Dialog {0}", dialog);
				throw new Errors.TimeOutError( msg );
			}
		}

		public void FileClose(  )
		{
			Tracing.StartMethodTrace();

			this.VerifyAlive();
			this.VerifyNoDialogs();
			this.VerifyDocumentCount(1);
	
			//this.SendMenuCommand( MenuCommandEnum.cmd_file_close );
			WindowsAutomation.UI.Menu_Select( this.m_painter, "file", "close" );

			
			if ( false == this.WaitForDocumentCount( 0 ) )
			{
				string msg = string.Format( "Time-out in file Close" );

				throw new Errors.TimeOutError( msg );
			}

			Tracing.EndMethodTrace();
		}

				
		public void Sleep(int milisec)
		{
			Tracing.WriteLine( "sleep {0} miliseconds", milisec );
			System.Threading.Thread.Sleep( milisec);
		}


	}

	class WaitForDialogToAppearObj: WindowsAutomation.Timing.IWaitClient
	{
		PainterApp m_app;
		string m_dialog_window_text;
		int m_found_dialog;

		public WaitForDialogToAppearObj( PainterApp app , string t)
		{
			this.m_app = app;
			this.m_dialog_window_text =t;
			this.m_found_dialog=0;
		}

		public bool StopWaiting( )
		{
			m_found_dialog = this.m_app.FindPainterDialog( this.m_dialog_window_text );
			return ( m_found_dialog!=0);
		}

		public void WaitCallback( WindowsAutomation.Timing.WaitObject.WaitState state, int elapsed )
		{
			// do nothing
		}

		public int FoundDialog
		{
			get
			{
				return this.m_found_dialog;
			}
		}
	}

	class WaitForDialogToCloseObj: WindowsAutomation.Timing.IWaitClient
	{
		PainterApp m_app;
		int m_ab;

		public WaitForDialogToCloseObj( PainterApp app , int ab)
		{
			this.m_app = app;
			this.m_ab=ab;
		}

		public bool StopWaiting( )
		{
			foreach (int h in m_app.GetDialogWindows() )
			{
				if (h==this.m_ab )
				{
					return false;
				}
			}
			return true;
		}

		public void WaitCallback( WindowsAutomation.Timing.WaitObject.WaitState state, int elapsed )
		{
			// do nothing
		}
	}

	class WaitOnDocumentCount : WindowsAutomation.Timing.IWaitClient
	{
		int m_desired_count;
		PainterApp m_app;
		System.Collections.ArrayList docs;

		public WaitOnDocumentCount( int desired_count, PainterApp app )
		{
			this.m_desired_count=desired_count;
			this.m_app = app;
			this.docs=null;
		}

		public bool StopWaiting( )
		{
			docs = this.m_app.GetDocumentWindows( );
			return ( docs.Count == this.m_desired_count );
		}

		public void WaitCallback( WindowsAutomation.Timing.WaitObject.WaitState state, int elapsed )
		{
			// do nothing
		}
	}



	public class WindowTextEnum
	{
		public const string Painter8WindowText= "Corel Painter 8";
		public const string ColorSetWindowText = "Color Set";
		public const string SelectImageWindowText = "Select Image";
		public const string SaveAsWindowText = "Save Image As";
		public const string OpenButtonWindowText = "&Open";
		public const string SaveButtonWindowText = "&Save";
		public const string ExportOptionsWindowText ="Export Options";
		public const string RGBButtonWindowText ="RGB";
		public const string CMYKButtonWindowText ="CMYK";
		public const string OKButtonWindowText ="OK";
		public const string SaveAlphaWindowText = "Save &Alpha";
		public const string RIFFFilesWindowText = "RIFF Files (*.RIF)";
	}

	public class WindowClassEnum
	{
		public const string PainterMainWindowClass = "ASIMainWndClass";
		public const string MDIClientWindowClass = "MDIClient";
		public const string DialogWindowClass = "#32770";
		public const string ASITHREEDWindowClass = "_ASI_THREED_";
		public const string EditFieldWindowClass = "Edit";
		public const string ButtonWindowClass = "Button";
		public const string ComboBoxWindowClass = "ComboBox";

	}

}
