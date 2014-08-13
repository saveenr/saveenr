using System;
namespace autorectlayout
{

	public class MyChromosome : geneticfx.IChromosome
	{
		private System.Collections.BitArray genes;
		private static Random r = new System.Random();
		private const int default_length = 16;

		public MyChromosome( )
		{
			// create a chromosome of bits set to zero
			this.genes = new System.Collections.BitArray( default_length, false );
		}
		public void SwapGenesBetween( int first_index, int last_index, geneticfx.IChromosome other_chromosome )
		{
			// swap genes between two chromosomes
			MyChromosome ocs = (MyChromosome) other_chromosome;
			for (int i=first_index;i<=last_index;i++)
			{
				bool swap = this.genes[ i ];
				this.genes[ i ] = ocs.genes[ i];
				ocs.genes[ i] = swap;
			}
		}

		public int GetLength()
		{
			// return the number of bits
			return this.genes.Length;
		}


		public geneticfx.IChromosome Clone()
		{
			// create a by-value copy of the object
			MyChromosome the_clone = new MyChromosome();
			int i=0;
			foreach ( bool v in this.genes )
			{
				the_clone.genes[i] = v;
				i++;
			}
			return the_clone;
		}

		public void MutateGene(int index)
		{
			// flip the bit
			this.genes[index] = !this.genes[index];
		}

		
		public float CalculateFitness( )
		{
			// return the number of bits that are set to 1
			int v=0;
			for (int i=0;i<genes.Length;i++)
			{
				if ( this.genes[i] ) { v++; }
			}
			return ((float)v); 
		}

		public void RandomizeGenes()
		{
			// randomly set all the bits
			for (int i=0;i<this.genes.Length;i++)
			{
				int x = r.Next(0,2);
				bool bit = (x != 0);
				this.genes[i] = bit ;
			}
		}

		public string GetGeneString()
		{
			// get a string representation of the genes
			System.Text.StringBuilder sb = new System.Text.StringBuilder( this.GetLength() );
			char on_char = '1';
			char off_char = '-';
			foreach (bool bit in this.genes)
			{
				if ( bit ) { sb.Append( on_char )  ; }
				else { sb.Append( off_char )  ; }
			}
			string s = sb.ToString();
			return s;
		}
	}


	class MaximizeBitArray
	{

		public static void StartGenerationHandler( geneticfx.Generation g, System.EventArgs e )
		{
			System.Console.WriteLine(">Generation: ID={0}", g.ID  );
			System.Console.WriteLine("\t>Population Size: {0}", g.population.Size );

			for (int i=0;i<g.population.Size;i++)
			{
				geneticfx.Organism o = g.population[ i ];
				System.Console.WriteLine("\t\t\t--------");
				System.Console.WriteLine("\t\t\torg ID  {0}", o.ID );
				System.Console.WriteLine("\t\t\tge      {0}",o.GenerationID);
				System.Console.WriteLine("\t\t\tfitness {0}", o.Fitness);
				System.Console.WriteLine("\t\t\tgenes   {0}",o.Genes.GetGeneString() );

			}
		}

		public static void EndGenerationHandler( geneticfx.Generation g, System.EventArgs e )
		{
			System.Console.WriteLine("End {0}",g.ID );
		}


		[STAThread]
		static void Main(string[] args)
		{
			geneticfx.Environment env = new geneticfx.Environment( );
			env.StartGeneration+= new geneticfx.Environment.EventHandlerDelegate( MaximizeBitArray.StartGenerationHandler );
			env.EndGeneration+= new geneticfx.Environment.EventHandlerDelegate( MaximizeBitArray.EndGenerationHandler );

			geneticfx.Population initial_population = new geneticfx.Population( 9 );

			for (int i=0;i<initial_population.Capacity;i++)
			{
				MyChromosome cs = new MyChromosome();
				cs.RandomizeGenes();
				geneticfx.Organism o = new geneticfx.Organism( cs, 0.0F );
				initial_population.AddOrganism(o);
			}

			env.Run( initial_population , geneticfx.FitnessDirection.Maximize);

		}
	}
}
