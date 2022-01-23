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



    /// <summary>
    /// Pääohjelma.
    /// </summary>
    static void Main(string[] args)
    { // Main alkaa.
        int jebulis = LaskeYhteen(13, 324);
        int jebulis5 = vahenna(13, 77);

        System.Console.WriteLine("int jebulis5 = {0}", jebulis5);
        System.Console.WriteLine("int jebulis = {0}", jebulis);

        int a = 0;
        a += 155;
        a += 100;



    } // Tahan loppuu Main  

    
    
    
}


