using System;

namespace Gfx
{

	public struct RectangleD 
	{

		PointD p0;
		SizeD size;


		public RectangleD( double x, double y, double w, double h )
		{
			this.p0.X=x;
			this.p0.Y=y;
			this.size.Width = w;
			this.size.Height =h;
		}

		public RectangleD( double x, double y, SizeD size )
		{
			this.p0.X=x;
			this.p0.Y=y;
			this.size = size;
		}

		public double X0
		{
			get { return this.p0.X; }
			set { this.p0.X = value; }
		}

		public double Y0
		{
			get { return this.p0.Y; }
			set { this.p0.Y= value; }
		}


		public double Y1
		{
			get { return this.Y0 + this.Size.Height; }
		}

		public double X1
		{
			get { return this.X0+ this.Size.Width;}
		}

		public SizeD Size
		{
			get 
			{
				return this.size;
			}
			set
			{
				this.size = value;
			}
		}

		public double Width
		{
			get 
			{
				return this.size.Width;
			}
			set
			{
				this.size.Width = value;
			}
		}

		public double Height
		{
			get 
			{
				return this.size.Height;
			}
			set
			{
				this.size.Height = value;
			}
		}

		public void Translate( double dx, double dy )
		{
			this.p0.Translate(dx,dy);
		}

		public void Scale( double sx, double sy )
		{
			this.p0.Scale(sx,sy);
			this.Size.Scale(sx,sy);
		}

		public PointD [] GetCorners()
		{
			PointD [] corners = new PointD[4];
			corners[0]=new PointD( this.p0.X, this.p0.Y );
			corners[1]=new PointD( this.p0.X+this.Size.Width, this.p0.Y );
			corners[2]=new PointD( this.p0.X+this.Size.Width, this.p0.Y + this.Size.Height);
			corners[3]=new PointD( this.p0.X, this.p0.Y + this.Size.Height);
			return corners;
		}

		public static RectangleD FitInRectangle( double w, double h, RectangleD b)
		{
			double scaling_factor = Math2D.GetFitToAreaScalingFactor( w, h, b.Size.Width, b.Size.Height );

			double new_height = h * scaling_factor ;
			double new_width = w * scaling_factor ;
			return new RectangleD(0,0,new_width,new_height);
		}
	}

}
