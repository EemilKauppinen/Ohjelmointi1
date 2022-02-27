using NUnit.Framework;
using static B2;

	[TestFixture]
	[DefaultFloatingPointTolerance(0.000001)]
	public  class TestB2
	{
		[Test]
		public  void TestSuurin17()
		{
			Assert.AreEqual( 4, Suurin(new double[,] { { 1, 2, 3 }, { 2, 2, 2 }, { 4, 2, 3 } }) , "in method Suurin, line 18");
			Assert.AreEqual( 19, Suurin(new double[,] { { 9, 2, 8 }, { 1, 2, 5 }, { 3, 19, -3 } }) , "in method Suurin, line 19");
		}
	}

