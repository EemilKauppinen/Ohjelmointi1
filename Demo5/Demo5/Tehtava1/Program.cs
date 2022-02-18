using System.Linq;
using System.Text;

public class Tehtava4
{
    public static void Main(string[] args)
    {
        StringBuilder jono = new StringBuilder("kissa istuu");
        LisaaAlkuunJaLoppuun(jono, "***"); // jono muuttuu aliohjelmassa
        Console.WriteLine("Jono on nyt " + jono);
        // tulostaa: Jono on nyt *** kissa istuu ***
    }
    /// <summary>
    /// Liittää annetun StringBuilderin alkuun ja loppuun merkkijonon ja välilyönit.
    /// Esimerkki aliohjelman käytöstä:
    /// 
    /// StringBuilder jono = new StringBuilder("kissa istuu");
    /// LisaaAlkuunJaLoppuun(jono, "***"); // jono muuttuu aliohjelmassa
    /// Console.WriteLine("Jono on nyt " + jono); 
    /// tulostaa: Jono on nyt *** kissa istuu ***
    /// 
    /// </summary>
    /// <param name="jono">StringBuilder, johon merkkijonot lisätään.</param>
    /// <param name="merkkijono">Lisättävä merkkijono</param>
    public static void LisaaAlkuunJaLoppuun(StringBuilder jono, String merkkijono)
    {

        jono.Append(" ");

        jono.Insert(0, " ");

        jono.Insert(0, merkkijono);

        jono.Append(merkkijono);

    }
}
