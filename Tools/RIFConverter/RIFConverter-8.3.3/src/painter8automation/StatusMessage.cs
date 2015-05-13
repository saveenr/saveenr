using System;

namespace PainterAutomation
{

	public class StatusMessage
	{

		public string Message;
		
		public StatusMessage( string m )
		{
			Init( m );
		}
		
		public void Init( string m )
		{
			this.Message = m;
		}


	}
}
