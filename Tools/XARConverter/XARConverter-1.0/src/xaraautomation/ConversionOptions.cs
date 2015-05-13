using System;

namespace XaraAutomation
{

	public class ConversionOptions
	{
		public FormatDescription Format;
		public bool SaveAlpha;
		public string Colorspace;

		public ConversionOptions (FormatDescription format)
		{
			this.Format = format;
			this.SaveAlpha =true;
			this.Colorspace ="rgb";
		}

			
	}
}
