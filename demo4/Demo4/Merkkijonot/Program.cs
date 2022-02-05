public class Merkkijonot
{
    public static void Main(string[] args)
    {
        Console.Write("Anna 1. jono >");
        string jono1 = Console.ReadLine();
        Console.Write("Anna 2. jono >");
        string jono2 = Console.ReadLine();
        string pidempi = PidempiMerkkijono(jono1, jono2);
        Console.WriteLine("\"" + pidempi + "\" on pidempi merkkijono");
    }
    /// <summary>
    /// Funktio, joka ottaa vastaan kaksi merkkijonoa, ja palauttaa näistä kahdesta pidemmän merkkijonon. Jonojen ollessa yhtä pitkiä palautetaan ensimmäisenä parametrina annettu jono.
    /// </summary>
    /// <param name="jono1">merkkijono1</param>
    /// <param name="jono2">merkkijono2</param>
    /// <returns>Palauttaa pidemmän merkkijonon tai jonojen ollessa yhtä pitkiä paluttaa ekan</returns>
    public static string PidempiMerkkijono(string jono1, string jono2)
    {
        if (jono1.Length > jono2.Length )
        {
            return jono1;
        }
        else if (jono1.Length < jono2.Length)
        {
            return jono2;
        }
        else 
        {
            return jono1;
        }
    }
}
