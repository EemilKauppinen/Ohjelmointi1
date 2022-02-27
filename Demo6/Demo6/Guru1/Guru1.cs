using Jypeli;
using Jypeli.Controls;

public class Peli : PhysicsGame
{
    private const int NY = 60;
    private int[,] sukupolvi;
    private int[,] seuraavaSukupolvi;
    private GameObject[,] oliot;
    private Timer timer = new Timer();
    private Color[] varit = { Color.Black, Color.White };

    public override void Begin()
    {
        Level.Background.Color = Color.Black;

        double dy = Screen.Height / NY; // Lasketaan ruudun korkeus pikseleinä
        int nx = (int)(Screen.Width / dy);  // ja montako mahtuu X-suuntaan
        int ny = NY;

        sukupolvi = new int[ny, nx];     //  Luodaan taulukot
        seuraavaSukupolvi = new int[ny, nx];
        oliot = new GameObject[ny, nx];
        // seuraavaSukupolvi = sukupolvi; // jos tämä on mukana, käyttäytyy eri tavalla

        LuoOliot(this, oliot);

        Camera.ZoomToAllObjects();

        timer.Interval = 0.1; // timeri antamaan tapahtuma 0.1 sek välein
        timer.Timeout += LaskeJaPiirraSeuraavaSukupolvi;
        ArvoSukupolvi();  // jos halutaan käynnistää automaattisesti
    }

    public void ArvoSukupolvi()
    {
        Sopulit.Arvo(sukupolvi, 0, 1);
        timer.Start();
    }

    private static void LuoOliot(PhysicsGame game, GameObject[,] oliot)
    {
        /// TODO: Täydennä niin, että luodaan yhtä monta suorakaidetta kuin
        /// on taulukoissa alkioita ja kukin luotu suorakaide tallennetaan
        /// oliot taulukkoon, jotta sen väriä päästään muuttamaan jatkossa.
        /// Koordinaatison yksikkönä kannattaa käyttää 1 ruutu = 1 yksikkö,
        /// ja origo on vasemmalla alhaalla.  Kameran zoomaus hoitaa
        /// kuvan kuntoon sitten.
        int[,] alkuSukupolvi = {
               { 1,0,1,1 },
               { 0,1,1,0 },
               { 1,0,0,0 },
               { 1,0,0,1 }
             };


    }

    private void LaskeJaPiirraSeuraavaSukupolvi()
    {
        /// TODO: Täydennä niin, että lasketaan ensin seuraava sukupolvi
        /// (ks. demo5:n SeuraavaSukupolvi, kutsu sitä).
        /// Sukupolven muodostamisen jälkeen käy sukupolvi-taulukko
        /// läpi ja muuta jokaisen oliot-taulukon vastaavassa
        /// paikassa olevan olion väri siten että 0 => musta ja 1 => valkoinen
    }
}