using NUnit.Framework;
using static Tehtava1;

	[TestFixture]
	[DefaultFloatingPointTolerance(0.000001)]
	public  class TestTehtava1
	{
		[Test]
		public  void TestPoistaPerattaiset19()
		{
			Assert.AreEqual( true, PoistaPerattaiset((new int[] { 1, 2, 2, 0, 5, 7, 3, 4, 4, 4, 8 }).ToList()).SequenceEqual((new int[] { 1, 2, 0, 5, 7, 3, 4, 8 }).ToList()) , "in method PoistaPerattaiset, line 20");
		}
	}

