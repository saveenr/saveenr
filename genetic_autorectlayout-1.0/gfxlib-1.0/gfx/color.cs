using System;
using System.Drawing;
using System.Drawing.Imaging;


namespace Gfx
{

	class util
	{
		public static bool in_range( double val, double min, double max )
		{
			return ( (min<=val) && (val<=max) );
		}
	}


	public struct HSVColor
	{


		public double Hue,Saturation,Value;


		public const double MaxHue=1.0;
		public const double MinHue=0.0;
		public const double MaxSaturation=1.0;
		public const double MinSaturation=0.0;
		public const double MaxValue=1.0;
		public const double MinValue=0.0;

		public override string ToString()
		{
			return string.Format("HSV({0},{1},{2})",this.Hue,this.Saturation,this.Value);
		}

		static void check_hsv( double _h,double _s,double _v )
		{
			if ( !util.in_range( _h, HSVColor.MinHue  , HSVColor.MaxHue ) )
			{
				throw new System.ArgumentException("Bad Hue");
			}
			if ( !util.in_range( _s, HSVColor.MinSaturation  , HSVColor.MaxSaturation) )
			{
				throw new System.ArgumentException("Bad Saturation");
			}
			if ( !util.in_range( _v, HSVColor.MinValue, HSVColor.MaxValue) )
			{
				throw new System.ArgumentException("Bad Value");
			}
		}

		public HSVColor( double hue ,double saturation ,double val)
		{
			HSVColor.check_hsv( hue,saturation,val);
			this.Hue = hue;
			this.Saturation = saturation;
			this.Value = val;

		}


		public void Set( double hue ,double saturation ,double val)
		{
			HSVColor.check_hsv( hue,saturation,val);
			this.Hue = hue;
			this.Saturation = saturation;
			this.Value = val;
		}


		public RGBColor GetRGB( )
		{
			RGBColor rgb_color = new RGBColor();

			
			if (this.Saturation==0)
			{
				rgb_color.Set( this.Value, this.Value, this.Value);
			}
			else
			{
				
				if ( this.Hue  == HSVColor.MaxHue )
				{
					this.Hue = HSVColor.MinHue;
				}

				double step =  HSVColor.MaxHue / 6;

				double xh = this.Hue / step;
				int i = (int) System.Math.Floor( xh );
				double f = xh - i;
				double p = this.Value * ( 1 - this.Saturation );
				double q = this.Value * ( 1 - ( this.Saturation * f ) );
				double t = this.Value * ( 1 - ( this.Saturation * (1-f) ) );

				switch (i)
				{
					case (0) :
					{
						rgb_color.Set( this.Value, t, p );
						break;
					}
					case (1) :
					{
						rgb_color.Set( q, this.Value, p );
						break;
					}
					case (2) :
					{
						rgb_color.Set( p , this.Value, t);
						break;
					}
					case (3) :
					{
						rgb_color.Set( p, q, this.Value );
						break;
					}
					case (4) :
					{
						rgb_color.Set( t, p , this.Value );
						break;
					}
					case (5) :
					{
						rgb_color.Set( this.Value,  p, q );
						break;
					}
					default :
					{
						throw new System.Exception("Unhandled Case");
					}

				}
				
			}

			return rgb_color;

		}

		public string GetWebColorString()
		{
			RGBColor c=this.GetRGB();
			return c.GetWebColorString();
		}

	}

	public struct RGBColor
	{
		public double r,g,b;

		public static double max_red=1.0;
		public static double min_red=0.0;
		public static double max_blue=1.0;
		public static double min_blue=0.0;
		public static double max_green=1.0;
		public static double min_green=0.0;

		static void check_rgb( double _r,double _g,double _b )
		{
			if ( !util.in_range( _r, RGBColor.min_red, RGBColor.max_red) )
			{
				throw new System.ArgumentException("Bad Red");
			}
			if ( !util.in_range( _g, RGBColor.min_green  , RGBColor.max_green ) )
			{
				throw new System.ArgumentException("Bad Green");
			}
			if ( !util.in_range( _b, RGBColor.min_blue, RGBColor.max_blue) )
			{
				throw new System.ArgumentException("Bad Blue");
			}
		}

		public override string ToString()
		{
			return string.Format("RGB({0},{1},{2})",r,g,b);
		}


		public RGBColor( double _r,double _g,double _b)
		{
			this.r = _r;
			this.g = _g;
			this.b = _b;
		}

		public void Set( double _r,double _g,double _b)
		{
			this.r = _r;
			this.g = _g;
			this.b = _b;
		}

		public System.Drawing.Color GetColor()
		{
			return System.Drawing.Color.FromArgb( (int) (this.r * 255), (int) (this.g* 255), (int) (this.b* 255) );
		}

		public HSVColor GetHSV( )
		{
			double _max;
			_max = System.Math.Max( this.r, this.g );
			_max = System.Math.Max( _max, this.b );

			double _min;
			_min = System.Math.Min( this.r, this.g );
			_min = System.Math.Min( _max, this.b );

			double the_h = HSVColor.MinHue;
			double the_s = HSVColor.MinSaturation;
			double the_v = _max;


			double delta = _max - _min;

			if (_max == 0.0)
			{
				the_s = 0.0;
			}
			else
			{
				the_s = delta / _max;

				if ( this.r == _max )
				{
					the_h = ( this.g - this.b ) / delta;
				}
				else if ( this.g == _max )
				{
					the_h = 2.0 + ( this.b - this.r ) / delta;
				}
				else if ( this.b == _max )
				{
					the_h = 4.0 + ( this.r - this.g ) / delta;
				}
				the_h *= 60.0;
				if (the_h<0)
				{
					the_h += 360;
				}

				the_h /= 360.0;
			}

			return new HSVColor( the_h, the_s, the_v);
		}

		public void GetRGBBytes( out byte r, out byte g, out byte b)
		{
			RGBColor.check_rgb( this.r, this.g, this.b );
			r = (byte) (this.r*255);
			g = (byte) (this.g*255);
			b = (byte) (this.b*255);
		}

		public string GetWebColorString()
		{
			byte rbyte,gbyte,bbyte;
			this.GetRGBBytes( out rbyte, out gbyte, out bbyte );
			string format_string = "#{0:x2}{1:x2}{2:x2}";
			string color_string = string.Format( format_string , rbyte,gbyte,bbyte);
			return color_string;
		}

	}

}
