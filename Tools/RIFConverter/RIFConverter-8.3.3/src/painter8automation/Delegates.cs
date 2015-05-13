using System;

namespace PainterAutomation.Delegates
{

	public delegate void LogCallback( StatusMessage msg , out bool stop);
	public delegate void JobCallback( int code, ConversionJob job );


}
