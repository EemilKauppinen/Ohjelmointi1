public class B3
{
    public static void Main(string[] args)
    {
        double a = 7.1001;
        double b = 7.1002;
        double c = 7.2002;
        bool lahella = Samat(a, b, 0.01);
        if (lahella) Console.WriteLine("Ovat melkein samoja");
        if (!Samat(a, b)) Console.WriteLine("Ovat eri suuria");
        if (!Samat(a, c, 0.01)) Console.WriteLine("Ovat eri suuria");
        if (Samat(a, c, 0.2)) Console.WriteLine("Ovat sinnepäin");
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
    /// Funktio tutkii onko kaksi lukua riittävän lähellä toisiaan.
    /// </summary>
    /// <param name="a">Ensimmäinen luku</param>
    /// <param name="b">toinen luku</param>
    /// <param name="eps">Paljonko luvut saa poiketa toisestaan.</param>
    /// <returns>true jos luvut ovat tarpeeksi lähellä toisiaan ja muutoin false.</returns>
    /// <example>
    /// <pre name="test">
    ///     Samat(7.1001, 7.1002) === false;
    ///     Samat(7.1001, 7.2002, 0.01) === true;
    ///     Samat(7.1001, 7.2002, 0.2 === true;
    /// </pre>
    /// </example>
    public static bool Samat(double a, double b, double eps = 0.00001)
    {
        //return Math.Abs(a - b) < eps;
        return Etaisyys(a, b) < eps;
    }
}