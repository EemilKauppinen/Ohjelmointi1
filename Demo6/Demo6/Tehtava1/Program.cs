
public class Tehtava1
{

    static public void Main()
    {
        Etaisyys(5, 10);
    }

    /// <summary>
    /// Funktio laskee kahden luvun välisen etäisyyden.
    /// </summary>
    /// <param name="a">Luku1</param>
    /// <param name="b">Luku2</param>
    /// <returns></returns>
    public static double Etaisyys(double a, double b)
    {

        double etaisyys = a - b;

        if (etaisyys < 0)
        {
            return etaisyys * (-1);
        }
        else
        {
            return etaisyys;
        }
    }
    /// <summary>
    /// Funktio laskee kahden pisteen etäisyyden 2d alastalla pythagoran lauseen avulla.
    /// </summary>
    /// <param name="x1">pisteen x-koordinaatti</param>
    /// <param name="y1">pisteen y-koordinaatti</param>
    /// <param name="x2">pisteen2 x-koordinaatti</param>
    /// <param name="y2">pisteen2 y-koordinaatti</param>
    /// <returns></returns>
    /// <example>
    /// <pre name="test">
    ///    Etaisyys(-7, -4, 17, 6.5) === 26.196374;
    /// </pre>
    /// </example>
    public static double Etaisyys(double x1, double y1, double x2, double y2)
    {
        double a = Etaisyys(x1, x2);
        double b = Etaisyys(y1, y2);
        double c = Math.Sqrt(Math.Pow(a, 2) + Math.Pow(b, 2));
        return c;
    }
}

