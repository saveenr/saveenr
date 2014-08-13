using System;
using System.Diagnostics;

namespace geneticfx
{

	public class GeneticUtil
	{
		public static void PickCrossOverPoints( IChromosome cs, out int left, out int right )
		{
			System.Random r = new Random();
			int chromosome_length = cs.GetLength();
			left = r.Next(0,chromosome_length-1);
			right = r.Next(left+1,chromosome_length);

			if ( ! ( (0<=left) && (left<chromosome_length-1) ) )
			{
				throw new Exception("error with left");
			}

			if ( ! ( (left<right) && (right<chromosome_length ) ) )
			{
				throw new Exception("error with left");
			}
		}



		public static void RepairCrossover( Organism parent1, Organism parent2)
		{
		}

		public static void CHECKNULLARG( object o )
		{
			if (o==null)
			{
				throw new System.ArgumentNullException();
			}
		}

	}


}
