#define TRACE

using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using WindowsAutomation;

namespace XaraAutomation
{

	public class ConverterEngine
	{


		public event Delegates.LogCallback OnLogMessage;
		public event Delegates.JobCallback OnJobStatusChange;

		private XaraApp m_xarahwnd;
		public ConversionJob [] Jobs;

		public ConverterEngine(XaraApp app )
		{
			Tracing.StartMethodTrace();
			this.Init( app );
			Tracing.EndMethodTrace();
		}
		
		public void Init (XaraApp app )
		{
			Tracing.StartMethodTrace();

			this.m_xarahwnd  = app;
			

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
			string extension = ".xar";
			string pattern = "*" + extension;

            Tracing.WriteLine( "Searching for {0} files in {1}", extension, inpath );

			this.Jobs = GenerateJobs(inpath, pattern, outpath, co);

            Tracing.WriteLine("{0} {1} files found", this.Jobs.Length, extension);
            Tracing.EndMethodTrace();
		}



		public void RunJobs( )
		{

			Tracing.StartMethodTrace();

			if ( this.Jobs == null)
			{
				throw new Errors.XaraError("Init() was not called");
			}

			
			foreach (ConversionJob job in this.Jobs)
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
				if ( ! (job.Options.Format.ID ==FormatsEnum.TIFFormat.ID ) )
				{
					Log(">>>Unknown Format {0}", job.Options.Format.ID );
					throw new Errors.InvalidFormatError( "Unknown format" );
				}

				this.m_xarahwnd.FileOpen( job.InputFile   );
				this.m_xarahwnd.FileSave( job.OutputFile  , job.Options.SaveAlpha  , job.Options.Colorspace   , job.Options.Format  , job.InputFile );
				this.m_xarahwnd.FileClose( );

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

			XaraAutomation.ConversionJob [] jobs = new XaraAutomation.ConversionJob [input_files.Count ] ;

			int index=0;
			foreach (string input_file in input_files)
			{
				// Create the Job object
				XaraAutomation.ConversionJob job = new XaraAutomation.ConversionJob(	
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
