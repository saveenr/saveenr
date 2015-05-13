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
 
HISTORY
2004-11-2 Removed the "using PainterAutomation" directive
2004-11-2 Now resets the stopwatch when user presses the start button

*/

namespace rifconverterapp
{

	public class RCAppForm : AppBar.ApplicationBarForm
	{

		PainterAutomation.ConverterEngine rc;

		private System.Windows.Forms.Button m_button_start;
		private System.Windows.Forms.Button m_button_stop;
		private System.ComponentModel.IContainer components;


		private System.Windows.Forms.TextBox m_textbox_from;
		private System.Windows.Forms.TextBox m_textbox_to;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.CheckBox m_checkbox_include_subfolders;


		private System.Windows.Forms.RichTextBox m_richtext_log;
		private System.Windows.Forms.Label m_label_log;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label m_label_current_job_number;
		private System.Windows.Forms.Timer m_timer1;
		private System.Windows.Forms.Label m_label_time_elapsed;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.LinkLabel linkwebsite;
		private System.Windows.Forms.GroupBox groupFormat;
		private System.Windows.Forms.RadioButton radioTIF;
		private System.Windows.Forms.RadioButton radioPSD;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button buttonBrowseFrom;
		private System.Windows.Forms.Button buttonBrowseTo;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.LinkLabel linkTraceFile;

		private WindowsAutomation.Timing.StopWatch m_watch1 = new WindowsAutomation.Timing.StopWatch();
		public static string appname ="RIFConverter8.3.3";
		private int m_painter_hwnd;
		private System.Threading.Thread m_worker_thread;
		private static System.IO.TextWriter logstream;
		private string trace_log_fname;
		private Prefs m_prefs;
		private int m_total_jobs;

		void m_worker_thread_function()
		{
			this.LogFromThread ( string.Format( "{0} started", m_worker_thread.Name ));
			try
			{
				rc.RunJobs( );
			}
			catch ( Exception err)
			{
				this.LogFromThread ( "--------------------");
				this.LogFromThread ( "Exception" );
				this.LogFromThread ( string.Format( "Type: {0}", err.GetType().ToString() ) );
				this.LogFromThread ( string.Format( "Message: {0}", err.Message ) );
				this.LogFromThread ( string.Format( "Source: {0}", err.Source ) );
			}
			this.m_StopWork();
			this.LogFromThread ( string.Format( "{0} stopped", m_worker_thread.Name ));
			this.m_worker_thread = null;
			this.m_timer1.Stop();

		}

		private void LogFromThread(string msg)
		{
			Invoke(new LogDelegate(this.m_Log ), new object [] { msg });
		}


		public RCAppForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			this.Construction_ApplicationBar();

			Trace.AutoFlush=true;
			this.trace_log_fname = System.IO.Path.Combine( System.Environment.GetFolderPath( System.Environment.SpecialFolder.Personal ) , appname + "_trace.txt" );
			logstream = System.IO.File.CreateText( this.trace_log_fname );
			Trace.Listeners.Add( new TextWriterTraceListener( logstream ) );
			Trace.AutoFlush=true;

			this.Text=appname;
			m_painter_hwnd = PainterAutomation.PainterApp.FindPainter8Window();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			this.m_Log( "" );

			this.m_prefs = new Prefs( Microsoft.Win32.Registry.CurrentUser , "Software\\" + appname );


			this.m_prefs.Add( new Pref("DefaultFrom", typeof(string) , "<Unspecified>") );
			this.m_prefs.Add( new Pref("DefaultTo", typeof(string)  , "<Unspecified>") );
			this.m_prefs.Add( new Pref("UseSubfolders", typeof(bool)  , true ) );
			this.m_prefs.Add( new Pref("SaveAlpha", typeof(bool) , false) );
			this.m_prefs.Add( new Pref("Overwrite", typeof(bool) , true ) );
			this.m_prefs.Add( new Pref("Colorspace", typeof(string) , "rgb") );
			this.m_prefs.Add( new Pref("OutputFormat", typeof(string) , "tif") );
			this.radioPSD.Checked=false;
			this.radioTIF.Checked=true;

			this.m_LoadPreferences();

