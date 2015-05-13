using System;

namespace WindowsAutomation.Errors
{

	public class AutomationError : System.Exception 
	{
		protected string m_msg;

		public AutomationError(string str) : base ( str )
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


        //throw new Errors.AutomationError( "Could not find child" );
	}

    public class FindError : AutomationError
    {

        public FindError(string str) : base ( str )
        {
        }
    }
}
