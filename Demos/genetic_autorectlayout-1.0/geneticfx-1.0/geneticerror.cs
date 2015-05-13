using System;
using System.Diagnostics;

namespace geneticfx
{
	public class GeneticError : System.Exception
	{
		public GeneticError( string msg ) :
			base(msg)
		{
		}
	}

}
