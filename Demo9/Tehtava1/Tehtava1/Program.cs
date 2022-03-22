public class Tehtava1
{

    public static void Main(String[] args)
    {

        int[] taulukko = { 3, 3, 4, 4, 4, 0, 3, 4, 6, 6, 2 };
        PisinNouseva(taulukko);
    }
    /// <summary>
    /// funktio, joka palauttaa int-lukuja sisältävän taulukon pisimmän aidosti kasvavan osajonon pituuden.
    /// </summary>
    /// <param name="intTaulukko">Taulukollinen numeroita joita testataan</param>
    /// <returns>palauttaa int-lukuja sisältävän taulukon pisimmän aidosti kasvavan osajonon pituuden.</returns>
    /// <example>
    /// <pre name="test">
    ///   PisinNouseva(new int[]{ 1,5,12,89,64,77,1000}) === 4; 
    ///   PisinNouseva(new int[]{ 0,0,0,0,0,0,0}) === 1; 
    ///   PisinNouseva(new int[]{ -1,-7,0,8,7,6}) === 3; 
    ///   PisinNouseva(new int[]{ 1}) === 1; 
    /// </pre>
    /// </example>
    public static int PisinNouseva(int[] intTaulukko)
    {
        int edellinen_luku = 0;

        int pituus = 0;

        int suurinPituus = 0;

        for (int i = 0; i < intTaulukko.Length; i++)
        {

            if (i == 0)
            {
                edellinen_luku = intTaulukko[i];
                pituus++;
                suurinPituus = pituus;

                continue;
            }
            else if (edellinen_luku > intTaulukko[i])
            {
                edellinen_luku = intTaulukko[i];
                pituus = 1;
            }
            else if (edellinen_luku < intTaulukko[i])
            {
                edellinen_luku = intTaulukko[i];
                pituus++;
            }
            else
            {
                edellinen_luku = intTaulukko[i];
                pituus = 1;

            }

            if (pituus > suurinPituus)
            {
                suurinPituus = pituus;
            }

        }

        return suurinPituus;
    }
}