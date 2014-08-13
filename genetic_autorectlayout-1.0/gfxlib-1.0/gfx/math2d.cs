using System;

namespace Gfx
{
	public class Math2D
	{


		public static double EuclideanDistance( double a, double b )
		{
			double asqr = a * a;
			double bsqr = b * b;
			double sum = asqr + bsqr;
			double result = System.Math.Sqrt( sum );
			return result;
		}

		public static SizeD FitToArea( SizeD s, SizeD max_s)
		{
			double f = GetFitToAreaScalingFactor( s.Width , s.Height , max_s.Width , max_s.Height );
			SizeD output = new SizeD( s.Width * f, s.Height * f );
			return output;
		}

		public static double GetFitToAreaScalingFactor( SizeD s, SizeD max)
		{
			return GetFitToAreaScalingFactor( s.Width , s.Height , max.Width , max.Height );
		}

		public static double GetFitToAreaScalingFactor( double w1, double h1, double max_width, double max_height )
		{
			if ( (w1<=0) || (h1<=0) || (max_width<=0) || (h1<=0) )
			{
				throw new ArgumentException( );
			}

			double input_aspect_ratio = h1 / w1;
			double bounding_apect_ratio = max_height / max_width;
			double scaling_factor;

			if ( input_aspect_ratio <= bounding_apect_ratio)
			{
				scaling_factor = max_width / w1;
			}
			else
			{
				scaling_factor = max_height / h1;
			}

			// check to make sure scaling factor can be used
			if ( w1 * scaling_factor > max_width + 1)
			{
				throw new Exception("incorrectly calculated scaling factor");
			}
			if ( h1 * scaling_factor > max_height + 1)
			{
				throw new Exception("incorrectly calculated scaling factor");
			}

			return scaling_factor;
		}

		public static RectangleD GetBoundingBox( RectangleD r1, RectangleD r2 )
		{
			double new_x0 = System.Math.Min( r1.X0 , r2.X0 );
			double new_y0 = System.Math.Min( r1.Y0 , r2.Y0 );
			double new_x1 = System.Math.Max( r1.X1 , r2.X1 );
			double new_y1 = System.Math.Max( r1.Y1 , r2.Y1 );
			RectangleD bb = new RectangleD( new_x0, new_y0, new_x1-new_x0, new_y1-new_y0);
			return bb;
		}

		public static PointD GetMidPoint( RectangleD r )
		{
			double x = r.X0  + (r.Size.Width/2.0);
			double y = r.X0 + (r.Size.Height/2.0);
			return new PointD(x,y);
		}

		public static PointD GetMidPoint( PointD a, PointD b )
		{
			double x = (a.X +b.X)/2.0;
			double y = (a.Y +b.Y)/2.0;
			return new PointD(x,y);
		}

	}


}
