using System.Linq;
public class Tehtava3
{

    public static void Main()
    {
        double joo = SanojenPituusKeskiarvo("Julius, Arnold. Jimmy");
        System.Console.WriteLine(joo);  
    }
    /// <summary>
    /// Funktio ottaa parametrina merkkijonon, ja palauttaa sanojen pituuksien keskiarvon.
    /// </summary>
    /// <param name="merkkiJono">Lista merkkijonoista</param>
    /// <returns>palauttaa sanojen pituuksien keskiarvon</returns>
    public static double SanojenPituusKeskiarvo(string merkkiJono)
    {
        double temp = 0;

        string[] sanat = merkkiJono.Split(' ', '.', ',').ToList().Where(sana => sana.Length > 0).ToArray();                                                              


        for (int i = 0; i < sanat.Length; i++)
        {
            // if (sanat[i].Length == 0) continue;
 
            temp += sanat[i].Length;
            
            //System.Console.WriteLine(sanat[i]); 
        }

        //System.Console.WriteLine(sanat.Length);

        return temp / sanat.Length;

    }

}
