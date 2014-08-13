using System;
using Gfx;

namespace autorectlayout
{


	class LayoutRects
	{

		public LayoutRects()
		{
		}

		public void StartGenerationHandler( geneticfx.Generation g, System.EventArgs e )
		{
			System.Console.WriteLine(">Generation: ID={0}", g.ID  );
			System.Console.WriteLine("\t>Population Size: {0}", g.population.Size );

		}


		public void EndGenerationHandler( geneticfx.Generation g, System.EventArgs e )
		{
			System.Console.WriteLine("End {0}",g.ID );
		}

		public void Run()
		{
			geneticfx.Environment env = new geneticfx.Environment( );
			env.StartGeneration+= new geneticfx.Environment.EventHandlerDelegate( this.StartGenerationHandler );
			env.EndGeneration+= new geneticfx.Environment.EventHandlerDelegate( this.EndGenerationHandler );

			geneticfx.Population initial_population = new geneticfx.Population( 50 );

			System.Collections.ArrayList rects = new System.Collections.ArrayList();

			rects.Add( new RECT(0,0,100,100) );
			rects.Add( new RECT(0,0,100,200) );

			rects.Add( new RECT(0,0,200,100) );
			rects.Add( new RECT(0,0,200,200) );
			
			rects.Add( new RECT(0,0,200,100) );
			rects.Add( new RECT(0,0,200,200) );
			
			rects.Add( new RECT(0,0,100,100) );
			rects.Add( new RECT(0,0,100,200) );
			
			rects.Add( new RECT(0,0,100,100) );
			rects.Add( new RECT(0,0,100,200) );
			rects.Add( new RECT(0,0,100,100) );
			rects.Add( new RECT(0,0,100,200) );

			for (int i=0;i<initial_population.Capacity;i++)
			{
				MyChromosome cs = new MyChromosome( rects );
				cs.RandomizeGenes();
				geneticfx.Organism o = new geneticfx.Organism( cs, 0.0F );
				initial_population.AddOrganism(o);
			}

			env.MutationRate = 0.10F;
			int num_generations=10;
			env.SetupForEvolution( initial_population , geneticfx.FitnessDirection.Minimize);

			string fname ="out.svg";
			fname = System.IO.Path.GetFullPath( fname );

			System.Xml.XmlWriter xw = new System.Xml.XmlTextWriter( fname, System.Text.Encoding.UTF8 );
			xw.WriteStartElement("svg");

			int cur_y = 100;
			for (int i=0;i<num_generations;i++)
			{
				env.EvolveNextGeneration();

				geneticfx.Generation generation = env.CurrentGeneration;

				int cur_x = 100;
				for (int generation_index=0;generation_index<generation.population.Size;generation_index++)
				{
					geneticfx.Organism o = generation.population[ generation_index ];
					MyChromosome mcr = (MyChromosome) o.Genes;

					RECT bb = RECTTOOLS.get_bounding_box( mcr.layout_rects );
					xw.WriteStartElement("g");
					xw.WriteAttributeString( "transform", string.Format( "translate({0},{1})", cur_x, cur_y ) );
					for (int icr=0;icr<mcr.Length;icr++)
					{
						RECT r = mcr.layout_rects [icr];
						xw.WriteStartElement("rect");
						xw.WriteAttributeString( "x", r.x0.ToString() );
						xw.WriteAttributeString( "y", r.y0.ToString());
						xw.WriteAttributeString( "width", r.w.ToString());
						xw.WriteAttributeString( "height", r.h.ToString() );
						xw.WriteAttributeString( "opacity", "0.1");
						xw.WriteEndElement();

						xw.WriteStartElement("text");
						xw.WriteAttributeString( "x", "0" );
						xw.WriteAttributeString( "y", "0" );
						string s = string.Format( "Gen{0} / Org{1} / Fit={2}", generation_index,o.ID,o.Fitness );
						xw.WriteString( s );
						xw.WriteEndElement();

					}
					xw.WriteEndElement();
					

					cur_x += (int) (1000 + 100);
				}
				cur_y += (int) (1000 + 100);


				xw.Flush();
			}
			xw.WriteEndElement();
			xw.Flush();
			xw.Close();

		}
	}



	class App
	{


		public static void test()
		{

			RECT rA = new RECT(10,10,10,10);
			RECT rB = new RECT(0,0,30,30);
			RECT [] ro = new RECT [4];

			int count = RECTTOOLS.decompose_overlapping( rA, rB, ro );

			//Assertion.AssertEquals( 1 , count );



		}

		[STAThread]
		static void Main(string[] args)
		{
			App.test();
			LayoutRects x = new LayoutRects();
			x.Run();
		}
	}
}
