using System;

namespace PainterAutomation
{

	public class ConversionOptions
	{
		public FormatDescription Format;
		public bool SaveAlpha;
		public string Colorspace;

		public ConversionOptions (FormatDescription format, bool a, string cs)
		{
			this.Format = format;
			this.SaveAlpha =a;
			this.Colorspace =cs;
		}

			
	}
}
