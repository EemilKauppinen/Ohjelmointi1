using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using static Sopulit;

	[TestFixture]
	[DefaultFloatingPointTolerance(0.000001)]
	public  class TestSopulit
	{
		[Test]
		public  void TestJonoksi47()
		{
			int[,] luvut = {{1,2,3}, {4,5,6}, {7,8,9}};
			Assert.AreEqual( "1 2 3\n4 5 6\n7 8 9", Sopulit.Jonoksi(luvut) , "in method Jonoksi, line 49");
			Assert.AreEqual( "1 2 3,4 5 6,7 8 9", Sopulit.Jonoksi(luvut," ",",") , "in method Jonoksi, line 50");
			Assert.AreEqual( "[ 1:2:3 ]|[ 4:5:6 ]|[ 7:8:9 ]", Sopulit.Jonoksi(luvut,":","|","[ {0} ]") , "in method Jonoksi, line 51");
		}
		[Test]
		public  void TestMuodostaUusiSukupolvi82()
		{
			int[,] alku = {
			{ 1,0,1,1 },
			{ 0,1,1,0 },
			{ 1,0,0,0 },
			{ 1,0,0,1 }
			};
			int[,] seuraava;
			seuraava = Sopulit.MuodostaUusiSukupolvi(alku);
			Assert.AreEqual( "0 0 1 1,1 0 1 1,1 0 1 0,0 0 0 0", Sopulit.Jonoksi(seuraava," ",",") , "in method MuodostaUusiSukupolvi, line 91");
			seuraava = Sopulit.MuodostaUusiSukupolvi(seuraava);
			Assert.AreEqual( "0 1 1 1,0 0 0 0,0 0 1 1,0 0 0 0", Sopulit.Jonoksi(seuraava," ",",") , "in method MuodostaUusiSukupolvi, line 93");
			seuraava = Sopulit.MuodostaUusiSukupolvi(seuraava);
			Assert.AreEqual( "0 0 1 0,0 1 0 0,0 0 0 0,0 0 0 0", Sopulit.Jonoksi(seuraava," ",",") , "in method MuodostaUusiSukupolvi, line 95");
		}
		[Test]
		public  void TestSeuraavaSukupolvi116()
		{
			int[,] vaihe = {
			{ 1,0,1,1 },
			{ 0,1,1,0 },
			{ 1,0,0,0 },
			{ 1,0,0,1 }
			};
			Sopulit.SeuraavaSukupolvi(vaihe);
			Assert.AreEqual( "0 1 0 0,1 0 0 0,1 1 0 0,1 1 1 0", Sopulit.Jonoksi(vaihe," ",",") , "in method SeuraavaSukupolvi, line 124");
			Sopulit.SeuraavaSukupolvi(vaihe);
			Assert.AreEqual( "0 0 0 0,1 1 0 0,0 0 1 0,0 1 1 0", Sopulit.Jonoksi(vaihe," ",",") , "in method SeuraavaSukupolvi, line 126");
			Sopulit.SeuraavaSukupolvi(vaihe);
			Assert.AreEqual( "0 0 0 0,0 0 0 0,0 1 1 0,0 1 1 0", Sopulit.Jonoksi(vaihe," ",",") , "in method SeuraavaSukupolvi, line 128");
		}
		[Test]
		public  void TestLaskeNaapurit159()
		{
			int[,] alku = {
			{ 1,0,1,1 },
			{ 0,1,1,0 },
			{ 1,0,0,0 },
			{ 2,0,0,0 }
			};
			Assert.AreEqual( 1, Sopulit.LaskeNaapurit(alku, 0, 0) , "in method LaskeNaapurit, line 166");
			Assert.AreEqual( 1, Sopulit.LaskeNaapurit(alku, 3, 0) , "in method LaskeNaapurit, line 167");
			Assert.AreEqual( 4, Sopulit.LaskeNaapurit(alku, 0, 1) , "in method LaskeNaapurit, line 168");
			Assert.AreEqual( 2, Sopulit.LaskeNaapurit(alku, 2, 2) , "in method LaskeNaapurit, line 169");
			Assert.AreEqual( 0, Sopulit.LaskeNaapurit(alku, 3, 2) , "in method LaskeNaapurit, line 170");
		}
		[Test]
		public  void TestArvo201()
		{
			int[,] luvut = new int[3,3];
			Sopulit.Arvo(luvut,4,8);
			foreach (int luku in luvut)
			Assert.AreEqual( true, 4 <= luku && luku <= 8 , "in method Arvo, line 205");
		}
		[Test]
		public  void TestTayta225()
		{
			int[,] luvut = new int[3,3];
			Sopulit.Tayta(luvut,7);
			foreach (int luku in luvut)
			Assert.AreEqual( 7, luku , "in method Tayta, line 229");
			Sopulit.Tayta(luvut,2);
			foreach (int luku in luvut)
			Assert.AreEqual( 2, luku , "in method Tayta, line 232");
		}
	}

