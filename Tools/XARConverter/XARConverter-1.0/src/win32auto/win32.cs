using System;
using System.Collections;
using System.Runtime.InteropServices ;
using System.Text;

namespace WindowsAutomation
{

	public class menuinfo
	{
					
		public string text;
		public int id;

	};

	public class win32
	{
		public delegate bool EnumWindowCallBack(int hwnd, int lParam); 



		[DllImport("kernel32", SetLastError=true)]
		public static extern int CreateFile(
			string FileName,                    // file name
			uint DesiredAccess,                 // access mode
			uint ShareMode,                     // share mode
			uint SecurityAttributes,            // Security Attributes
			uint CreationDisposition,           // how to create
			uint FlagsAndAttributes,            // file attributes
			int hTemplateFile                   // handle to template file
			);
 
		[DllImport("kernel32")] public static extern int CloseHandle(int h); 

		[DllImport("user32")] public static extern int EnumWindows(EnumWindowCallBack x, int y); 
		[DllImport("user32")] public static extern int EnumChildWindows(int hWndParent, EnumWindowCallBack cb, int lParam);
		[DllImport("user32")] public static extern bool IsIconic(int hwnd); 

		[DllImport("user32")] public static extern int GetMenu(int hwnd); 
		[DllImport("user32")] public static extern int GetMenuItemID(int hMenu, int nPos);
		[DllImport("user32")] public static extern int GetMenuItemCount(int hMenu); 
		[DllImport("user32")] public static extern int GetSubMenu(int hMenu, int nPos);
		[DllImport("user32.dll")] public static extern uint RealGetWindowClass( int hwnd, [Out] StringBuilder pszType, uint cchType);
		[DllImport("user32")] public static extern int GetForegroundWindow();
		[DllImport("user32")] public static extern int SetForegroundWindow( int hwnd );
		[DllImport("user32")] public static extern int SetFocus( int hwnd );
		[DllImport("user32")] public static extern int GetParent( int hwnd );
		[DllImport("user32")] public static extern int GetAncestor( int hwnd , int p);
		[DllImport("user32")] public static extern int ShowWindow( int hwnd , int p);
		[DllImport("user32")] public static extern int GetDesktopWindow();
		[DllImport("user32")] public static extern int GetWindowTextLength( int hwnd );
		[DllImport("user32")] public static extern int GetWindowLong( int hwnd , int id);
		[DllImport("user32.dll")] public static extern int GetWindowText( int hWnd, [Out] StringBuilder lParam, int len);
		
		[DllImport("user32.dll")] public static extern int PostMessage( int  hWnd, int Msg, int wParam, int lParam);
		[DllImport("user32.dll")] public static extern int SendMessage( int  hWnd, int Msg, int wParam, int lParam);
		[DllImport("user32.dll")] public static extern int SendMessage( int hWnd, int Msg, int wParam, string lParam);
		[DllImport("user32.dll")] public static extern int SendMessage( int hWnd, int Msg, int wParam,[Out] StringBuilder lParam);


		[DllImport("user32")] public static extern int GetMenuString( int hwnd , int pos, int a, int b, int c );
		[DllImport("user32")] public static extern int GetMenuString( int hwnd , int pos, [Out] StringBuilder lParam, int b, int c);

		[DllImport("user32")] public static extern int FindWindowEx(int hWnd1, int hWnd2, string lpsz1, string lpsz2);

		public enum constants
		{
			SW_SHOWNORMAL=1,
			SW_SHOWMAXIMIZED=3,
			SW_RESTORE=9,
			GA_ROOT = 2,
			GA_ROOTOWNER = 3,
			CB_ERR = (-1),
			CB_GETCOUNT = 0x0146,
			CB_GETLBTEXT = 0x0148,
			CB_GETLBTEXTLEN = 0x0149,
			WM_COMMAND = 0x0111,
			WM_GETTEXTLENGTH = 0x000E,
			WM_SETTEXT = 0x000C,
			WM_GETTEXT = 0x000D,
			GWL_ID = -12,
			CB_SETCURSEL = 0x014E,
			CBN_SELENDOK= 9,
			MF_BYPOSITION = 0x0400,
			WM_LBUTTONDOWN  = 0x201,
			WM_LBUTTONUP = 0x202
		}





	}



	public class WindowEnumCollector
	{
        private System.Collections.Generic.List<int> list;

        public System.Collections.Generic.List<int> hwnds
        {
			get
			{
				return this.list;
			}
		}

		public WindowEnumCollector()
		{
			this.list = new System.Collections.Generic.List<int>();
			this.Reset();
		}

		public void Reset()
		{
			this.list.Clear();
		}

		public virtual bool WindowEnumCallback(int hwnd, int lParam) 
		{
			if ( this.CollectCondition( hwnd, lParam ) )
			{
				this.AddItem(hwnd);
			}
			return ( this.ContinueCondition( hwnd, lParam ) );
		}

		public void AddItem( int hwnd )
		{
			this.list.Add( hwnd );
		}

		public virtual bool CollectCondition( int hwnd, int lParam)
		{
			return true;
		}

		public virtual bool ContinueCondition( int hwnd, int lParam)
		{
			return true;
		}



	}


	public class WindowEnumCollectorWithParent: WindowEnumCollector
	{
		protected int m_parent_hwnd;
		public WindowEnumCollectorWithParent(int parent_hwnd) 
		{
			this.m_parent_hwnd = parent_hwnd;
		}

		public override bool CollectCondition( int hwnd, int lParam)
		{
			if ( WindowsAutomation.win32.GetParent(  hwnd ) == this.m_parent_hwnd )
			{
				return true;
			}
			else
			{
				return false;
			}
		}
	}


	public class WindowCollectorEx: WindowEnumCollector
	{
		protected int m_parent_hwnd;
		protected int m_owner_hwnd;
		protected string m_window_class;
		protected string m_window_text;
		protected string m_window_text_raw;
		protected int m_control_id;

		public WindowCollectorEx(int parent_hwnd, int owner_hwnd, string wind_class, string wind_text, string wind_text_raw, int control_id ) 
		{
			this.m_parent_hwnd = parent_hwnd;
			this.m_owner_hwnd = owner_hwnd;
			this.m_window_class = wind_class;
			this.m_window_text = wind_text;
			this.m_window_text_raw = wind_text_raw;
			this.m_control_id = control_id;
		}

		public override bool CollectCondition( int hwnd, int lParam)
		{
			bool found=false;

			//bool b1 = ( (this.m_parent_hwnd==(0)) || ( this.m_parent_hwnd == libwindow.GetParent( hwnd ) ));
			bool b2 = ( (this.m_owner_hwnd==(0)) || ( this.m_owner_hwnd  == WindowsAutomation.UI.Window_GetRootOwner( hwnd ) ));
			bool b3 = ( (this.m_window_class ==null) || ( this.m_window_class == WindowsAutomation.UI.Window_GetWindowClass ( hwnd ) ));
			bool b4 = ( (this.m_window_text ==null) || ( this.m_window_text == WindowsAutomation.UI.Window_GetWindowText( hwnd ) ));
			bool b5 = ( (this.m_window_text_raw ==null) || ( this.m_window_text_raw == WindowsAutomation.UI.Window_GetTextRaw( hwnd ) ));
			bool b6 = ( (this.m_control_id ==(0)) || ( this.m_control_id == WindowsAutomation.UI.GetControlID( hwnd ) ));

			found = b2 && b3 && b4 && b5 && b6;
			return found;
		}
	}



}
