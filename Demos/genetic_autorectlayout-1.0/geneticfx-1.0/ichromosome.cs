using System;
using System.Diagnostics;

namespace geneticfx
{


	public interface IChromosome
	{
		void SwapGenesBetween( int first_index, int last_index, IChromosome other_genes );
		IChromosome Clone();
		int GetLength();
		void MutateGene( int index );
		float CalculateFitness();
		void RandomizeGenes();
		string GetGeneString();

	}


}
