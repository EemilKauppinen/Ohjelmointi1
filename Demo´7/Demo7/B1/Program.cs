using System;
using System.Text;
public class B1
{
    public static void Main()
    {
        char parsittu_materiaali;
        char parsittu_jalanMateriaali;
        int  parsittu_leveys = 0;
        
        while (true)
        {
            System.Console.Write("Miten levea puu > ");

            string leveys = System.Console.ReadLine();
            try
            {
                
                parsittu_leveys = Int32.Parse(leveys);
            }
            catch (Exception e)
            {
                System.Console.WriteLine("Antamasi syöte oli virheellinen");
                continue;
            }
            break;
        }

        while (true)
        {
            System.Console.Write("Mistä puu on tehty > ");

            string materiaali = System.Console.ReadLine();
            try
            {

                parsittu_materiaali = Char.Parse(materiaali);
            }
            catch (Exception e)
            {
                System.Console.WriteLine("Antamasi syöte oli virheellinen");
                continue;
            }
            break;
        }

        while (true)
        {
            System.Console.Write("Mistä jalka on tehty > ");

            string jalanMateriaali = System.Console.ReadLine();
            try
            {

                parsittu_jalanMateriaali = Char.Parse(jalanMateriaali);
            }
            catch (Exception e)
            {
                System.Console.WriteLine("Antamasi syöte oli virheellinen");
                continue;
            }
            break;
        }

        string puu = LuoPuu(parsittu_leveys, parsittu_materiaali, parsittu_jalanMateriaali);

        Console.WriteLine(puu);

    }
    /// <summary>
    /// Funktio joka luo ASCII-grafiikka-"puun" (ASCII-grafiikalla).
    /// </summary>
    /// <param name="luku">Puun leveys</param>
    /// <param name="merkki">Merkki, josta puu tehdään</param>
    /// <param name="merkki2">Merkki, joka on puun jalka</param>
    /// <returns>Paluttaa puun merkkijonona</returns>
    public static string LuoPuu(int luku, char merkki, char merkki2)
    {
        StringBuilder sb = new StringBuilder();

        int temp = -1;
        
        for (int i = 0; i < (luku/2) + 1; i++)
        {
            temp += 2;

            int tyhjaLukumaara = ((luku / 2) - i);

            System.Console.Write(TeeKuusenRivi(tyhjaLukumaara, merkki, temp));

            //sb.Append(' ', tyhjaLukumaara);
            //sb.Append(merkki, temp);
            //sb.Append('\n');

        }

        // Tehdään jalka
        sb.Append(' ', (luku - 3) / 2);     

        sb.Append(merkki2, 3);

        return sb.ToString();
    }
    /// <summary>
    /// Funktio kolmion yksittäisen rivin rakentamiseen, jossa arvotaan minkä verran koristeita riviin tulee.
    /// </summary>
    /// <param name="tyhjaLukumaara">Välilyöntien lukumaara</param>
    /// <param name="merkki">Merkki, josta puu tehdään</param>
    /// <param name="merkkienLukumaara">Puun merkkien lukumaara</param>
    /// <returns></returns>
    public static string TeeKuusenRivi(int tyhjaLukumaara, char merkki, int merkkienLukumaara)
    {
        StringBuilder sb = new StringBuilder();

        Random rng = new Random();
        
            sb.Append(' ', tyhjaLukumaara);
            sb.Append(merkki, merkkienLukumaara);
            sb.Append('\n');


        int koristeidenMaara = rng.Next(0, merkkienLukumaara);

        for (int i = 0; i < koristeidenMaara; i++)
        {
            sb[rng.Next(tyhjaLukumaara, merkkienLukumaara + tyhjaLukumaara)] = 'o';
        }   
        return sb.ToString();


    }
}
