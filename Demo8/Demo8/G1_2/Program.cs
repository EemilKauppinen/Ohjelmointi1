public class Tehatava4
{

    public static void Main()
    {
        //OnkoAnagrammi()"kissa", "koira")


    }
    /// <summary>
    /// Funktio, joka palauttaa tiedon (true / false) siitä ovatko kaksi merkkijonoa anagrammeja keskenään.
    /// </summary>
    /// <param name="a">Ensimmäinen merkkijono</param>
    /// <param name="b">Toinen erkkijono</param>
    /// <returns>Palauttaa true jos on anargrammaja ja false jos eivät ole.</returns>
    public static bool OnkoAnagrammi(String a, String b)
    {

        // Varataan taulukko esiintymiä varten. Kaikki on aluksi nolla. Huomaa että nyt on varattu vain 256 char paikkaa.
        int[] histogrammi = new int[256];

        // Jos esiintymät ovat eri pituisi niin ne eivät ole anagrammeja
        if (a.Length != b.Length)
        {
            return false;
        }
        // Laitetaan a:aan esiintymät taukukkoon. 
        for (int i = 0; i < a.Length; i++)
        {
            int temp = (int)a[i];
            
            histogrammi[temp]++;
        }
        // Vähennetään esiintymät.
        for (int i = 0; i < b.Length; i++)
        {
            int temp = (int)b[i];
            histogrammi[temp]--;

            // Jos menee miinukselle b:ssä on enemmän tämän kirjaimen esiintymiä kuin a:ssa, jolloin ne eivät voi olla anagrammeja.
            if (histogrammi[temp] < 0)
            {
                return false;
            }
        }

        // Kun tiedetään että a:ssa ja b:ssä yhtä paljon merkkejä ja esiintymät eivät mene negatiiviseksi, jolloin ne kumoavat toisensa ja ovat siis anagrammeja
        // Käsittääkseni algoritmi on lineaarinen koska siinä on kaksi for looppia. kummatkin merkkijonot käydään täsmälleen vain kerran läpi.
        return true;
    }
}