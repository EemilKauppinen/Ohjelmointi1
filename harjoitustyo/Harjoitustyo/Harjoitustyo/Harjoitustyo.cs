using Jypeli;
using Jypeli.Assets;
using Jypeli.Controls;
using Jypeli.Widgets;
using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.IO;

public class StaattinenObjecti : GameObject
{
    // kun laskuri on 0, kappale ei näy.
    // kun laskuri on 1, tavallinen kappale näkyy.
    // kun laskuri on 2, start kappale näkyy.
    // kun laskuri on 3, end kappale näkyy.
    int laskuri = 0;
    private bool nakyva = false;

    public StaattinenObjecti(Animation animation) : base(animation)
    {
        this.nakyva = false;
        this.IsVisible = false;
    }

    public StaattinenObjecti(ILayout layout) : base(layout)
    {
        this.nakyva = false;
        this.IsVisible = false;
    }

    public StaattinenObjecti(double width, double height) : base(width, height)
    {
        this.nakyva = false;
        this.IsVisible = false;
    }

    public StaattinenObjecti(double width, double height, Shape shape) : base(width, height, shape)
    {
        this.nakyva = false;
        this.IsVisible = false;
    }

    public StaattinenObjecti(double width, double height, double x, double y) : base(width, height, x, y)
    {
        this.nakyva = false;
        this.IsVisible = false;
    }

    public StaattinenObjecti(double width, double height, Shape shape, double x, double y) : base(width, height, shape, x, y)
    {
        this.nakyva = false;
        this.IsVisible = false;
    }

    public void muutaNakyvyys()
    {
        // this.nakyva = !this.nakyva;
        // this.IsVisible = this.nakyva;
        this.IsVisible = !this.IsVisible;
        //this.Color = Color.Transparent;
    }

    public void KasvataLaskuria()
    {
        laskuri += 1;
        if (this.laskuri == 4)
        {
            laskuri = 0;
        }

        MuutaKappaleenTila();
    }

    public int PalautaLaskurinArvo()
    {
        return laskuri;
    }

    public void MuutaKappaleenTila()
    {
        if (laskuri == 0)
        {
            this.IsVisible = false;
        }
        if (laskuri == 1)
        {
            this.IsVisible = true;
        }
        if (laskuri == 2)
        {
            this.IsVisible = true;
            this.Color = Color.Red;
        }
        if (laskuri == 3)
        {
            this.IsVisible = true;
            this.Color = Color.Green;
        }
    }
    public void AsetaLaskurinArvo(int laskurinArvo)
    {
        if (laskurinArvo > 4 || laskurinArvo < 0)
        {
            throw new ArgumentException("Laskurin arvo oli ihan puuta-heinää");
        }
        laskuri = laskurinArvo;
        MuutaKappaleenTila();
            
    }
}

public class HarjoitustyoEditori : PhysicsGame
{
    static int ruudukonKokoX = 128;
    static int ruudukonKokoY = 64;
    static int kentanLeveys = 1000;
    static int kentanKorkeus = 500;
    private bool ladattu = false;
    private Kentta ekaKentta = new Kentta("ekaKentta.png", "kentta1.txt");

    List<StaattinenObjecti> kentanTekoLista = new List<StaattinenObjecti>(); // ruudukonKokoX * ruudukonKokoY);
    public override void Begin()
    {
        //new PhysicsObject(Animation animation)
        AsetaOhjaimet();

        //Level.CreateBorders();

        SetWindowSize(kentanLeveys, kentanKorkeus); // asettaa ikkunan koon
        // Camera.ZoomToLevel();

        Level.Background.Image = Image.FromFile(ekaKentta.AnnaTaustaKuvanNimi()); //Image.FromFile("testi.png"); ;

        LuoTestiKentta();

    }



