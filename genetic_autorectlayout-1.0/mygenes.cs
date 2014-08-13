using System;
using Gfx;

namespace autorectlayout
{

	public struct  MyGene
	{
		public int side;
		public double q;

		public MyGene(int a,double b)
		{
			MyChromosome.CHECK_SIDE( a );
			MyChromosome.CHECK_Q( b );
			this.side=a;
			this.q=b;
		}

	}

	public class MyChromosome : geneticfx.IChromosome
	{
		public System.Collections.ArrayList genes;
		private static Random r = new System.Random();
		public System.Collections.ArrayList original_rects=null;
		public RECT [] layout_rects;

		public MyChromosome( System.Collections.ArrayList rects )
		{
			if ( rects.Count<2)
			{
				throw new Exception("rects must not be empty");
			}
			this.original_rects = rects;
			this.layout_rects = new RECT[ this.original_rects.Count ];
			this.genes = new System.Collections.ArrayList( this.original_rects.Count-1 );
			for (int i=0;i<this.original_rects.Count-1;i++)
			{
				this.genes.Add( new MyGene(0,0.5) );
			}
		}
		public void SwapGenesBetween( int first_index, int last_index, geneticfx.IChromosome other_chromosome )
		{
			MyChromosome ocs = (MyChromosome) other_chromosome;
			for (int i=first_index;i<=last_index;i++)
			{

				MyGene swap = (MyGene) this.genes[ i ];
				this.genes[ i ] = ocs.genes[ i];
				ocs.genes[ i] = swap;
			}
		}

		public int Length
		{
			get
			{
				return genes.Count;
			}
		}

		public int GetLength()
		{
			return this.Length;
		}


		public geneticfx.IChromosome Clone()
		{
			MyChromosome the_clone = new MyChromosome( this.original_rects );
			int i=0;
			foreach ( MyGene g in this.genes )
			{
				the_clone.genes[i] = g;
				i++;
			}
			return the_clone;
		}

		public void MutateGene(int index)
		{
			double rate = 0.5;
			MyGene g = (MyGene) this.genes[index];
			if ( r.NextDouble() >rate ) 
			{
				g.side = MyChromosome.get_random_side();
			}
			else
			{
				g.q= MyChromosome.get_random_q();
			}
			this.genes[index] = g;
		}

		public static int get_random_side()
		{
			return r.Next(0,4);
		}

		public static double get_random_q()
		{
			return (r.NextDouble()*2.0)-1.0;
		}

		private void layout_rects_from_genes()
		{
			for (int i=0;i<this.original_rects.Count;i++)
			{
				this.layout_rects[i]=(RECT)this.original_rects[i];
			}

			for (int i=0;i<genes.Count;i++)
			{
				MyGene g = (MyGene) this.genes[i];
				this.layout_rects[i+1] = RECTTOOLS.relative_reposition( this.layout_rects[i], this.layout_rects[i+1],g.side,g.q );
			}

			RECT bb = RECTTOOLS.get_bounding_box( this.layout_rects );
			for (int i=0;i<this.layout_rects.Length;i++)
			{
				RECT r = this.layout_rects[i];
				r.x0 = r.x0 - bb.x0;
				r.y0 = r.y0 - bb.y0;

				System.Diagnostics.Debug.Assert( r.x0 >= 0.0 );
				System.Diagnostics.Debug.Assert( r.y0 >= 0.0 );
			}
		}
		
		public float CalculateFitness( )
		{
			this.layout_rects_from_genes();

			int overlaps=0;
			for (int i=0;i<this.layout_rects.Length-1;i++)
			{
				for (int j=0;j<=i;j++)
				{
					if ( i!=j )
					{
						if ( this.layout_rects[i].overlaps(this.layout_rects[j]) )
						{
							overlaps++;
						}
					}
				}
			}
			double overlap_fitness = (double) overlaps/ (double) (this.layout_rects.Length* this.layout_rects.Length) ;

			RECT bb=this.layout_rects[0];
			foreach (RECT r in this.layout_rects)
			{
				bb = RECTTOOLS.get_bounding_box( bb, r );
			}

			double actual_area = RECTTOOLS.get_union_of_area( this.layout_rects );
			double bb_area = (bb.w * bb.h );
			double area_fitness = (bb_area - actual_area)/bb_area;
			double ar_fitness = System.Math.Abs( bb.h - bb.w );

			System.Diagnostics.Debug.Assert( (0<=overlap_fitness) && (overlap_fitness<=1.1) );
			System.Diagnostics.Debug.Assert( (0<=area_fitness) && (area_fitness<=1.1) );
			if ( overlaps>3)
			{
				area_fitness = 1.0;
			}

			//double fitness = (0.5) * overlap_fitness + (0.25) * area_fitness * (0.25) * ar_fitness;
			double fitness = overlap_fitness;
			return (float) fitness; 
		}

		public void RandomizeGenes()
		{
			for (int i=0;i<genes.Count;i++)
			{
				MyGene g = (MyGene) this.genes[i];
				int new_side = r.Next(0,4);
				double new_q = (r.NextDouble()*2.0)-1.0;
				MyGene new_g;
				new_g.side = new_side;
				new_g.q = new_q;
				this.genes[i] = new_g;
			}
		}

		public string GetGeneString()
		{
			System.Text.StringBuilder sb = new System.Text.StringBuilder( this.Length );
			foreach (MyGene g in this.genes)
			{
				sb.Append( string.Format( "({0},{1:0.#})", g.side,g.q ) );
			}
			string s = sb.ToString();
			return s;
		}

		public static bool is_valid_q( double q)
		{
			return ( (-1.0 <= q) && ( q<=1.0) ) ;
		}

		public static bool is_valid_side( int side )
		{
			return ( (0<=side) && (side<=3) );
		}

		public static void CHECK_SIDE( int side )
		{
			if ( ! MyChromosome.is_valid_side(side) )
			{
				throw new Exception( "side is out of range" );
			}

		}

		public static void CHECK_Q( double q )
		{
			if ( ! MyChromosome.is_valid_q( q ) )
			{
				throw new Exception( "Relative q is out of range" );
			}
		}
	}


}
