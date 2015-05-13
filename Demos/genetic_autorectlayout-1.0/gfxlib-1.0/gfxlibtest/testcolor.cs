using System;
using NUnit.Framework;

namespace Gfxtest
{

	[TestFixture]
	public class TestColor
	{
		public Gfx.HSVColor CreateHSVColor( double h, double s, double v )
		{
			Gfx.HSVColor c = new Gfx.HSVColor(h,s,v);
			Assertion.AssertEquals( h , c.Hue );
			Assertion.AssertEquals( v , c.Value );
			Assertion.AssertEquals( s , c.Saturation );
			return c;
		}

		[Test]
		public void CreateLegalColors()
		{
			Gfx.HSVColor c1 = this.CreateHSVColor(0,0,0);
			Gfx.HSVColor c2 = this.CreateHSVColor(1,1,1);
			Gfx.HSVColor c3 = this.CreateHSVColor(0.1,0.5,0.7);

			
			Gfx.RGBColor c4 = new Gfx.RGBColor(1,0,0);
			Assertion.AssertEquals( 255, c4.GetColor().R );
			Assertion.AssertEquals( 0, c4.GetColor().G );
			Assertion.AssertEquals( 0, c4.GetColor().B );

		}

		[Test]
		[ExpectedException(typeof(System.ArgumentException))]
		public void CreateBadColor1()
		{
			Gfx.HSVColor c1 = this.CreateHSVColor(0,0,-1);
		}

		[Test]
		[ExpectedException(typeof(System.ArgumentException))]
		public void CreateBadColor2()
		{
			Gfx.HSVColor c1 = this.CreateHSVColor(0,-1,0);
		}

		[Test]
		[ExpectedException(typeof(System.ArgumentException))]
		public void CreateBadColor3()
		{
			Gfx.HSVColor c1 = this.CreateHSVColor(-1,0,0);
		}

		[Test]
		[ExpectedException(typeof(System.ArgumentException))]
		public void CreateBadColor4()
		{
			Gfx.HSVColor c1 = this.CreateHSVColor(0,0,2);
		}

		[Test]
		[ExpectedException(typeof(System.ArgumentException))]
		public void CreateBadColor5()
		{
			Gfx.HSVColor c1 = this.CreateHSVColor(0,2,0);
		}

		[Test]
		[ExpectedException(typeof(System.ArgumentException))]
		public void CreateBadColor6()
		{
			Gfx.HSVColor c1 = this.CreateHSVColor(2,0,0);
		}

	}
}
