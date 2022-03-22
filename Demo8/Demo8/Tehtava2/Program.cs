public class Tehtava2
{
    public static void Main()
    {

    }
    /// <summary>
    /// Funktio, joka palauttaa tosi jos vuosi on karkausvuosi, ja epätosi jos vuosi ei ole karkausvuosi.
    /// </summary>
    /// <param name="vuosi">Vuosi, josta halutaan tietää onko se karkaus vuosi.</param>
    /// <returns>Palauttaa tosi jos vuosi on karkausvuosi, ja epätosi jos vuosi ei ole karkausvuosi.</returns>
    public static bool Karkausvuosi(int vuosi)
    {
        if (vuosi % 4 == 0 && (vuosi / 100) % 4 == 0)
        {
            return true;
        }
        return false;
     
    }
}