			this.m_total_jobs=0;

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
			this.components = new System.ComponentModel.Container();
			this.m_button_start = new System.Windows.Forms.Button();
			this.m_button_stop = new System.Windows.Forms.Button();
			this.m_textbox_from = new System.Windows.Forms.TextBox();
			this.m_textbox_to = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.m_checkbox_include_subfolders = new System.Windows.Forms.CheckBox();
			this.m_richtext_log = new System.Windows.Forms.RichTextBox();
			this.m_label_log = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.m_label_current_job_number = new System.Windows.Forms.Label();
			this.m_label_time_elapsed = new System.Windows.Forms.Label();
			this.m_timer1 = new System.Windows.Forms.Timer(this.components);
			this.label11 = new System.Windows.Forms.Label();
			this.linkwebsite = new System.Windows.Forms.LinkLabel();
			this.groupFormat = new System.Windows.Forms.GroupBox();
			this.radioPSD = new System.Windows.Forms.RadioButton();
			this.radioTIF = new System.Windows.Forms.RadioButton();
			this.label2 = new System.Windows.Forms.Label();
			this.buttonBrowseFrom = new System.Windows.Forms.Button();
			this.buttonBrowseTo = new System.Windows.Forms.Button();
			this.label3 = new System.Windows.Forms.Label();
			this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
			this.label4 = new System.Windows.Forms.Label();
			this.linkTraceFile = new System.Windows.Forms.LinkLabel();
			this.groupFormat.SuspendLayout();
			this.SuspendLayout();
			// 
			// m_button_start
			// 
			this.m_button_start.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.m_button_start.Font = new System.Drawing.Font("Trebuchet MS", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.m_button_start.Location = new System.Drawing.Point(40, 336);
			this.m_button_start.Name = "m_button_start";
			this.m_button_start.Size = new System.Drawing.Size(128, 40);
			this.m_button_start.TabIndex = 1;
			this.m_button_start.Text = "Start";
			this.m_button_start.Click += new System.EventHandler(this.m_button_start_Click);
			// 
			// m_button_stop
			// 
			this.m_button_stop.Location = new System.Drawing.Point(184, 336);
			this.m_button_stop.Name = "m_button_stop";
			this.m_button_stop.Size = new System.Drawing.Size(120, 40);
			this.m_button_stop.TabIndex = 2;
			this.m_button_stop.Text = "Stop";
			this.m_button_stop.Click += new System.EventHandler(this.m_button_stop_Click);
			// 
			// m_textbox_from
			// 
			this.m_textbox_from.Location = new System.Drawing.Point(8, 32);
			this.m_textbox_from.Name = "m_textbox_from";
			this.m_textbox_from.ReadOnly = true;
			this.m_textbox_from.Size = new System.Drawing.Size(248, 21);
			this.m_textbox_from.TabIndex = 13;
			this.m_textbox_from.Text = "<Unspecified>";
			// 
			// m_textbox_to
			// 
			this.m_textbox_to.Location = new System.Drawing.Point(8, 128);
			this.m_textbox_to.Name = "m_textbox_to";
			this.m_textbox_to.ReadOnly = true;
			this.m_textbox_to.Size = new System.Drawing.Size(248, 21);
			this.m_textbox_to.TabIndex = 14;
			this.m_textbox_to.Text = "<Unspecified>";
			// 
			// label6
			// 
			this.label6.BackColor = System.Drawing.Color.DarkGray;
			this.label6.Font = new System.Drawing.Font("Trebuchet MS", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label6.Location = new System.Drawing.Point(0, 0);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(344, 24);
			this.label6.TabIndex = 20;
			this.label6.Text = "Step 1: Where are the RIF files?";
			// 
			// label7
			// 
			this.label7.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label7.Location = new System.Drawing.Point(0, 416);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(48, 16);
			this.label7.TabIndex = 21;
			this.label7.Text = "Status:";
			this.label7.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// m_checkbox_include_subfolders
			// 
			this.m_checkbox_include_subfolders.Location = new System.Drawing.Point(8, 72);
			this.m_checkbox_include_subfolders.Name = "m_checkbox_include_subfolders";
			this.m_checkbox_include_subfolders.Size = new System.Drawing.Size(152, 16);
			this.m_checkbox_include_subfolders.TabIndex = 25;
			this.m_checkbox_include_subfolders.Text = "Include Subfolders";
			// 
			// m_richtext_log
			// 
			this.m_richtext_log.AcceptsTab = true;
			this.m_richtext_log.BackColor = System.Drawing.SystemColors.Control;
			this.m_richtext_log.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.m_richtext_log.CausesValidation = false;
			this.m_richtext_log.DetectUrls = false;
			this.m_richtext_log.HideSelection = false;
			this.m_richtext_log.Location = new System.Drawing.Point(48, 544);
			this.m_richtext_log.Name = "m_richtext_log";
			this.m_richtext_log.Size = new System.Drawing.Size(272, 128);
			this.m_richtext_log.TabIndex = 33;
			this.m_richtext_log.TabStop = false;
			this.m_richtext_log.Text = "";
			// 
			// m_label_log
			// 
			this.m_label_log.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.m_label_log.Location = new System.Drawing.Point(0, 536);
			this.m_label_log.Name = "m_label_log";
			this.m_label_log.Size = new System.Drawing.Size(40, 16);
			this.m_label_log.TabIndex = 34;
			this.m_label_log.Text = "Log:";
			this.m_label_log.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label1
			// 
			this.label1.BackColor = System.Drawing.Color.DarkGray;
			this.label1.Font = new System.Drawing.Font("Trebuchet MS", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label1.Location = new System.Drawing.Point(0, 96);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(344, 24);
			this.label1.TabIndex = 35;
			this.label1.Text = "Step 2: Where to put the output files?";
			// 
			// m_label_current_job_number
			// 
			this.m_label_current_job_number.Location = new System.Drawing.Point(48, 416);
			this.m_label_current_job_number.Name = "m_label_current_job_number";
			this.m_label_current_job_number.Size = new System.Drawing.Size(280, 72);
			this.m_label_current_job_number.TabIndex = 43;
			this.m_label_current_job_number.Text = "<Job>";
			// 
			// m_label_time_elapsed
			// 
			this.m_label_time_elapsed.Location = new System.Drawing.Point(48, 496);
			this.m_label_time_elapsed.Name = "m_label_time_elapsed";
			this.m_label_time_elapsed.Size = new System.Drawing.Size(100, 16);
			this.m_label_time_elapsed.TabIndex = 47;
			this.m_label_time_elapsed.Text = "<time>";
			// 
			// m_timer1
			// 
			this.m_timer1.Interval = 1000;
			this.m_timer1.Tick += new System.EventHandler(this.m_timer1_Tick);
			// 
			// label11
			// 
			this.label11.Location = new System.Drawing.Point(0, 496);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(48, 16);
			this.label11.TabIndex = 50;
			this.label11.Text = "Time:";
			this.label11.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// linkwebsite
			// 
			this.linkwebsite.Location = new System.Drawing.Point(208, 496);
			this.linkwebsite.Name = "linkwebsite";
			this.linkwebsite.Size = new System.Drawing.Size(120, 23);
			this.linkwebsite.TabIndex = 51;
			this.linkwebsite.TabStop = true;
			this.linkwebsite.Text = "RIFConverter website";
			this.linkwebsite.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkwebsite_LinkClicked);
			// 
			// groupFormat
			// 
			this.groupFormat.Controls.Add(this.radioPSD);
			this.groupFormat.Controls.Add(this.radioTIF);
			this.groupFormat.Location = new System.Drawing.Point(8, 216);
			this.groupFormat.Name = "groupFormat";
			this.groupFormat.Size = new System.Drawing.Size(144, 72);
			this.groupFormat.TabIndex = 52;
			this.groupFormat.TabStop = false;
			this.groupFormat.Text = "To Format";
			// 
			// radioPSD
			// 
			this.radioPSD.Location = new System.Drawing.Point(8, 40);
			this.radioPSD.Name = "radioPSD";
			this.radioPSD.Size = new System.Drawing.Size(56, 24);
			this.radioPSD.TabIndex = 1;
			this.radioPSD.Text = "PSD";
			// 
			// radioTIF
			// 
			this.radioTIF.Location = new System.Drawing.Point(8, 16);
			this.radioTIF.Name = "radioTIF";
			this.radioTIF.Size = new System.Drawing.Size(56, 24);
			this.radioTIF.TabIndex = 0;
			this.radioTIF.Text = "TIF";
			// 
			// label2
			// 
			this.label2.BackColor = System.Drawing.Color.DarkGray;
			this.label2.Font = new System.Drawing.Font("Trebuchet MS", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label2.Location = new System.Drawing.Point(0, 184);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(344, 24);
			this.label2.TabIndex = 53;
			this.label2.Text = "Step 3: What kind of conversion?";
			// 
			// buttonBrowseFrom
			// 
			this.buttonBrowseFrom.Location = new System.Drawing.Point(264, 32);
			this.buttonBrowseFrom.Name = "buttonBrowseFrom";
			this.buttonBrowseFrom.Size = new System.Drawing.Size(64, 23);
			this.buttonBrowseFrom.TabIndex = 54;
			this.buttonBrowseFrom.Text = "Browse";
			this.buttonBrowseFrom.Click += new System.EventHandler(this.buttonBrowseFrom_Click);
			// 
			// buttonBrowseTo
			// 
			this.buttonBrowseTo.Location = new System.Drawing.Point(264, 128);
			this.buttonBrowseTo.Name = "buttonBrowseTo";
			this.buttonBrowseTo.Size = new System.Drawing.Size(64, 23);
			this.buttonBrowseTo.TabIndex = 55;
			this.buttonBrowseTo.Text = "Browse";
			this.buttonBrowseTo.Click += new System.EventHandler(this.buttonBrowseTo_Click);
			// 
			// label3
			// 
			this.label3.BackColor = System.Drawing.Color.DarkGray;
			this.label3.Font = new System.Drawing.Font("Trebuchet MS", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label3.Location = new System.Drawing.Point(0, 296);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(344, 24);
			this.label3.TabIndex = 56;
			this.label3.Text = "Step 4: Do the conversion";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(16, 160);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(312, 16);
			this.label4.TabIndex = 57;
			this.label4.Text = "RIFCoverter will not overwrite files that already exist.";
			// 
			// linkTraceFile
			// 
			this.linkTraceFile.Location = new System.Drawing.Point(208, 520);
			this.linkTraceFile.Name = "linkTraceFile";
			this.linkTraceFile.TabIndex = 58;
			this.linkTraceFile.TabStop = true;
			this.linkTraceFile.Text = "Show Debug Trace";
			this.linkTraceFile.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkTraceFile_LinkClicked);
			// 
			// RCAppForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
			this.ClientSize = new System.Drawing.Size(342, 728);
			this.Controls.Add(this.linkTraceFile);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.buttonBrowseTo);
			this.Controls.Add(this.buttonBrowseFrom);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.groupFormat);
			this.Controls.Add(this.linkwebsite);
			this.Controls.Add(this.label11);
			this.Controls.Add(this.m_label_time_elapsed);
			this.Controls.Add(this.m_label_current_job_number);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.m_label_log);
			this.Controls.Add(this.m_richtext_log);
			this.Controls.Add(this.m_button_stop);
			this.Controls.Add(this.m_checkbox_include_subfolders);
			this.Controls.Add(this.m_button_start);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.m_textbox_to);
			this.Controls.Add(this.m_textbox_from);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "RCAppForm";
			this.Text = "APPTITLE";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.OnClosing);
			this.groupFormat.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new RCAppForm());
		}

		private void RCAppForm_Load(object sender, System.EventArgs e)
		{
			this.m_SetPainterPosition();

		}


		protected void m_SetPainterPosition()
		{

			if ( m_painter_hwnd!=0 )
			{
				WindowsAutomation.UI.Window_ShowRestore( m_painter_hwnd );
			}
		}


		private void m_button_quit_Click(object sender, System.EventArgs e)
		{
			if (this.m_worker_thread!=null)
			{
				this.m_StopWorkerThread();
				this.m_StopWork();
			}
			this.Close();
		}


		private void m_button_start_Click(object sender, System.EventArgs e)
		{

			if ( !System.IO.Directory.Exists( this.m_textbox_to.Text ) )
			{
				System.Windows.Forms.MessageBox.Show( string.Format( "Path does not exist {0}.", this.m_textbox_to.Text  ));
				return;
			}

			if ( !System.IO.Directory.Exists( this.m_textbox_from.Text ) )
			{
				System.Windows.Forms.MessageBox.Show( string.Format( "Path does not exist {0}.", this.m_textbox_from.Text ) );
				return;
			}

			int painter_hwnd = PainterAutomation.PainterApp.FindPainter8Window();	

			if ( painter_hwnd==0 )
			{
				System.Windows.Forms.MessageBox.Show( "RIFConverter cannot find the painter 8 window. Start Painter 8 then try again.");
				return;
			}

			this.m_SavePreferences();
			this.m_StartWork();
			System.Threading.Thread.Sleep(1000); // QUESTION: Why is this needed?

			bool save_alpha;
			string cs = "rgb";
			if ( this.m_OutputFormat == PainterAutomation.FormatsEnum.PSDFormat )
			{
				save_alpha = true;
			}
			else
			{
				save_alpha = false;
			}
					
			PainterAutomation.ConversionOptions co = new PainterAutomation.ConversionOptions( 
				this.m_OutputFormat , 
				save_alpha, 
				cs );

			PainterAutomation.Delegates.LogCallback callback = new PainterAutomation.Delegates.LogCallback( m_ConverterCallback);
			PainterAutomation.Delegates.JobCallback job_callback = new PainterAutomation.Delegates.JobCallback( this.m_ConverterJobCallback );
				
			painter_hwnd = PainterAutomation.PainterApp.FindPainter8Window();	
			WindowsAutomation.UI.Window_ShowMaximized( painter_hwnd );
			PainterAutomation.PainterApp painterapp  = new PainterAutomation.PainterApp( callback );
			painterapp.Activate();
			painterapp.VerifyNoDialogs();

			rc = new PainterAutomation.ConverterEngine( painterapp );
			rc.OnLogMessage += callback;
			rc.OnJobStatusChange += job_callback;
			rc.PrepareJobs(this.m_textbox_from.Text , this.m_textbox_to.Text , co);
			this.m_total_jobs=rc.Jobs.Length;
			this.LogFromThread( "Starting" );
			this.m_StartWorkerThread();
				

		}


		public void m_ConverterCallback( PainterAutomation.StatusMessage msg , out bool stop)
		{
			// NOTE: This function will be called from the worker thread not the UI thread
			this.LogFromThread( msg.Message );
			stop=false;
		}

		public void m_ConverterJobCallback( int code, PainterAutomation.ConversionJob job)
		{
			// NOTE: This function will be called from the worker thread not the UI thread
			if (job!=null)
			{
				string jobnum = string.Format( "{0} of {1}", job.SequenceNumber + 1, this.m_total_jobs );
				string infile = System.IO.Path.GetFileName( job.InputFile );
				string outfile = System.IO.Path.GetFileName( job.OutputFile  );
				this.SetStatusFromThread( "Running" , jobnum, infile , outfile );
			}
			else
			{
				this.SetStatusFromThread( "N/A", "N/A", "N/A", "N/A");
			}
		}

		public void SetStatus( string x, string job_info , string infile, string outfile)
		{
			string s = string.Format( "{0}\r\n{1}\r\n{2}\r\n{3}", x, job_info, infile, outfile );
			this.m_label_current_job_number.Text = s;
		}

		private void SetStatusFromThread(string x, string a,string b,string c)
		{
			Invoke(new SetStatusDelegate(this.SetStatus ), new object [] {x, a,b,c});
		}


		private void m_StartWork()
		{
			this.m_SetPainterPosition();
			this.m_ClearLog( "" );
			if ( !System.IO.Directory.Exists( this.m_textbox_from.Text   ) )
			{
				// WORKITEM: show an error dialog instead
				this.m_Log( "The input path does not exist" );
				return;
			}
			if ( !System.IO.Directory.Exists( this.m_textbox_to.Text ) )
			{
				// WORKITEM: show an error dialog instead
				this.m_Log( "The outpth path does not exist" );
				return;
			}
			this.SetStatus("Running","N/A","N/A","N/A");
			this.m_watch1.Start();
			this.m_timer1.Start();
		}

		private void m_StartWorkerThread()
		{
			this.m_worker_thread = new System.Threading.Thread( new System.Threading.ThreadStart( m_worker_thread_function ) );
			this.m_worker_thread.Name="RIFConverter Worker Thread";
			this.m_worker_thread.Start();
		}


		private void m_StopWorkerThread()
		{
			if ( this.m_worker_thread != null )
			{
				this.m_worker_thread.Abort();
				this.m_worker_thread =null;
			}
		}

		private void m_StopWork()
		{
			this.m_timer1.Stop();
			this.SetStatusFromThread("Not Running","N/A","N/A","N/A");
			this.LogFromThread( "Stopped" );
		}


		private void m_ClearLog( string s )
		{
			this.m_richtext_log.Clear();
			
		}

		private void m_Log( string s )
		{
			this.m_richtext_log.AppendText( s );
			this.m_richtext_log.AppendText( "\r\n" );
			this.m_LogScrollToBottom();
			
		}

		private void m_LogScrollToBottom()
		{
			this.m_richtext_log.Select( this.m_richtext_log.TextLength-1,0);
			this.m_richtext_log.ScrollToCaret();
		}

		private void m_button_stop_Click(object sender, System.EventArgs e)
		{
			this.m_Log( "User pressed STOP" );
			if ( this.m_worker_thread !=null )
			{
				this.m_StopWorkerThread();
				this.m_StopWork();

			}
		}

		private PainterAutomation.FormatDescription m_OutputFormat
		{
			get
			{
				if ( this.radioPSD.Checked==true )
				{
					return PainterAutomation.FormatsEnum.PSDFormat;
				}
				else if ( this.radioTIF.Checked==true )
				{
					return PainterAutomation.FormatsEnum.TIFFormat;
				}
				else
				{
					throw new PainterAutomation.Errors.InvalidFormatError("");
				}
			}
			set
			{
				if ( value.ID == PainterAutomation.FormatsEnum.TIFFormat.ID )
				{
					this.radioTIF.Checked=true;
					this.radioPSD.Checked=false;
				}
				else if ( value.ID == PainterAutomation.FormatsEnum.PSDFormat.ID )
				{
					this.radioTIF.Checked=false;
					this.radioPSD.Checked=true;
				}
				else
				{
					throw new PainterAutomation.Errors.InvalidFormatError("");
				}
			}

		}

		private void m_SavePreferences()
		{
			// WORKITEM: Don't Use prefs


			this.m_prefs.Set( "DefaultFrom", this.m_textbox_from.Text );
			this.m_prefs.Set( "DefaultTo", this.m_textbox_to.Text  );
			this.m_prefs.Set( "UseSubfolders", this.m_checkbox_include_subfolders.Checked );
			this.m_prefs.Set( "OutputFormat", this.m_OutputFormat.ID  );		
			
		 
		}



		private void m_LoadPreferences()
		{

			// WORKITEM: don't use prefs

			this.m_textbox_from.Text = (string) this.m_prefs.Get( "DefaultFrom" );
			this.m_textbox_to.Text= (string) this.m_prefs.Get( "DefaultTo" );

			this.m_checkbox_include_subfolders.Checked = (bool)this.m_prefs.Get( "UseSubfolders" ) ;
			this.m_OutputFormat = PainterAutomation.FormatsEnum.GetDescriptionFromName( (string) this.m_prefs.Get( "OutputFormat" ) );

		}

		private void m_timer1_Tick(object sender, System.EventArgs e)
		{
			this.m_label_time_elapsed.Text  = string.Format( "{0:00}:{1:00}:{2:00}", this.m_watch1.ElapsedSpan.Hours,this.m_watch1.ElapsedSpan.Minutes  , this.m_watch1.ElapsedSpan.Seconds );
		}




		private void OnClosing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (this.m_worker_thread!=null)
			{
				this.m_StopWorkerThread();
				this.m_StopWork();
			}

			this.m_SavePreferences();
		}

		private void buttonBrowseFrom_Click(object sender, System.EventArgs e)
		{

			DialogResult result = folderBrowserDialog1.ShowDialog();
			if( result == DialogResult.OK )
			{
				this.m_textbox_from.Text  = folderBrowserDialog1.SelectedPath;
			}

		
		}

		private void buttonBrowseTo_Click(object sender, System.EventArgs e)
		{
			DialogResult result = folderBrowserDialog1.ShowDialog();
			if( result == DialogResult.OK )
			{
				this.m_textbox_to.Text  = folderBrowserDialog1.SelectedPath;
			}



		}

		private void linkwebsite_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			Process.Start("http://saveen.members.winisp.net/rifconverter/"); 
		}



		private void linkTraceFile_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			if ( System.IO.File.Exists( this.trace_log_fname ) ) 
			{
				System.Diagnostics.Process.Start( this.trace_log_fname  );
			}
			else
			{
				System.Windows.Forms.MessageBox.Show( string.Format( "Trace file \"{0}\"does not exist .", this.m_textbox_to.Text  ));
			}

		}


	}

	public delegate void LogDelegate(string msg);
	public delegate void SetStatusDelegate(string x, string a,string b,string c);



}
