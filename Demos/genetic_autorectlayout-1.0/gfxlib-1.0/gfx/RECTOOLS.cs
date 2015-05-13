using System;

namespace Gfx
{

	public class RECTTOOLS
	{
		private static double displace( double v0, double v1, double q)
		{
			double v = v0/2.0;
			v -= v1/2.0;
			v += (q/2.0) *(v0+v1);
			return v;
		}

		public static RECT relative_reposition( RECT r0, RECT r1, int side, double q)
		{
			RECT r2 = r1;
			double d=2.0;
			if ( side == 0 ) 
			{
				r2.x0 = r0.x0 + displace( r0.w, r1.w, q ) ;
				r2.y0 = r0.y0 - r1.h + d;
			}
			else if (side == 1 ) 
			{
				r2.x0 = r0.x0 - r1.w + d;
				r2.y0 = r0.y0 + displace( r0.h, r1.h, q );
			}
			else if ( side == 2 ) 
			{
				r2.x0 = r0.x0 + displace( r0.w, r1.w, q ) ;
				r2.y0 = r0.y0 + r0.h + d;
			}
			else if  (side == 3 ) 
			{
				r2.x0 = r0.x0 + r0.w + d;
				r2.y0 = r0.y0 + displace( r0.h, r1.h, q );
			}
			return r2;

		}

		public static RECT get_bounding_box( RECT r0, RECT r1 )
		{
			double x0 = System.Math.Min( r0.x0, r1.x0 );
			double y0 = System.Math.Min( r0.y0, r1.y0 );

			double x1 = System.Math.Max( r0.x1, r1.x1 );
			double y1 = System.Math.Max( r0.y1, r1.y1);

			RECT bb = RECT.FromPoints(x0,y0,x1,y1);
			return bb;
		}


		public static RECT get_bounding_box( RECT [] ra)
		{
			RECT bb = ra[0];
			for (int i=0;i<ra.Length;i++)
			{
				bb = RECTTOOLS.get_bounding_box( bb, ra[i] );
			}
			return bb;
		}

		public static bool is_sliver( RECT r )
		{
			return ( (r.w<1.0) || (r.h<1.0) );
		}


		public static int decompose_overlapping( RECT A, RECT B , RECT [] ra)
		{
			int overlaps=0;
			RECT C;
			RECT i;
			if ( A.overlaps_interior( B ) )
			{

				if ( B.y0 < A.y0 ) 
				{
					//0
					C = new RECT( B.x0, B.y0, B.w,  A.y0-B.y0);
					i = C.intersection(B);
					if ( ! is_sliver(i) )
					{
						ra[ overlaps ] = i;
						RECTTOOLS.CHECK_NO_INTERIOR_OVERLAP( C, A );
						overlaps++;
					}
				}
				if ( B.x1 > A.x1) 
				{
					//1
					C = RECT.FromPoints( A.x1, A.y0, B.x1, A.y1 );
					i = C.intersection(B);
					if ( ! is_sliver(i)  )
					{

						RECTTOOLS.CHECK_NO_INTERIOR_OVERLAP( C, A );
						ra[overlaps] = i;
						overlaps++;
					}
				}
				if ( B.x0 < A.x0 ) 
				{
					//2
					C = RECT.FromPoints( B.x0, A.y0, A.x0, A.y1);
					i = C.intersection(B);
					if ( ! is_sliver(i)  )
					{

						RECTTOOLS.CHECK_NO_INTERIOR_OVERLAP( C, A );
						ra[overlaps] = i;
						overlaps++;
					}
				}
				if ( B.y1 > A.y1 )
				{
					//3
					C=new RECT( B.x0, A.y1, B.w, B.y1-A.y1 );
					i = C.intersection(B);
					if ( ! is_sliver(i) )
					{
						ra[overlaps]=i;
						overlaps++;
					}
				}



			}

			return overlaps;
		}


		public static void CHECK_NO_INTERIOR_OVERLAP( RECT A, RECT B)
		{
			if ( A.overlaps_interior( B ) )
			{
				RECT i = A.intersection(B);
				if ( (i.w >= 1.0) && (i.h>=1.0) )
				{
					throw new Exception("should not overlap");
				}
			}
		}

		public static System.Collections.ArrayList  decompose_overlapping_rects( RECT [] ra )
		{
			System.Collections.ArrayList good_list = new System.Collections.ArrayList();

			System.Collections.ArrayList input_list = new System.Collections.ArrayList();
			foreach (RECT r in ra)
			{
				input_list.Add( r );
			}


			while( input_list.Count > 0 )
			{
				RECT rB = (RECT) input_list[0];
				input_list.RemoveAt( 0 );

				bool resolved_overlap_conflict=false;
				foreach( RECT rA in good_list )
				{
					if (rA.overlaps_interior(rB))
					{
						RECT i = rA.intersection(rB);
						
						if ( (i.w>=1.0) && ( i.h>=1.0) ) 
						{

							RECT [] overlapping_rects = new RECT[4]; 
							int num_overlaps = RECTTOOLS.decompose_overlapping( rA, rB , overlapping_rects );
							for (int j=0;j<num_overlaps;j++)
							{
								RECT rX = overlapping_rects[j];
								input_list.Add( overlapping_rects[j] );
							}
							resolved_overlap_conflict=true;
							break;
						}
					}
				}
				if (!resolved_overlap_conflict)
				{
					good_list.Add( rB );
				}

			}


			return good_list;

		}

		
		public static double get_union_of_area( RECT [] ra )
		{
			System.Collections.ArrayList LIST = decompose_overlapping_rects( ra );
			double total_area=0.0;
			foreach (RECT r in LIST)
			{
				total_area += r.area ;
			}

			return total_area;
		}
	}

}
