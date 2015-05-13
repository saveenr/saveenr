using System;

namespace XaraAutomation.Delegates
{

	public delegate void LogCallback( StatusMessage msg , out bool stop);
	public delegate void JobCallback( int code, ConversionJob job );


}
