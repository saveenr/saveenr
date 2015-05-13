using System;
using System.Diagnostics;

namespace geneticfx
{

	public class SequenceNumberGenerator
	{
		public int id;

		public SequenceNumberGenerator ( int first_id )
		{
			this.id = first_id;
		}

		public int GetUniqueID()
		{
			int the_id = this.id;
			this.id ++;
			return the_id;
		}
	}

}
