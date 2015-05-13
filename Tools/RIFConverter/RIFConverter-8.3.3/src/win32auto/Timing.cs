using System;

namespace WindowsAutomation.Timing
{

	public class StopWatch
	{
		protected System.DateTime m_start_time;

		public void Start()
		{
			this.m_start_time = System.DateTime.Now;
		}

		public System.TimeSpan ElapsedSpan
		{
			get
			{
				return System.DateTime.Now - this.m_start_time ;
			}
		}


		public int ElapsedMiliseconds
		{
			get
			{
				return (int) this.ElapsedSpan.TotalMilliseconds;
			}
		}

		
	}


		
	public interface IWaitClient
	{
		bool StopWaiting();
		void WaitCallback( WaitObject.WaitState state, int elapsed);
	}


	public class WaitObject
	{

		public enum WaitState {begin_state, interval_state, finished_state, timeout_state};
		
		public static bool WaitForCondition( IWaitClient client, int timeout, int interval, out int elapsed)
		{
			elapsed=0;
			client.WaitCallback(WaitState.begin_state,0);
			StopWatch watch = new StopWatch();
			watch.Start();
			bool success=false;
			while ( watch.ElapsedMiliseconds  <= timeout)
			{
				if ( client.StopWaiting() == true )
				{
					client.WaitCallback(WaitState.finished_state , watch.ElapsedMiliseconds  );
					success=true;
					break;
				}
				System.Threading.Thread.Sleep( interval);
				client.WaitCallback(WaitState.interval_state , watch.ElapsedMiliseconds );
			}
			client.WaitCallback(WaitState.timeout_state ,watch.ElapsedMiliseconds );
			return success;
		}

	}

}
