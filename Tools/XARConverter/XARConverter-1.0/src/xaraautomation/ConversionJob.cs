#define TRACE

using System;
using System.Text;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace XaraAutomation
{
	public class ConversionJob
	{
		public string InputFile;
		public string OutputFile;
		public long InputFilesize;
		public int SequenceNumber;
		public ConversionOptions Options;

		private ConversionJob()
		{
		}

		public ConversionJob( string inf, string outf, int index, ConversionOptions o )
		{
			WindowsAutomation.IO.VerifyFileExists( inf );

			this.InputFile = inf;
			this.OutputFile =outf;
			this.SequenceNumber=index;
			this.Options = o;
			
			System.IO.FileInfo fi = new System.IO.FileInfo( this.InputFile );

			this.InputFilesize = fi.Length;

		}
	

	}


}
