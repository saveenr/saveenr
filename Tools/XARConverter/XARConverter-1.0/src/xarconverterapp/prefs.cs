using System;

namespace rifconverterapp
{


	class Pref
	{
		public string m_name;
		public System.Type m_type;
		public object m_defval;

		public Pref( string name, System.Type type , object defval )
		{
			this.m_name=name;
			this.m_type=type;
			this.m_defval=defval;
		}

	}

	class Prefs
	{
		private Microsoft.Win32.RegistryKey rootkey;
		private string path;
		private System.Collections.ArrayList m_list;


		public Prefs(Microsoft.Win32.RegistryKey k, string xpath)
		{
			rootkey = k;
			path = xpath;
			this.m_list  = new System.Collections.ArrayList();
		}


		public void Add( Pref p )
		{
			this.m_list.Add(p);
		}

		Pref Find( string name )
		{
			foreach (Pref p in this.m_list)
			{
				if ( name.ToLower() == p.m_name.ToLower() )
				{
					return p;
				}
			}
			return null;
		}

		public object Get( string name )
		{

			Pref p = this.Find( name );
			if (p==null)
			{
				throw new Exception("No such pref: " + name);
			}
			object o = this.__getregvalue( p.m_name );
			if (o==null) 
			{ 
				o = p.m_defval; 
			}
			else
			{
				if ( p.m_type== false.GetType() )
				{
					o = ("True" == o.ToString()  ) ;
				}
			}
			return o;
		}

		private Microsoft.Win32.RegistryKey __getkey( string name )
		{
			Microsoft.Win32.RegistryKey k = this.rootkey.CreateSubKey( path  );
			return k;
		}

		private object __getregvalue( string name  )
		{
			Microsoft.Win32.RegistryKey k = this.__getkey( path );
			object o = k.GetValue(name);
			k.Close();
			return o;
		}



		public void __setregkey( string name, object val, object defval )
		{
			Microsoft.Win32.RegistryKey k = this.__getkey( path );
			if ( val==null )
			{
				val = defval;
			}
			k.SetValue( name, val);
			k.Close();
		}

		public void Set( string name, object val)
		{
			Pref p = this.Find( name );
			if (p==null)
			{
				throw new Exception("No such pref: " + name);
			}
			this.__setregkey( p.m_name, val , p.m_defval );
		}


	}

}
