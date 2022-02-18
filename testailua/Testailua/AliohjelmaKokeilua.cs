public class AliohjelmaKokeilua
{

    public static int LaskeYhteen(int a, int b)
    {
        System.Console.WriteLine("Suositetaan aliohjelma LaskeYhteen(a = {0}. b = {1})", a, b);
        return a + b;
    }

    public static int vahenna(int a, int b)
    {
        System.Console.WriteLine("Suositetaan aliohjelma Vahentaa luvut toisistaan(a = {0}. b = {1})", a, b);
        return a - b;
    }

    public static double[] anna_vektori(int indeksi, int ruudukon_leveys)
    {
        int x = indeksi;
        int y = x / ruudukon_leveys;
        x = x - y * ruudukon_leveys;
        var joo = new double[] { x, y };
        System.Console.WriteLine("[{0}, {1}]", joo[0], joo[1]);
        return new double[] { x, y };

        //fn index_to_uvec3(index: u32, dim_x: u32, dim_y: u32) -> vec3<u32> {
        //    var x = index;
        //    let wh = dim_x * dim_y;
        //    let z = x / wh;
        //    x = x - z * wh; // check
        //    let y = x / dim_x;
        //    x = x - y * dim_x;
        //    return vec3<u32>(x, y, z);
        //}
    }

    public static int anna_indeksi(int x_koordinaatti, int y_koordinaatti, int ruudukon_leveys)
    {
        return x_koordinaatti + y_koordinaatti * ruudukon_leveys;

        //for (int y = 0; y < ruudukon_korkeus; y++) {
        //for (int x = 0; x < ruudukon_leveys; x++) {
        //    int listassaOlevaSijainti = x + y * ruudukon_leveys;
        //        System.Console.Write("[({0},{1}) :: {2}]", x, y, listassaOlevaSijainti);
        //        if (listassaOlevaSijainti % ruudukon_leveys == 0 && listassaOlevaSijainti != 0)
        //        {
        //            System.Console.WriteLine();
        //        }
        //    }
        //};
    }



    /// <summary>
    /// Pääohjelma.
    /// </summary>
    static void Main(string[] args)
    { // Main alkaa.
        //int jebulis = LaskeYhteen(13, 324);
        //int jebulis5 = vahenna(13, 77);

        //System.Console.WriteLine("int jebulis5 = {0}", jebulis5);
        //System.Console.WriteLine("int jebulis = {0}", jebulis);

        int a = 0;
        a += 155;
        a += 100;
        anna_indeksi(5, 8, 200);
        for (int i = 0; i < 100; i++)
        {
            anna_vektori(i, 10);
        }

    } // Tahan loppuu Main  

    
    
    
}


