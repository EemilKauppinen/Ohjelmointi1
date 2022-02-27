using NUnit.Framework;
using static Tehtava1;

	[TestFixture]
	[DefaultFloatingPointTolerance(0.000001)]
	public  class TestTehtava1
	{
		[Test]
		public  void TestLaskeKirjaimet17()
		{
			LaskeKirjaimet("KOIRA", 'O');
			LaskeKirjaimet("juliUs", 'j');
		}
	}

