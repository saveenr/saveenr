#define TRACE

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Diagnostics;
using System.Runtime.InteropServices;


/*

TO USE
- Your Form should inherit from the ApplicationBarForm 
- To set default location use this.DefaultEdge
- After your form's call to InitializeComponent(); then call this.Construction_ApplicationBar();

HISTORY


*/



namespace AppBar
{

		/*
		 * this appbar code comes from: http://www.codeproject.com/Purgatory/AppBar.asp
		 * it was written by Mad__ 
		 * */

	
	[StructLayout(LayoutKind.Sequential)]
	public struct RECT
	{
		public int left;
		public int top;
		public int right;
		public int bottom;
	}


	[StructLayout(LayoutKind.Sequential)]
	public struct APPBARDATA
	{
		public int cbSize;
		public IntPtr hWnd;
		public int uCallbackMessage;
		public int uEdge;
		public RECT rc;
		public IntPtr lParam;
	}

	public enum ABMsg : int
	{
		ABM_NEW=0,
		ABM_REMOVE=1,
		ABM_QUERYPOS=2,
		ABM_SETPOS=3,
		ABM_GETSTATE=4,
		ABM_GETTASKBARPOS=5,
		ABM_ACTIVATE=6,
		ABM_GETAUTOHIDEBAR=7,
		ABM_SETAUTOHIDEBAR=8,
		ABM_WINDOWPOSCHANGED=9,
		ABM_SETSTATE=10
	}



	public enum ABNotify : int
	{
		ABN_STATECHANGE=0,
		ABN_POSCHANGED,
		ABN_FULLSCREENAPP,
		ABN_WINDOWARRANGE
	}


	public enum ABEdge : int
	{
		ABE_LEFT=0,
		ABE_TOP,
		ABE_RIGHT,
		ABE_BOTTOM
	}


	public class ApplicationBarForm : System.Windows.Forms.Form
	{
		/*
		 * this appbar code comes from: http://www.codeproject.com/Purgatory/AppBar.asp
		 * it was written by Mad__ 
		 * */


		protected int DefaultEdge = (int)ABEdge.ABE_LEFT;
		private bool fBarRegistered = false;

		[DllImport("SHELL32", CallingConvention=CallingConvention.StdCall)]
		static extern uint SHAppBarMessage(int dwMessage, ref APPBARDATA pData);
		[DllImport("USER32")]
		static extern int GetSystemMetrics(int Index);
		[DllImport("User32.dll", ExactSpelling=true, CharSet=System.Runtime.InteropServices.CharSet.Auto)]
		private static extern bool MoveWindow(IntPtr hWnd, int x, int y, int cx, int cy, bool repaint);
		[DllImport("User32.dll", CharSet=CharSet.Auto)]
		private static extern int RegisterWindowMessage(string msg);
		private int uCallBack;

		private void RegisterBar()
		{
			APPBARDATA abd = new APPBARDATA();
			abd.cbSize = Marshal.SizeOf(abd);
			abd.hWnd = this.Handle;
			if (!fBarRegistered)
			{
				uCallBack = RegisterWindowMessage("AppBarMessage");
				abd.uCallbackMessage = uCallBack;

				uint ret = SHAppBarMessage((int)ABMsg.ABM_NEW, ref abd);
				fBarRegistered = true;

				ABSetPos();
			}
			else
			{
				SHAppBarMessage((int)ABMsg.ABM_REMOVE, ref abd);
				fBarRegistered = false;
			}
		}

		private void ABSetPos()
		{
			APPBARDATA abd = new APPBARDATA();
			abd.cbSize = Marshal.SizeOf(abd);
			abd.hWnd = this.Handle;
			abd.uEdge = this.DefaultEdge;

			if (abd.uEdge == (int)ABEdge.ABE_LEFT || abd.uEdge == (int)ABEdge.ABE_RIGHT) 
			{
				abd.rc.top = 0;
				abd.rc.bottom = SystemInformation.PrimaryMonitorSize.Height;
				if (abd.uEdge == (int)ABEdge.ABE_LEFT) 
				{
					abd.rc.left = 0;
					abd.rc.right = Size.Width;
				}
				else 
				{
					abd.rc.right = SystemInformation.PrimaryMonitorSize.Width;
					abd.rc.left = abd.rc.right - Size.Width;
				}

			}
			else 
			{
				abd.rc.left = 0;
				abd.rc.right = SystemInformation.PrimaryMonitorSize.Width;
				if (abd.uEdge == (int)ABEdge.ABE_TOP) 
				{
					abd.rc.top = 0;
					abd.rc.bottom = Size.Height;
				}
				else 
				{
					abd.rc.bottom = SystemInformation.PrimaryMonitorSize.Height;
					abd.rc.top = abd.rc.bottom - Size.Height;
				}
			}

			// Query the system for an approved size and position. 
			SHAppBarMessage((int)ABMsg.ABM_QUERYPOS, ref abd); 

			// Adjust the rectangle, depending on the edge to which the 
			// appbar is anchored. 
			switch (abd.uEdge) 
			{ 
				case (int)ABEdge.ABE_LEFT: 
					abd.rc.right = abd.rc.left + Size.Width;
					break; 
				case (int)ABEdge.ABE_RIGHT: 
					abd.rc.left= abd.rc.right - Size.Width;
					break; 
				case (int)ABEdge.ABE_TOP: 
					abd.rc.bottom = abd.rc.top + Size.Height;
					break; 
				case (int)ABEdge.ABE_BOTTOM: 
					abd.rc.top = abd.rc.bottom - Size.Height;
					break; 
			}

			// Pass the final bounding rectangle to the system. 
			SHAppBarMessage((int)ABMsg.ABM_SETPOS, ref abd); 

			// Move and size the appbar so that it conforms to the 
			// bounding rectangle passed to the system. 
			MoveWindow(abd.hWnd, abd.rc.left, abd.rc.top, abd.rc.right - abd.rc.left, abd.rc.bottom - abd.rc.top, true); 
		}

		protected override void WndProc(ref System.Windows.Forms.Message m)
		{
			if (m.Msg == uCallBack)
			{
				switch(m.WParam.ToInt32())
				{
					case (int)ABNotify.ABN_POSCHANGED:
						ABSetPos();
						break;
				}
			}

			base.WndProc(ref m);
		}



		protected override System.Windows.Forms.CreateParams CreateParams
		{
			get
			{
				CreateParams cp = base.CreateParams;
				/*
				//cp.Style &= (~0x00C00000); // WS_CAPTION
				cp.Style &= (~0x00800000); // WS_BORDER
				*/
				cp.Style |= 0x00C00000; // WS_CAPTION
				cp.Style |= 0x00800000; // WS_BORDER
				cp.ExStyle = 0x00000080 | 0x00000008; // WS_EX_TOOLWINDOW | WS_EX_TOPMOST
				return cp;
			}
		}

		protected void OnLoad_ApplicationBar(object sender, System.EventArgs e)
		{
			RegisterBar();
		}

		protected void OnClosing_ApplicationBar(object sender, System.ComponentModel.CancelEventArgs e)
		{
			RegisterBar();
		}

		protected void Construction_ApplicationBar()
		{
			this.Load += new System.EventHandler(this.OnLoad_ApplicationBar);  // NEEDED FOR APPBAR
			this.Closing += new System.ComponentModel.CancelEventHandler(this.OnClosing_ApplicationBar); // NEEDED FOR APPBAR
		}


	}

}
