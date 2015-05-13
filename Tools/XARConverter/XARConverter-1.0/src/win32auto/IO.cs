using System;
using System.Collections;

namespace WindowsAutomation
{

	public class IO
	{

		public static void VerifyFileExists( string f )
		{
			if ( !System.IO.File.Exists(f) )
			{
				throw new Errors.AutomationError( string.Format( "File {0} does not exist", f ) );
			}
		}

		public static void VerifyFileDoesNotExist( string f )
		{
			if ( System.IO.File.Exists(f) )
			{
				throw new Errors.AutomationError( string.Format( "File {0} does exist s", f ) );
			}
		}

		public static bool DeleteFileIfExists(string f)
		{
			if ( System.IO.File.Exists(f))
			{
				System.IO.File.Delete(f);
				return true;
			}
			return false;
		}

		public static ArrayList GetFoldersRecursive( string path, bool recursive )
		{
			ArrayList list = new ArrayList();

			string [] folders = System.IO.Directory.GetDirectories( path );
			foreach (string folder in folders)
			{
				list.Add( folder);
			}

			if ( recursive )
			{
				foreach (string folder in folders)
				{
					ArrayList list2 = GetFoldersRecursive( folder, recursive );
					foreach (object item in list2)
					{
						list.Add(item);
					}
				}

			}
			return list;

		}

		public static ArrayList GetFilesRecursive( string path, string pattern , bool recursive)
		{
			ArrayList folders = GetFoldersRecursive( path, recursive );
			folders.Insert(0, path );
			ArrayList files = new ArrayList();

			foreach (string folder in folders)
			{
				string [] names= System.IO.Directory.GetFiles( folder, pattern );
				foreach (string name in names)
				{
					files.Add(name);
				}
			}
			return files;
		}


		public static bool CanGetExclusiveReadAccess( string fname )
		{
			bool success=false;

			string sz_fname = fname;

			uint GENERIC_READ = 0x80000000;
			uint OPEN_ALWAYS = 4;
			uint FILE_ATTRIBUTE_NORMAL = 0x80;
			int INVALID_HANDLE_VALUE = (-1);

			uint desired_access= GENERIC_READ;
			uint sharemode=0; // Do not try to share the file ... exclusive read is whant we want
			uint security_descriptor= 0; // null means use default security
			uint creation_disposition = OPEN_ALWAYS; 
			uint attributes = FILE_ATTRIBUTE_NORMAL;
			int template_file=0; // not needed
			
			int h=INVALID_HANDLE_VALUE;

			h = win32.CreateFile(		sz_fname , 
				desired_access,
				sharemode,
				security_descriptor,
				creation_disposition,
				attributes,
				template_file
				);

			if ( h==INVALID_HANDLE_VALUE )
			{
				success=false;
			}
			else
			{
				win32.CloseHandle( h );
				h=INVALID_HANDLE_VALUE;
				success=true;
			}

			return success;
		}


		class WaitExclusiveReadClient: WindowsAutomation.Timing.IWaitClient
		{
			string m_fname;

			public WaitExclusiveReadClient( string fname)
			{
				this.m_fname = fname;
			}

			public bool StopWaiting( )
			{
				return (WindowsAutomation.IO.CanGetExclusiveReadAccess(this.m_fname));
			}

			public void WaitCallback( WindowsAutomation.Timing.WaitObject.WaitState state, int elapsed )
			{
				// do nothing
			}
		}


		public static bool WaitForExclusiveRead( string fname, int timeout_miliseconds,  out int waited, WindowsAutomation.Timing.IWaitClient client, int callback_interval)
		{
			WaitExclusiveReadClient wait_client = new WaitExclusiveReadClient( fname );
			int interval = 500; // .5 second
			bool success;
			success = WindowsAutomation.Timing.WaitObject.WaitForCondition( wait_client, timeout_miliseconds, interval, out waited );
			return success;
		}

	}
}
