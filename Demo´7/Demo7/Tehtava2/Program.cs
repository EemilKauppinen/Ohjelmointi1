using System;
using System.Text;
public class Tehtava2
{

    public static void Main()
    {
      
        string kolmio = LuoKolmio('@', 8);
        Console.WriteLine(kolmio);

    }

    /// <summary>
    /// Funktio, joka palauttaa suorakulmaisen kolmion niin kutsutulla ASCII-grafiikalla.
    /// </summary>
    /// <param name="merkki">Merkki, joka halutaan tulostaa kolmio muodostemassa</param>
    /// <param name="luku">Kolmion syvyys</param>
    /// <returns></returns>
    public static string LuoKolmio(char merkki, int luku)
    {
        StringBuilder sb = new StringBuilder();

        int pituus = luku;

        for (int i = 1; i < pituus + 1; i++)
        {
            for (int j = 0; j < i; j++)
            {
               sb.Append(merkki);
            }

            sb.Append('\n');
        }

        

        return sb.ToString();

    }
}
