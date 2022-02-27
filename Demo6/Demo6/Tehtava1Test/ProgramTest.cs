using NUnit.Framework;
using static Tehtava1;

	[TestFixture]
	[DefaultFloatingPointTolerance(0.000001)]
	public  class TestTehtava1
	{
		[Test]
		public  void TestEtaisyys39()
		{
			Assert.AreEqual( 26.196374, Etaisyys(-7, -4, 17, 6.5) , "in method Etaisyys, line 40");
		}
	}

