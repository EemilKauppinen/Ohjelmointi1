using NUnit.Framework;
using static Tehtava1;

	[TestFixture]
	[DefaultFloatingPointTolerance(0.000001)]
	public  class TestTehtava1
	{
		[Test]
		public  void TestPisinNouseva16()
		{
			Assert.AreEqual( 4, PisinNouseva(new int[]{ 1,5,12,89,64,77,1000}) , "in method PisinNouseva, line 17");
			Assert.AreEqual( 1, PisinNouseva(new int[]{ 0,0,0,0,0,0,0}) , "in method PisinNouseva, line 18");
			Assert.AreEqual( 3, PisinNouseva(new int[]{ -1,-7,0,8,7,6}) , "in method PisinNouseva, line 19");
			Assert.AreEqual( 1, PisinNouseva(new int[]{ 1}) , "in method PisinNouseva, line 20");
		}
	}

