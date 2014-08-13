using System;
using NUnit.Framework;
using Gfx;

namespace Gfxtest
{

	[TestFixture]
	public class TestRECT
	{

		[Test]
		public void TestEquality()
		{
			RECT r0 = new RECT( 0,0,10,10);
			RECT r1 = r0;
			Assertion.AssertEquals( r0, r1 );
		}

		public void TestStructness()
		{
			RECT r0 = new RECT( 0,0,10,10);
			RECT r1 = r0;
			r1.x0 = 100;
			Assertion.Assert( !r0.Equals( r1)  );
			Assertion.AssertEquals( r0.x0 , 0 );
		}

		[Test]
		public void TestOverlap1()
		{
			RECT r0 = new RECT( 0,0,10,10);
			RECT r1 = new RECT( 10,10,20,20);
			RECT r2 = new RECT( 11,11,20,20);
			RECT r3 = new RECT( 9.999,9.999,20,20);
			Assertion.Assert( r1.overlaps(r0) );
			Assertion.Assert( !r1.overlaps_interior(r0) );
			Assertion.Assert( !r2.overlaps(r0) );
			Assertion.Assert( r3.overlaps(r0) );
			Assertion.Assert( r3.overlaps_interior(r0) );
		}

		[Test]
		public void TestOverlap2()
		{
			RECT r0 = new RECT( 10,0,10,10);
			RECT r1 = new RECT( 0,0,10,10);
			Assertion.Assert( r1.overlaps(r0) );
			Assertion.Assert( !r1.overlaps_interior(r0) );
		}


		[Test]
		public void TestDecompose()
		{
			RECT rA = new RECT(0,0,10,10);
			RECT rB = new RECT(5,5,15,15);
			RECT rC = new RECT(50,50,10,10);

			RECT [] output = new RECT[4];

			int num_output_rects;

			num_output_rects = RECTTOOLS.decompose_overlapping( rA, rA , output );
			Assertion.AssertEquals( num_output_rects, 0 );

			num_output_rects = RECTTOOLS.decompose_overlapping( rA, rC , output );
			Assertion.AssertEquals( num_output_rects, 0 );

			num_output_rects = RECTTOOLS.decompose_overlapping( rA, rB , output );
			Assertion.AssertEquals( num_output_rects, 2 );

		}

		[Test]
		public void TestMidPoint()
		{
			RECT rA = new RECT(10,10,10,10);
			POINT mp = rA.midpoint();
			Assertion.AssertEquals( mp.x, 15.0 );
			Assertion.AssertEquals( mp.y, 15.0 );

		}

		[Test]
		public void TestIntersection()
		{
			RECT rA = new RECT(10,10,10,10);
			RECT rB = new RECT(0,0,30,30);
			RECT rC = new RECT(15,15,20,20);
			RECT rD = new RECT(0,0,15,15);
			RECT rI;

			rI = rA.intersection( rB );
			Assertion.AssertEquals( rI, rA );

			rI = rA.intersection( rC );
			Assertion.AssertEquals( rI, new RECT( 15,15,5,5 ) );

			rI = rA.intersection( rD );
			Assertion.AssertEquals( rI, new RECT( 10,10,5,5 ) );


		}

		[Test]
		public void TestDecomposeAll()
		{
			RECT [] ra= new RECT[2];
			ra[0] = new RECT(10,10,10,10);
			ra[1]= new RECT(15,15,10,10);

			double area = RECTTOOLS.get_union_of_area( ra );
			Assertion.AssertEquals( area, 25*7.0);
		}

		[Test]
		public void TestArea2()
		{
			RECT [] ra= new RECT[3];
			ra[0] = new RECT(10,10,10,10);
			ra[1]= new RECT(15,15,10,10);
			ra[2] = new RECT(15,15,2,2);

			double area = RECTTOOLS.get_union_of_area( ra );
			Assertion.AssertEquals( area, 25*7.0);
		}

		[Test]
		public void TestArea3()
		{
			RECT [] ra= new RECT[4];
			ra[0] = new RECT(10,10,10,10);
			ra[1]= new RECT(15,15,10,10);
			ra[2] = new RECT(15,15,2,2);
			ra[3] = new RECT(100,100,10,10);

			double area = RECTTOOLS.get_union_of_area( ra );
			Assertion.AssertEquals( ( 25*7.0 ) + 100, area);
		}

		[Test]
		public void TestArea4a()
		{
			RECT [] ra= new RECT[2];
			ra[0] = new RECT(10,10,10,10);
			ra[1] = new RECT(0,0,400,400);

			double area = RECTTOOLS.get_union_of_area( ra );
			Assertion.AssertEquals(( 400.0 * 400.0 ) , area );
		}

		[Test]
		public void TestArea4b()
		{
			RECT [] ra= new RECT[2];
			ra[0] = new RECT(0,0,400,400);
			ra[1] = new RECT(10,10,10,10);

			double area = RECTTOOLS.get_union_of_area( ra );
			Assertion.AssertEquals(( 400.0 * 400.0 ) , area );
		}


		[Test]
		public void TestDX0()
		{
			RECT rA = new RECT(0,0,20,20);
			RECT rB = new RECT(10,10,20,20);
			RECT [] ro = new RECT [3];

			int count = RECTTOOLS.decompose_overlapping( rA, rB, ro );

			Assertion.AssertEquals(2, count );
			Assertion.AssertEquals( new RECT(20,10,10,10), ro[0] );
			Assertion.AssertEquals( new RECT(10,20,20,10), ro[1] );
		}


		[Test]
		public void TestDX1()
		{
			RECT rA = new RECT(0,0,20,20);
			RECT rB = new RECT(10,-10,20,20);
			RECT [] ro = new RECT [4];

			int count = RECTTOOLS.decompose_overlapping( rA,rB, ro );

			Assertion.AssertEquals(2, count );
			Assertion.AssertEquals( new RECT(10,-10,20,10), ro[0] );
			Assertion.AssertEquals( new RECT(20,0,10,10), ro[1] );
		}

		[Test]
		public void TestDX2()
		{
			RECT rA = new RECT(0,0,20,20);
			RECT rB = new RECT(-10,-10,20,20);
			RECT [] ro = new RECT [4];

			int count = RECTTOOLS.decompose_overlapping( rA, rB, ro );

			Assertion.AssertEquals(2, count );
			Assertion.AssertEquals( new RECT(-10,-10,20,10), ro[0] );
			Assertion.AssertEquals( new RECT(-10,0,10,10), ro[1] );
		}

		[Test]
		public void TestDX3()
		{
			RECT rA = new RECT(0,0,20,20);
			RECT rB = new RECT(-10,10,20,20);
			RECT [] ro = new RECT [4];

			int count = RECTTOOLS.decompose_overlapping( rA, rB, ro );

			Assertion.AssertEquals(2, count );
			Assertion.AssertEquals( new RECT(-10,10,10,10), ro[0] );
			Assertion.AssertEquals( new RECT(-10,20,20,10), ro[1] );
		}


	}
}
