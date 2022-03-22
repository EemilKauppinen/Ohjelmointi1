public class B3
{







    public static void Main()
    {
        int loydettyIndeksi = EtsiIndeksi("koira", OnkoÄ);
        System.Console.WriteLine("loydettyIndeksi == {0}", loydettyIndeksi);



        Func<int, string> munFunktio = x =>
        {
            return x.ToString();
        };

        System.Console.WriteLine(munFunktio(1234));
        System.Console.WriteLine(munFunktio(32));
        System.Console.WriteLine(munFunktio(-333));
        System.Console.WriteLine(munFunktio(1234));
        System.Console.WriteLine(munFunktio(333331234));
    }



    public static int EtsiIndeksi(string taulukko, Predicate<char> apuFunktio)
    {
        for (int i = 0; i < taulukko.Length; i++)
        {
            char kirjain = taulukko[i];
            bool kelpaako = apuFunktio(kirjain);
            if (kelpaako) { return i; }
        }

        return -1;
    }

    public static bool OnkoK(char c)
    {
        if (c == 'k')
        {
            return true;
        }
        return false;
    }

    public static bool OnkoA(char c)
    {
        if (c == 'a')
        {
            return true;
        }
        return false;
    }

    public static bool OnkoÄ(char c)
    {
        if (c == 'ä')
        {
            return true;
        }
        return false;
    }

    public static void PupuAliohjelma(char c, Predicate<char> funktio)
    {


    }
    /// <summary>
    /// funktio, joka tutkii voidaanko annettu merkkijono upottaa toiseen merkkijonoon. 
    /// </summary>
    /// <param name="alkuperainenSana">Sana johon verrataan upotettavaa sanaa</param>
    /// <param name="upotettavaSana">Sana, joka upotetaan toisen sanan sisälle</param>
    /// <returns>Palauttaa false, jos upottaminen ei onnistu ja true jos onnistuu</returns>
    public static bool SisaltaaKaikkiMerkit(string alkuperainenSana, string upotettavaSana)
    {
        List<char> alkuperainenMerkkijono = alkuperainenSana.ToList();

        for (int i = 0; i < upotettavaSana.Length; i++)
        {
            int indeksi = alkuperainenMerkkijono.FindIndex(kirjain => kirjain == upotettavaSana[i]);

            if (indeksi < 0)
            {
                return false;
            }
            
            alkuperainenMerkkijono.RemoveAt(indeksi);
        }
        return true;
    }
}