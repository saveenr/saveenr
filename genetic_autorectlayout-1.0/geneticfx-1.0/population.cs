using System;
using System.Diagnostics;

namespace geneticfx
{
	public class Population
	{
		private System.Collections.ArrayList organisms;
		private int population_limit;
		private static System.Random rand = new Random();

		public Population( int population_limit)
		{
			this.population_limit = population_limit;
			this.organisms = new System.Collections.ArrayList(population_limit);
		}

		public void AddOrganism( Organism p0 )
		{
			GeneticUtil.CHECKNULLARG( p0 );

			if ( this.IsFull )
			{
				throw new GeneticError( "Cannot add an organism to a full population");
			}

			this.organisms.Add( p0 );
		}

		public bool IsEmpty
		{
			get
			{
				return this.Size==0;
			}
		}


		public int GetIndexOfRandomMember( )
		{
			int index = Population.rand.Next( 0, this.Size );
			return index;
		}

		public int Size
		{
			get
			{
				return this.organisms.Count;
			}
		}

		public int Capacity
		{
			get
			{
				return this.population_limit ;
			}
		}

		public bool IsFull
		{
			get
			{
				return this.Size == this.Capacity;
			}
		}

		public Organism this[int n]
		{
			get
			{
				return (Organism) this.organisms[n];
			}
			set
			{
				this.organisms[n]=value;
			}
		}

		public void Sort( System.Collections.IComparer c )
		{
			this.organisms.Sort( c );
		}


	}

}
