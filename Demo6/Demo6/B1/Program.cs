public class B1
{
    public static void Main(String[] args)
    {
        System.Console.WriteLine("{0}", Skaalaa(0.2, -3, 3)); //~~~-1.8;
        System.Console.WriteLine("{0}", Skaalaa(0.2, 1, 6)); //~~~2.0;
        System.Console.WriteLine("{0}", Skaalaa(0.0, 1, 6)); //~~~1.0;
        System.Console.WriteLine("{0}", Skaalaa(1.0, 1, 6)); //~~~6.0;
    }

    /// <summary>
    /// Funktio, joka skaalaa välillä [0, 1] olevan luvun välille [min, max]
    /// </summary>
    /// <param name="luku">[0, 1] välillä oleva luku</param>
    /// <param name="min">Uuden välin alaraja</param>
    /// <param name="max">Uuden välin yläraja</param>
    /// <returns>Skaalattu luku</returns>
    /// <example>
    /// <pre name="test">
    ///    Skaalaa(0.2, -3, 3) ~~~ -1.8;
    ///    Skaalaa(0.2, 1, 6)  ~~~ 2.0;
    ///    Skaalaa(0.0, 1, 6)  ~~~ 1.0;
    ///    Skaalaa(1.0, 1, 6)  ~~~ 6.0;
    /// </pre>
    /// </example>
    public static double Skaalaa(double luku, double min, double max)
    {
        double pisteitenValinenEtaisyys = Math.Abs(min - max);

        return Math.Round((pisteitenValinenEtaisyys * luku) + min, 1);
    }


}