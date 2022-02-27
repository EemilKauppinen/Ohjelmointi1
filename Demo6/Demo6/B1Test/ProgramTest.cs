using NUnit.Framework;
using static B1;

	[TestFixture]
	[DefaultFloatingPointTolerance(0.000001)]
	public  class TestB1
	{
		[Test]
		public  void TestSkaalaa19()
		{
			Assert.AreEqual( -1.8, Skaalaa(0.2, -3, 3) , 0.000001, "in method Skaalaa, line 20");
			Assert.AreEqual( 2.0, Skaalaa(0.2, 1, 6)  , 0.000001, "in method Skaalaa, line 21");
			Assert.AreEqual( 1.0, Skaalaa(0.0, 1, 6)  , 0.000001, "in method Skaalaa, line 22");
			Assert.AreEqual( 6.0, Skaalaa(1.0, 1, 6)  , 0.000001, "in method Skaalaa, line 23");
		}
	}

