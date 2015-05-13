using System;
using System.Text;
using System.Runtime.InteropServices;
using System.Diagnostics;

using WindowsAutomation;

namespace XaraAutomation.Errors
{

	public class XaraError : System.Exception 
	{
		string m_msg;

		public XaraError(string str) : base ( str )
		{
			Tracing.StartMethodTrace();
			this.Init( str );
			Tracing.EndMethodTrace();
		}

		private void Init(string str)
		{
			this.m_msg  = str;
		}


		public override string Message
		{
			get
			{
				return this.m_msg;
			}
		}
	}


	public class UnexpectedDialog: XaraError 
	{
		public UnexpectedDialog(string str) : 
		base ( str )
		{
		}

	}

	public class CancelledByUserError: XaraError 
	{
		public CancelledByUserError(string str) : 
			base ( str )
		{
		}

	}

	public class InvalidFormatError: XaraError 
	{
		public InvalidFormatError(string str) : 
			base ( str )
		{
		}

	}

	public class WindowNotFoundError: XaraError 
	{
		public WindowNotFoundError(string str) : 
			base ( str )
		{
		}

	}

	public class TimeOutError: XaraError 
	{
		public TimeOutError(string str) : 
			base ( str )
		{
		}

	}
}
