using Jypeli;

/// @author  Eemil Kauppinen
/// @version 23.1.2022
///
/// <summary>
/// Demo 2 Guru 3 tehtävä.
/// </summary>
public class G3 : PhysicsGame
{
    /// <summary>
    /// Ali ohjelma joka luo halutun pallon.
    /// </summary>
    /// <param name="x">pallon X koordinaatti</param>
    /// <param name="y">pallon Y koordinaatti</param>
    /// <param name="sade">pallon säde</param>
    /// <param name="c">pallon väri</param>
    public void Teepallo(double x, double y, double sade, Color c)
    {
        GameObject pallo = new GameObject(sade, sade, Shape.Circle);
        pallo.X = x;
        pallo.Y = y;
        pallo.Color = c;
        Add(pallo);
    }

    /// <summary>
    /// Tekee valkoisen lumiukon.
    /// </summary>
    /// <param name="lkm">pallojen lukumäärä</param>
    public void TeeLumiukko(int lkm)
    {
        TeeLumiukko(lkm, 0, 0, Color.White);
    }

    /// <summary>
    /// Ohjelma tekee lumiukon.
    /// </summary>
    /// <param name="lkm">pallojen lukumäärä</param>
    /// <param name="x">x koordinaatti</param>
    /// <param name="y">y koordinaatti</param>
    /// <param name="c">väri</param>
    public void TeeLumiukko(int lkm, double x, double y, Color c)
    {
        double pallon_y_koordinaati = y;

        // For loopissa luodaan lumiukko siten että seuraava pallo on pienenpi ja korkeammalla kuin edellinen.
        for (int i = 0; i < lkm; i++)
        {

            // Lasketaan pallon halkaisija. Aina kun i kasvaa niin halkaisija pienenee.
            double prosentti_osuus = (double)i / lkm;
            double halkaisija = 150 * (1.0 - prosentti_osuus);

            // Luodaan pallo. Kutsutaan Teepallo alaohjelmaa.
            Teepallo(x, pallon_y_koordinaati, halkaisija, c);

            // Seuraavan pallon y koordinaatti saadaan lisäämällä tämän hetkiseen y koordinaattiin tämän hetkisen pallon säde.
            pallon_y_koordinaati += halkaisija / 2;

        }
    }
    /// <summary>
    /// Tekee valkoisen lumiukon.
    /// </summary>
    /// <param name="lkm">pallojen lukumäärä</param>
    /// <param name="x">x koordinaatti</param>
    /// <param name="y">y koordinaatti</param>
    public void TeeLumiukko(int lkm, double x, double y)
    {
        TeeLumiukko(lkm, x, y, Color.White);
    }
    public override void Begin()
    {
        // Kirjoita ohjelmakoodisi tähän

        // System.Console.WriteLine("hihhulihei");
        PhoneBackButton.Listen(ConfirmExit, "Lopeta peli");
        Keyboard.Listen(Key.Escape, ButtonState.Pressed, ConfirmExit, "Lopeta peli");

        TeeLumiukko(5); // tekee 5-palloisen lumiukon origoon, väri valkoinen
        TeeLumiukko(3, 100, 100); // 3 palloa, alin pallo x=100, y=100, väri valkoinen
        TeeLumiukko(4, -100, -100, Color.Green); // 4 palloa, alin pallo x=-100, y=-100, väri vihreä
    }
}