    private void LuoStaattinenObjekti(int x, int y, Shape s, Color c)
    {
        int indeksi = anna_indeksi(x, y);
        double r = (double)kentanLeveys * 2.0 / (double)ruudukonKokoX;
        double xKoordinaatti = 0.5 * r + (double)x * r - kentanLeveys;
        double yKoordinaatti = 0.5 * r + (double)y * r - kentanKorkeus;
        StaattinenObjecti kappale = new StaattinenObjecti(r, r, Shape.Rectangle);
        // StaattinenObjecti kappale = new StaattinenObjecti(r, r, Shape.Circle);
        kappale.X = xKoordinaatti;
        kappale.Y = yKoordinaatti;
        kappale.Color = c;
        // kappale.
        // kappale.MakeStatic();
        Mouse.ListenOn(kappale, MouseButton.Left, ButtonState.Pressed, AktivoiStaattinenPallo, null, kappale);
        //Vector impulssi = new Vector(RandomGen.NextDouble(-20, 20), RandomGen.NextDouble(-20, 20));
        //Vector impulssi = new Vector(20, 500);
        //kappale.Hit(impulssi * kappale.Mass);
        // AddCollisionHandler(kappale,KasittelePallonTormays);
        this.Add(kappale);
        kentanTekoLista.Add(kappale);
    }

    void AktivoiStaattinenPallo(StaattinenObjecti objekti)
    {
        //objekti.muutaNakyvyys();
        objekti.KasvataLaskuria();
    }
    private void LuoTestiKentta()
    {
        for (int y = 0; y < ruudukonKokoY; y++)
        {
            for (int x = 0; x < ruudukonKokoX; x++)
            {
                LuoStaattinenObjekti(x, y, Shape.Rectangle, Color.Black);
                //ruudukonKokoX
            }
        }

    }


    /// <summary>
    /// Luodaan räjäytettävä pallo, joka saa satunnaisen vauhdin johonkin suuntaan.
    /// </summary>
    /// <param name="x">Kappaleen keskipisteen x-koordinaatti</param>
    /// <param name="y">Kappaleen keskipisteen y-koordinaatti</param>
    /// <param name="w">Kappaleen leveys.</param>
    /// <param name="h">Kappaleen korkeus.</param>
    public void LuoPallo(double x, double y, double w, double h, Color c)
    {
        PhysicsObject kappale = new PhysicsObject(w, h, Shape.Circle);
        kappale.Position = new Vector(x, y);
        kappale.Color = c;
        kappale.MakeStatic();
        //Vector impulssi = new Vector(RandomGen.NextDouble(-20, 20), RandomGen.NextDouble(-20, 20));
        //Vector impulssi = new Vector(20, 500);
        //kappale.Hit(impulssi * kappale.Mass);
        // AddCollisionHandler(kappale,KasittelePallonTormays);
        this.Add(kappale);
    }

    public void LuoKolmio(double x, double y, double w, double h, Color c)
    {
        PhysicsObject kappale = new PhysicsObject(w, h, Shape.Rectangle);
        kappale.Position = new Vector(x, y);
        kappale.Color = c;
        kappale.MakeStatic();
        //Vector impulssi = new Vector(RandomGen.NextDouble(-20, 20), RandomGen.NextDouble(-20, 20));
        //Vector impulssi = new Vector(20, 500);
        //kappale.Hit(impulssi * kappale.Mass);
        // AddCollisionHandler(kappale,KasittelePallonTormays);
        this.Add(kappale);
    }

    public void LuoLiikkuvaPallo(double x, double y, double w, double h, Color c)
    {
        PhysicsObject kappale = new PhysicsObject(w, h, Shape.Circle);
        kappale.Position = new Vector(x, y);
        kappale.Color = c;
        //kappale.MakeStatic();
        Vector impulssi = new Vector(RandomGen.NextDouble(-120, 120), RandomGen.NextDouble(-120, 120));
        //Vector impulssi = new Vector(20, 500);
        kappale.Hit(impulssi * kappale.Mass);
        // AddCollisionHandler(kappale,KasittelePallonTormays);
        this.Add(kappale);
    }

    public void LuoNelikulmio(double x, double y, double w, double h, Color c)
    {
        PhysicsObject kappale = new PhysicsObject(w, h, Shape.Rectangle);
        kappale.Position = new Vector(x, y);
        kappale.Color = c;
        kappale.MakeStatic();
        // Vector impulssi = new Vector(RandomGen.NextDouble(-20, 20), RandomGen.NextDouble(-20, 20));
        //kappale.Hit(impulssi * kappale.Mass);
        // AddCollisionHandler(kappale,KasittelePallonTormays);
        Add(kappale);
    }

