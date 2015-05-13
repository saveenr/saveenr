using System;
using System.Diagnostics;

namespace geneticfx
{


	public class Generation
	{
		public Population population;
		public int ID;
		public System.DateTime create_date;

		public Generation(int id)
		{
			this.population = null;
			this.ID = id;
			this.create_date = System.DateTime.Now;
		}

	}

	public class MinimizeFitnessComparer : System.Collections.IComparer 
	{
		
		public int Compare( Object x, Object y )  
		{
			Organism g0 = (Organism) x;
			Organism g1 = (Organism) y;
			return g0.Fitness.CompareTo( g1.Fitness );
		}

	}

	public class MaximizeFitnessComparer : System.Collections.IComparer 
	{
		
		public int Compare( Object x, Object y )  
		{
			Organism g0 = (Organism) x;
			Organism g1 = (Organism) y;
			return g1.Fitness.CompareTo( g0.Fitness );
		}

	}

	public enum FitnessDirection { Maximize, Minimize };

	public class Environment
	{
		public delegate void EventHandlerDelegate( Generation sender, System.EventArgs e);
		public event EventHandlerDelegate StartGeneration;
		public event EventHandlerDelegate EndGeneration;

		public Generation CurrentGeneration;
		
		public int generations_maximum = 100;
		public float CrossoverRate = 0.5F;
		public float MutationRate = 0.01F;
		private FitnessDirection direction;

		private static MinimizeFitnessComparer m_min_comparer = new MinimizeFitnessComparer();
		private static MaximizeFitnessComparer m_max_comparer = new MaximizeFitnessComparer();
		public static SequenceNumberGenerator generation_id_generator = new SequenceNumberGenerator(0);
		private static System.Random rand = new Random();

		public Environment( )
		{
			this.CurrentGeneration = null;
		}

		public void SetupForEvolution(Population the_population, FitnessDirection fd )
		{
			this.direction = fd;
			this.CurrentGeneration = new Generation(0);
			this.CurrentGeneration.population  = the_population ;
						

			if ( this.CurrentGeneration.population.IsEmpty )
			{
				throw new GeneticError("Cannot start with empty population");
			}
		}

		public void EvolveNextGeneration()
		{
			Generation old_generation  = this.CurrentGeneration;
			this.StartGeneration( this.CurrentGeneration , null );
			this.StepThroughGeneration();
			this.EndGeneration( old_generation , null );
		}

		protected void StepThroughGeneration()
		{

			for (int i=0;i<this.CurrentGeneration.population.Size;i++)
			{
				Organism o = this.CurrentGeneration.population[ i ];
				if (o.GenerationID==-1)
				{
					o.GenerationID =this.CurrentGeneration.ID ;
				}

			}
			this.SortPopulation( this.CurrentGeneration.population , this.direction );
			this.MakeNextGenerationThroughCrossover();
		}

		public void MakeNextGenerationThroughCrossover()
		{

			Generation next_generation = new Generation( Environment.generation_id_generator.GetUniqueID() );
			next_generation.population = new Population( this.CurrentGeneration.population.Capacity );
			
			Organism best_organism = this.FindBestOrganismInPopulation( this.CurrentGeneration.population );
			next_generation.population.AddOrganism( best_organism );

			while (!next_generation.population.IsFull)
			{
				geneticfx.Population offspring_population = new geneticfx.Population( this.CurrentGeneration.population.Capacity );
				Organism parent0 = this.SelectForCrossover();

				bool perform_crossover = Environment.rand.NextDouble() < this. CrossoverRate;
				
				if ( perform_crossover )
				{
					Organism parent1 = this.SelectForCrossover();
					Organism child0;
					Organism child1;
					this.Crossover( parent0, parent1, out child0, out child1);
					
					child0.GenerationID = next_generation.ID;
					offspring_population.AddOrganism(child0);

					child1.GenerationID = next_generation.ID;
					offspring_population.AddOrganism(child1);

				}
				else
				{
					offspring_population.AddOrganism( parent0  );
				}

				for (int i=0;i<offspring_population.Size;i++)
				{
					if (next_generation.population.IsFull)
					{
						break;
					}

					Organism child = offspring_population[ i ];
					for (int gi=0;gi<child.Genes.GetLength();gi++)
					{
						bool perform_mutation = Environment.rand.NextDouble() < this.MutationRate ;
						if ( perform_mutation )
						{
							child.Genes.MutateGene(gi);
						}
					}
					child.Fitness = child.Genes.CalculateFitness();
					next_generation.population.AddOrganism( child );
				}

			}

			if ( this.CurrentGeneration.population.Size != next_generation.population.Size )
			{
				throw new GeneticError("Population sizes do not match");
			}
			this.CurrentGeneration = next_generation;
		}

		Organism SelectForCrossover()
		{
			return this.tournament();
		}

		Population GetRandomMembersFromPopulation( Population p, int num)
		{
			Population organisms = new Population( p.Capacity );
			for (int i=0;i<num;i++)
			{
				int index = p.GetIndexOfRandomMember( );
				Organism p0 = p[ index ];
				organisms.AddOrganism( p0 );
			}
			return organisms;
		}


		Organism tournament()
		{
			int num_competitors=8;
			Population competitors = this.GetRandomMembersFromPopulation( this.CurrentGeneration.population , num_competitors);

			this.SortPopulation( competitors, this.direction  );

			float choose_best = 0.90F;
			if ( rand.NextDouble() < choose_best )
			{
				return competitors[ 0 ];
			}
			else
			{
				int index = competitors.GetIndexOfRandomMember();
				Organism p0 = competitors[ index ];
				return p0;
			}

		}

		public Organism FindBestOrganismInPopulation( Population p)
		{
			if ( p.IsEmpty )
			{
				throw new GeneticError("Is empty");
			}

			Organism best_organism = p[ 0 ];
			return best_organism;
		}

		public void SortPopulation( Population p, FitnessDirection fd )
		{
			if ( fd==FitnessDirection.Minimize )
			{
				p.Sort( Environment.m_min_comparer );

				// Double-check the sort
				if (p[ 0 ].Fitness >p[ p.Size-1 ].Fitness )
				{
					throw new GeneticError("Sort did not work");
				}
			}
			else
			{
				p.Sort( Environment.m_max_comparer );

				// Double-check the sort
				if (p[ 0 ].Fitness <p[ p.Size-1 ].Fitness )
				{
					throw new GeneticError("Sort did not work");
				}
			}
		}

		public void Crossover( Organism parent0, Organism parent1, out Organism child0, out Organism child1 )
		{
			child0 = parent0.Clone();
			child1 = parent1.Clone();

			int left;
			int right;
			GeneticUtil.PickCrossOverPoints( parent0.Genes , out left, out right );
			child0.Genes.SwapGenesBetween( left, right, child1.Genes );
		}

	}
}
