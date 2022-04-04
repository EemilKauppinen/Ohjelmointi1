using Jypeli;
using Jypeli.Assets;
using Jypeli.Controls;
using Jypeli.Widgets;
using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Collections.Generic;
    

public enum KappaleenTila
{
    Tyhja,           // 0
    TormaysPalikka,  // 1
    AloitusPiste,    // 2
    LopetusPiste,    // 3
    Impulssi,        // 4
    Laiton           // 5
}

public class StaattinenObjecti : GameObject
{
    private KappaleenTila tila = KappaleenTila.Tyhja;
    private bool nakyva = false;
    private bool onValittu = false;
    private static Image nuoli = Image.FromFile("arrow.png");
    private static Image nuoliValittu = Image.FromFile("arrow_green.png");

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
        this.IsVisible = !this.IsVisible;
    }

    public void KasvataKappaleenTilaa()
    {

        if (tila == KappaleenTila.Tyhja)
        {
            tila = KappaleenTila.TormaysPalikka;
        }
        else if (tila == KappaleenTila.TormaysPalikka)
        {
            tila = KappaleenTila.AloitusPiste;
        }
        else if (tila == KappaleenTila.AloitusPiste)
        {
            tila = KappaleenTila.Impulssi;
        }
        else if (tila == KappaleenTila.Impulssi)
        {
            tila = KappaleenTila.LopetusPiste;
        }
        else if (tila == KappaleenTila.LopetusPiste)
        {
            tila = KappaleenTila.Tyhja;
        }
        
        MuutaKappaleenTila();
    }

    public KappaleenTila PalautaKappaleenTila()
    {
        return this.tila;
    }
    public void OnValittu(bool valittu)
    {
        this.onValittu = valittu;
        if (tila == KappaleenTila.Impulssi)
        {
            if (onValittu)
            {
                this.Image = nuoliValittu;
            }
            else
            {
                this.Image = nuoli;
            }
            this.IsVisible = true;

        }
    }
    public void MuutaKappaleenTila()
    {
        if (tila == KappaleenTila.Tyhja)
        {
            this.Image = null;
            this.IsVisible = false;
        }
        if (tila == KappaleenTila.TormaysPalikka)
        {
            this.Image = null;
            this.IsVisible = true;
            this.Color = Color.Black;
        }
        if (tila == KappaleenTila.AloitusPiste)
        {
            this.Image = null;
            this.IsVisible = true;
            this.Color = Color.Red;
        }
        if (tila == KappaleenTila.LopetusPiste)
        {
            this.Image = null;
            this.IsVisible = true;
            this.Color = Color.Green;
        }
        if (tila == KappaleenTila.Impulssi)
        {
            if (onValittu)
            {
                this.Image = nuoliValittu;
            }
            else
            {
                this.Image = nuoli;
            }
            this.IsVisible = true;
            
        }
    }
    public void AsetaLaskurinArvo(KappaleenTila tila)
    {
        if ((int)KappaleenTila.Laiton <= (int)tila || (int)tila < 0)
        {
            throw new ArgumentException("Laskurin arvo oli ihan puuta-heinää");
        }
        this.tila = tila;
        MuutaKappaleenTila();
            
    }
}

public class HarjoitustyoEditori : PhysicsGame
{
    Dictionary<KappaleenTila, char> TilaToChar = new Dictionary<KappaleenTila,char>();

    private static int ruudukonKokoX = 128;
    private static int ruudukonKokoY = 64;
    private static int kentanLeveys = 1000;
    private static int kentanKorkeus = 500;

    private bool ladattu = false;
    private Kentta ekaKentta = new Kentta("ekaKentta.png", "kentta1.txt");

    List<StaattinenObjecti> kentanTekoLista = new List<StaattinenObjecti>(); // ruudukonKokoX * ruudukonKokoY);
    public override void Begin()
    {
        TilaToChar.Add(KappaleenTila.Tyhja, 'I');
        TilaToChar.Add(KappaleenTila.TormaysPalikka, 'V');
        TilaToChar.Add(KappaleenTila.AloitusPiste, 'S');
        TilaToChar.Add(KappaleenTila.LopetusPiste, 'E');
        //new PhysicsObject(Animation animation)
        AsetaOhjaimet();

        //Level.CreateBorders();

        SetWindowSize(kentanLeveys, kentanKorkeus); // asettaa ikkunan koon
        // Camera.ZoomToLevel();

        Level.Background.Image = Image.FromFile(ekaKentta.AnnaTaustaKuvanNimi()); //Image.FromFile("testi.png"); ;

        LuoTestiKentta();

    }

    public void AktivoiImpulssiPalikka(StaattinenObjecti objekti)
    {
        foreach(var kentanPalanen in kentanTekoLista)
        {
            kentanPalanen.OnValittu(false);

        }
        objekti.OnValittu(true);
    }

    private void LuoStaattinenObjekti(int x, int y, Shape s, Color c)
    {
        int indeksi = anna_indeksi(x, y);
        double r = (double)kentanLeveys * 2.0 / (double)ruudukonKokoX;
        double xKoordinaatti = 0.5 * r + (double)x * r - kentanLeveys;
        double yKoordinaatti = 0.5 * r + (double)y * r - kentanKorkeus;
        StaattinenObjecti kappale = new StaattinenObjecti(r, r, Shape.Rectangle);
        kappale.X = xKoordinaatti;
        kappale.Y = yKoordinaatti;
        kappale.Color = c;
        Mouse.ListenOn(kappale, MouseButton.Left, ButtonState.Pressed, AktivoiStaattinenPallo, null, kappale);
        Mouse.ListenOn(kappale, MouseButton.Right, ButtonState.Pressed, AktivoiImpulssiPalikka, null, kappale);
        this.Add(kappale);
        kentanTekoLista.Add(kappale);
    }

    void AktivoiStaattinenPallo(StaattinenObjecti objekti)
    {     
        objekti.KasvataKappaleenTilaa();
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
            sp.Append(TilaToChar[kentanPalanen.PalautaKappaleenTila()]);

        }
        System.IO.File.WriteAllText(tiedosto, sp.ToString());

    }
    public void LoadMapData(string kenttaDatanTiedostonNimi)
    {
        if (this.ladattu == false)
        {
            string kentanDataMerkkijonona = System.IO.File.ReadAllText(kenttaDatanTiedostonNimi);

            for (int i = 0; i < kentanDataMerkkijonona.Length; i++)
            {
         
                    // TODO default.
                    var kappaleenTila = TilaToChar.FirstOrDefault(x => x.Value == kentanDataMerkkijonona[i]).Key;
                    kentanTekoLista[i].AsetaLaskurinArvo(kappaleenTila);

                    

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

