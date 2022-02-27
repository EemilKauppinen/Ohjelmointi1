using System;
public static class B2
{
    public static void Main(String[] args)
    {
        double[,] mat1 = { { 1, 2, 3 }, { 2, 2, 2 }, { 4, 2, 3 } };
        double[,] mat2 = { { 9, 2, 8 }, { 1, 2, 5 }, { 3, 19, -3 } };
        double suurin1 = Suurin(new double[,] { { 1, 2, 3 }, { 2, 2, 2 }, { 4, 2, 3 } });
        double suurin2 = Suurin(new double[,] { { 9, 2, 8 }, { 1, 2, 5 }, { 3, 19, -3 } });
    }
    /// <summary>
    /// Funktio paluttaa kaksiulotteisen double taulukon suurimman arvon.
    /// </summary>
    /// <param name="taulukot">Kaksiulotteinen double taulukko</param>
    /// <returns>Palauttaa suurimman arvon</returns>
    /// <example>
    /// <pre name="test">
    ///    Suurin(new double[,] { { 1, 2, 3 }, { 2, 2, 2 }, { 4, 2, 3 } }) === 4;
    ///    Suurin(new double[,] { { 9, 2, 8 }, { 1, 2, 5 }, { 3, 19, -3 } }) === 19;
    /// </pre>
    /// </example>
    public static double Suurin(double[,] taulukot)
    {
        double lopputulos = taulukot[0,0];


        for (int i = 0; i < taulukot.GetLength(0); i++)
        {
            for (int j = 0; j < taulukot.GetLength(1); j++)
            {
                double luku = taulukot[i, j];
                if (lopputulos < luku)
                {
                    lopputulos = luku;
                }
                
            }
        }
        return lopputulos;
    }

}