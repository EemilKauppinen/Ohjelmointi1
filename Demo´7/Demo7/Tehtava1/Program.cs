public class Tehtava1
{

    public static void Main(String[] args)
    {
        int sMaara = LaskeKirjaimet("kisSa", 's');
        int kMaara = LaskeKirjaimet("kiSSa", 'k');
        
    }
    /// <summary>
    /// Funktio, joka laskee merkkijonossa olevien annetun kirjaimen esiintymien lukumäärän.
    /// </summary>
    /// <param name="merkkijono">Tutkittava merkkijono</param>
    /// <param name="kirjain">Kirjain jonka esintyvyyttä halutaan tutkia</param>
    /// <returns>Kirjainmen esiintymien lukumäärä</returns>
    /// <example>
    /// <pre name="test">
    ///    LaskeKirjaimet("KOIRA", 'O') === 1;
    ///    LaskeKirjaimet("juliUs", 'j') === 1;
    ///    LaskeKirjaimet("akrgafbkrABFKkbakrfBFMJ", 'B ') === 2;
    ///    LaskeKirjaimet("akrgafbkrAqFKkbakrfiFMJ", 'B ') === 0;
    /// </pre>
    /// </example>
    public static int LaskeKirjaimet(string merkkijono, char kirjain)
    {
        int lukumaara = 0;


        for (int i = 0; i < merkkijono.Length; i++)
        {   
            if (char.ToLower(merkkijono[i]) == char.ToLower(kirjain))
            {
                lukumaara++;
            }
        }

        return lukumaara;
    }
} 