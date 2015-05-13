using System;

namespace PainterAutomation
{

	public class FormatDescription
	{
		public string DisplayName;
		public string FilenameExtension;
		public string ID;

		public FormatDescription(string n, string e,string id )
		{
			this.DisplayName=n;
			this.FilenameExtension=e;
			this.ID =id;
		}

	}
	
	public class FormatsEnum
	{
		public static readonly FormatDescription TIFFormat = new FormatDescription("TIFF",".tif","tif");
		public static readonly FormatDescription PSDFormat = new FormatDescription("PSD",".psd","psd");
		private static readonly FormatDescription []enum_list = {TIFFormat,PSDFormat};

		public static FormatDescription  GetDescriptionFromName(string n)
		{
			foreach (FormatDescription f in enum_list)
			{
				if ( n== f.ID)
				{
					return f;
				}
				
			}
			throw new Errors.InvalidFormatError("no such format"); 
		}
	}
}
