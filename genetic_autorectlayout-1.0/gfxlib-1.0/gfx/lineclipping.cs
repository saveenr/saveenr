using System;
using System.Drawing;

namespace Gfx
{

	public class LineClipping
	{
		private LineClipping()
		{
		}

		/*
		 * Copied from Section 4.3 of Ammeraal, L. (1998) Computer Graphics for Java Programmers,
			Chichester: John Wiley.

		 */

		protected static int clip_code( int x, int y, int xmin, int ymin, int xmax, int ymax )
		{
			return (
				(x < xmin ? 8 : 0) | 
				(x > xmax ? 4 : 0) |
				(y < ymin ? 2 : 0) | 
				(y > ymax ? 1 : 0) );
		}

		public static bool ClipLine( ref Point p1, ref Point p2, Rectangle r)
		{
			int x1=p1.X ,y1=p1.Y,x2=p2.X ,y2=p2.Y;
			bool d = LineClipping.internal_clip_line( 
				ref x1, ref y1 , 
				ref x2, ref y2 , 
				r.Left ,r.Top , r.Right ,r.Bottom );
			p1.X = x1;
			p1.Y = y1;
			p2.X = x2;
			p2.Y = y2;
			return d;
		}

		protected static bool internal_clip_line( ref int xP, ref int yP, ref int xQ, ref int yQ,
			int xmin, int ymin, int xmax, int ymax)
		{
			int cP = clip_code(xP, yP,xmin,ymin,xmax,ymax);
			int cQ = clip_code(xQ, yQ,xmin,ymin,xmax,ymax);
			int dx, dy;
			while ((cP | cQ) != 0)
			{
				if ((cP & cQ) != 0) return false;
				dx = xQ - xP; dy = yQ - yP;
				if (cP != 0)
				{
					if ((cP & 8) == 8)
					{
						yP += (xmin-xP) * dy / dx;
						xP = xmin;
					}  
					else if ((cP & 4) == 4)
					{
						yP += (xmax-xP) * dy / dx;
						xP = xmax;
					}  
					else if ((cP & 2) == 2)
					{
						xP += (ymin-yP) * dx / dy;
						yP = ymin;
					}  
					else if ((cP & 1) == 1)
					{
						xP += (ymax-yP) * dx / dy;
						yP = ymax;
					}  
					cP = clip_code(xP, yP,xmin,ymin,xmax,ymax);
				}  
				else if (cQ != 0)
				{
					if ((cQ & 8) == 8)
					{
						yQ += (xmin-xQ) * dy / dx; 
						xQ = xmin;
					}  
					else if ((cQ & 4) == 4)
					{
						yQ += (xmax-xQ) * dy / dx; 
						xQ = xmax;
					}  
					else if ((cQ & 2) == 2)
					{
						xQ += (ymin-yQ) * dx / dy;
						yQ = ymin;
					}  
					else if ((cQ & 1) == 1)
					{
						xQ += (ymax-yQ) * dx / dy; 
						yQ = ymax;
					}  
					cQ = clip_code(xQ, yQ,xmin,ymin,xmax,ymax);
				}  
			}
			return true;
		}         



	}


}
