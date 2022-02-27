using System;

public class B4
{
    static public void Main()
    {
     
    }
    /// <summary>
    /// Funktio palauttaa true mikäli pienempi luku on enintään suhteellisen osuuden määräämän etäisyyden päässä suuremmasta luvusta.
    /// </summary>
    /// <param name="a">Luku1</param>
    /// <param name="b">Luku2</param>
    /// <param name="suhteellinenOsuus">Prosenttiosuus suuremmasta luvusta</param>
    /// <returns>Funktio palauttaa false mikäli pienempi luku on enemmän kuin suhteellisen osuuden määräämän etäisyyden päässä suuremmasta luvusta.</returns>
    /// <example>
    /// <pre name="test">
    ///    SuhtSamat(0.10, 0.12, 0.1) === false
    ///    SuhtSamat(0.10, 0.11, 0.1) === true
    ///    SuhtSamat(1.0, 1.2, 0.1)   === false
    ///    SuhtSamat(1.0, 1.1, 0.1)   === true
    ///    SuhtSamat(10, 12, 0.1)     === false
    ///    SuhtSamat(10, 11, 0.1)     === true
    ///    SuhtSamat(1000, 1200, 0.1) === false
    ///    SuhtSamat(1000, 1100, 0.1) === true
    /// </pre>
    /// </example>
    public static bool SuhtSamat(double a, double b, double suhteellinenOsuus)
    {
       
        if (a > b)
        {
           if(a * suhteellinenOsuus > Math.Abs(a - b))
            {
                return true;
            }
           else
           {
                return false;
           }
        }
        else
        {
            if (b * suhteellinenOsuus > Math.Abs(a - b))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

