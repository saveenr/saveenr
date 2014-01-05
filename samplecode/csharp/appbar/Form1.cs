using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Runtime.InteropServices;

namespace appbar1
{


		/// <summary>
		/// Summary description for Form1.
		/// </summary>
		public class MainForm : System.Windows.Forms.Form
		{
			/// <summary>
			/// Required designer variable.
			/// </summary>
			private System.ComponentModel.Container components = null;

			public MainForm()
			{
				//
				// Required for Windows Form Designer support
				//
				InitializeComponent();

				//
				// TODO: Add any constructor code after InitializeComponent call
				//
			}

			/// <summary>
			/// Clean up any resources being used.
			/// </summary>
			protected override void Dispose( bool disposing )
			{
				if( disposing )
				{
					if (components != null) 
					{
						components.Dispose();
					}
				}
				base.Dispose( disposing );
			}

			#region Windows Form Designer generated code
			/// <summary>
			/// Required method for Designer support - do not modify
			/// the contents of this method with the code editor.
			/// </summary>
			private void InitializeComponent()
			{
				this.label1 = new System.Windows.Forms.Label();
				this.buttonQuit = new System.Windows.Forms.Button();
				this.SuspendLayout();
				// 
				// label1
				// 
				this.label1.Location = new System.Drawing.Point(40, 32);
				this.label1.Name = "label1";
				this.label1.TabIndex = 0;
				this.label1.Text = "label1";
				// 
				// buttonQuit
				// 
				this.buttonQuit.Location = new System.Drawing.Point(184, 104);
				this.buttonQuit.Name = "buttonQuit";
				this.buttonQuit.TabIndex = 1;
				this.buttonQuit.Text = "Quit";
				this.buttonQuit.Click += new System.EventHandler(this.buttonQuit_Click);
				// 
				// MainForm
				// 
				this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
				this.ClientSize = new System.Drawing.Size(274, 682);
				this.Controls.Add(this.buttonQuit);
				this.Controls.Add(this.label1);
				this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
				this.Name = "MainForm";
				this.Text = "AppBar";
				this.Closing += new System.ComponentModel.CancelEventHandler(this.OnClosing);
				this.Load += new System.EventHandler(this.OnLoad);
				this.ResumeLayout(false);

			}
			#endregion

			private System.Windows.Forms.Label label1;
			private System.Windows.Forms.Button buttonQuit;

			#region APPBAR

			[StructLayout(LayoutKind.Sequential)]
				struct RECT
			{
				public int left;
				public int top;
				public int right;
				public int bottom;
			}

			[StructLayout(LayoutKind.Sequential)]
				struct APPBARDATA
			{
				public int cbSize;
				public IntPtr hWnd;
				public int uCallbackMessage;
				public int uEdge;
				public RECT rc;
				public IntPtr lParam;
			}

			enum ABMsg : int
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

			enum ABNotify : int
			{
				ABN_STATECHANGE=0,
				ABN_POSCHANGED,
				ABN_FULLSCREENAPP,
				ABN_WINDOWARRANGE
			}

			enum ABEdge : int
			{
				ABE_LEFT=0,
				ABE_TOP,
				ABE_RIGHT,
				ABE_BOTTOM
			}

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
				abd.uEdge = (int)ABEdge.ABE_LEFT;

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
					//cp.Style &= (~0x00C00000); // WS_CAPTION
					cp.Style &= (~0x00800000); // WS_BORDER
					cp.ExStyle = 0x00000080 | 0x00000008; // WS_EX_TOOLWINDOW | WS_EX_TOPMOST
					return cp;
				}
			}

			#endregion

			/// <summary>
			/// The main entry point for the application.
			/// </summary>
			[STAThread]
			static void Main() 
			{
				Application.Run(new MainForm());
			}

			private void OnLoad(object sender, System.EventArgs e)
			{
				RegisterBar();
			}

			private void OnClosing(object sender, System.ComponentModel.CancelEventArgs e)
			{
				RegisterBar();
			}

			private void buttonQuit_Click(object sender, System.EventArgs e)
			{
				this.Close();
			}
		}
}
