using NUnit.Framework;
using static B3;

	[TestFixture]
	[DefaultFloatingPointTolerance(0.000001)]
	public  class TestB3
	{
		[Test]
		public  void TestSamat44()
		{
			Assert.AreEqual( false, Samat(7.1001, 7.1002) , "in method Samat, line 45");
			Assert.AreEqual( true, Samat(7.1001, 7.2002, 0.01) , "in method Samat, line 46");
			Assert.AreEqual( true, Samat(7.1001, 7.2002, 0.2 , "in method Samat, line 47");
		}
	}