    public static int anna_indeksi(int x_koordinaatti, int y_koordinaatti)
    {
        return x_koordinaatti + y_koordinaatti * ruudukonKokoX;
    }

    public static Vector anna_vektori(int indeksi, int ruudukon_leveys)
    {
        int x = indeksi;
        int y = x / ruudukon_leveys;
        x = x - y * ruudukon_leveys;
        return new Vector(x, y);
    }

    public void CreateMapData(string tiedosto)
    {
        // Tekee v:tta ja i:ta sen mukaan onko jokin näkyvää vai ei.
        StringBuilder sp = new StringBuilder();

        // Käy läpi kentantekolistan.
        foreach (var kentanPalanen in kentanTekoLista)
        {
            if (kentanPalanen.PalautaLaskurinArvo() == 0)
            {

                sp.Append("0");
            }
            if (kentanPalanen.PalautaLaskurinArvo() == 1)
            {   

                sp.Append("1");
            }
            if (kentanPalanen.PalautaLaskurinArvo() == 2)
            {
                System.Console.WriteLine("tallennetaan 2");
                sp.Append("2");
            }
            if (kentanPalanen.PalautaLaskurinArvo() == 3)
            {
                System.Console.WriteLine("tallennetaan 3");
                sp.Append("3");
            }
        }

        // Uusi stringbuilder korvaa aina edellisen tiedoston sisällön.
        System.IO.File.WriteAllText(tiedosto, sp.ToString());

        //using (StreamWriter outputFile = new StreamWriter("mapData.txt"))
        //{

        //outputFile.WriteLine(sp.ToString());
        //}

        //System.Console.WriteLine("{0}", sp.ToString());
    }
    public void LoadMapData(string kenttaDatanTiedostonNimi)
    {
        if (this.ladattu == false)
        {
          
            // 
            string kentanDataMerkkijonona = System.IO.File.ReadAllText(kenttaDatanTiedostonNimi);
            //System.Console.WriteLine(text);

            //  
            // "00020100202"
            // 
            for (int i = 0; i < kentanDataMerkkijonona.Length; i++)
            {
                try
                {

                    int parsittuLuku = Int32.Parse(kentanDataMerkkijonona[i].ToString());
                    kentanTekoLista[i].AsetaLaskurinArvo(parsittuLuku);
                }
                catch (Exception e)
                {
                    System.Console.WriteLine("Virhe LoadMapData aliohjelmassa.");
                    //continue;
                }

            }
        }

    }
    /// <summary>
    /// Asettaa näppäimet ja niihin liittyvät tapahtumat.
    /// </summary>
    void AsetaOhjaimet()
    {
        Keyboard.Listen(Key.L,
                 ButtonState.Down,
                 delegate () {
                     LoadMapData(ekaKentta.AnnakenttaDatatiedostonNimi());
                     this.ladattu = true;


                 },
                 "Lataa"
        );
        Keyboard.Listen(Key.L,
            ButtonState.Released,
            delegate () {
                this.ladattu = false;
         },
         "Lataa"
        );
        //Keyboard.Listen(Key.S, ButtonState.Down, AsetaNopeus, "Liikuta palloa ylös", peli_pallo, nopeusYlos);
        Keyboard.Listen(Key.S,
                        ButtonState.Down,
                        delegate() {
                            CreateMapData(ekaKentta.AnnakenttaDatatiedostonNimi());


                        },
                        "Tallenna"
        );

        PhoneBackButton.Listen(ConfirmExit, "Lopeta peli");
        Keyboard.Listen(Key.Escape, ButtonState.Pressed, ConfirmExit, "Lopeta peli");
    }

}
public class Kentta
{
    private string taustaKuvanNimi;
    private string kenttaDatatiedostonNimi;


    public Kentta(string taustaKuvanNimi, string kenttaDatatiedostonNimi )
    {
        this.taustaKuvanNimi = taustaKuvanNimi;
        this.kenttaDatatiedostonNimi = kenttaDatatiedostonNimi;
    }

    public string AnnaTaustaKuvanNimi()
    {
        return this.taustaKuvanNimi;
    }
    public string AnnakenttaDatatiedostonNimi()
    {
        return this.kenttaDatatiedostonNimi;
    }
}

