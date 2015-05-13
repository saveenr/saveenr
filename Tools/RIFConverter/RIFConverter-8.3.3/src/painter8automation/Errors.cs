using System;
using System.Text;
using System.Runtime.InteropServices;
using System.Diagnostics;

using WindowsAutomation;

namespace PainterAutomation.Errors
{

	public class PainterError : System.Exception 
	{
		string m_msg;

		public PainterError(string str) : base ( str )
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


	public class UnexpectedDialog: PainterError 
	{
		public UnexpectedDialog(string str) : 
		base ( str )
		{
		}

	}

	public class CancelledByUserError: PainterError 
	{
		public CancelledByUserError(string str) : 
			base ( str )
		{
		}

	}

	public class InvalidFormatError: PainterError 
	{
		public InvalidFormatError(string str) : 
			base ( str )
		{
		}

	}

	public class WindowNotFoundError: PainterError 
	{
		public WindowNotFoundError(string str) : 
			base ( str )
		{
		}

	}

	public class TimeOutError: PainterError 
	{
		public TimeOutError(string str) : 
			base ( str )
		{
		}

	}
}
