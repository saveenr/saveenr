#define TRACE

using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using WindowsAutomation;

namespace PainterAutomation
{

	public class ConverterEngine
	{


		public event Delegates.LogCallback OnLogMessage;
		public event Delegates.JobCallback OnJobStatusChange;

		private PainterApp m_painter;
		private ConversionJob [] m_jobs;

		public ConverterEngine(PainterApp pa )
		{
			Tracing.StartMethodTrace();
			this.Init( pa );
			Tracing.EndMethodTrace();
		}
		
		public void Init (PainterApp pa )
		{
			Tracing.StartMethodTrace();

			this.m_painter  = pa;
			

			Tracing.EndMethodTrace();
		}	

		public void Log( string format, params object [] args)
		{
			if (this.OnLogMessage !=null)
			{
				string s= string.Format( format , args );
				StatusMessage msg = new StatusMessage( s );
				bool stop_flag=false;
				this.OnLogMessage( msg, out stop_flag );
				if (stop_flag)
				{
					throw new Errors.CancelledByUserError( "Cancelled by caller");
				}
			}
		}

		public void PrepareJobs( string inpath, string outpath , ConversionOptions co)
		{

			Tracing.StartMethodTrace();

			Log( "Searching for RIFs in {0}", inpath );

			this.m_jobs = GenerateJobs( inpath , "*.rif", outpath , co);

			Log("{0} RIF files to convert", this.m_jobs.Length );

			Tracing.EndMethodTrace();
		}

		public ConversionJob [] Jobs
		{
			get
			{
				return this.m_jobs ;
			}
		}


		public void RunJobs( )
		{

			Tracing.StartMethodTrace();

			if ( this.m_jobs == null)
			{
				throw new Errors.PainterError("Init() was not called");
			}

			
			foreach (ConversionJob job in this.m_jobs)
			{
				this.DoJob(job);
			}

			Tracing.EndMethodTrace();
		}

		private void report_job_status( int code, ConversionJob job )
		{
			if (this.OnJobStatusChange!=null)
			{
				this.OnJobStatusChange(code,job);
			}
		}

		public void DoJob( ConversionJob job )
		{
			Tracing.StartMethodTrace();

			this.report_job_status( 0, job );
			Log( "" );
			Log( "Converting file #{0} ", job.SequenceNumber +1 );
			Log( "Input file: {0} " , job.InputFile  );
			Log( "Output file: {0} " , job.OutputFile );

			bool skip_this_file=false;

			bool output_already_existed = System.IO.File.Exists( job.OutputFile );
			if ( output_already_existed )
			{
				Log( "Output file already exists; Skipping." );
				skip_this_file=true;
			}
			else
			{
				// Ensure that that is a path ready for the output file

				string path_for_file = System.IO.Path.GetDirectoryName(job.OutputFile  );
				if ( !System.IO.Directory.Exists( path_for_file ) )
				{
					Log( "Creating folder {0}", path_for_file );
					System.IO.Directory.CreateDirectory( path_for_file );
				}
			}


			if (skip_this_file==false)
			{
				if ( ! ( (job.Options.Format.ID ==FormatsEnum.PSDFormat.ID ) || (job.Options.Format.ID ==FormatsEnum.TIFFormat.ID )) )
				{
					Log(">>>Unknown Format {0}", job.Options.Format.ID );
					throw new Errors.InvalidFormatError( "Unknown format" );
				}

				Log("File/Open");
				this.m_painter.FileOpen( job.InputFile   );
				Log("File/Save");
				this.m_painter.FileSave( job.OutputFile  , job.Options.SaveAlpha  , job.Options.Colorspace   , job.Options.Format   );
				Log("File/Close");
				this.m_painter.FileClose( );

			}

			Log( "Finished file #{0} ", job.SequenceNumber +1  );

			Tracing.EndMethodTrace();

		}





		public bool MyWaitCallback( int status, int time )
		{
			Tracing.StartMethodTrace();

			bool retval =true;
			
			Tracing.EndMethodTrace();

			return retval;
		}





		public static string CalculateOutputFilename( string infname , string inroot, string outroot, ConversionOptions co)
		{
			string basename = System.IO.Path.GetFileName(infname);

			string relpath = infname.Substring( inroot.Length+1 );
			string outfname = System.IO.Path.Combine( outroot, relpath );
			string ext=co.Format.FilenameExtension;

			outfname = System.IO.Path.ChangeExtension( outfname , ext);

			return outfname;
		}


		public static ConversionJob [] GenerateJobs( string inpath, string search_pattern, string outpath, ConversionOptions co)
		{
			System.Collections.ArrayList input_files = WindowsAutomation.IO.GetFilesRecursive( inpath, search_pattern , true );

			PainterAutomation.ConversionJob [] jobs = new PainterAutomation.ConversionJob [input_files.Count ] ;

			int index=0;
			foreach (string input_file in input_files)
			{
				// Create the Job object
				PainterAutomation.ConversionJob job = new PainterAutomation.ConversionJob(	
						input_file,
						CalculateOutputFilename( input_file, inpath , outpath, co),
						index,
						co );

				// Place it in the list
				jobs[index]=job;

				index++;
			}
			return jobs;
		}
	}
}
