using System;

namespace geneticfx
{
	public class Organism 
	{
		protected static SequenceNumberGenerator generator = new SequenceNumberGenerator(0);
		protected IChromosome m_chromosome;
		public float Fitness;
		public int ID;
		public int GenerationID;

		public Organism ( IChromosome cs , float fitness )
		{
			GeneticUtil.CHECKNULLARG( cs );
			this.m_chromosome = cs;
			this.Fitness = fitness;
			this.ID = Organism.generator.GetUniqueID();
			this.GenerationID = -1;
		}

		public IChromosome Genes
		{
			get
			{
				return this.m_chromosome;
			}
		}




		public Organism Clone()
		{
			IChromosome cs = this.Genes.Clone();
			Organism the_clone = new Organism( cs , this.Fitness  );

			if ( the_clone.Genes.GetLength() != this.Genes.GetLength() )
			{
				throw  new GeneticError("Cloned Chromosomes of diffetent lengths");
			}

			return the_clone;
		}

	}

}
