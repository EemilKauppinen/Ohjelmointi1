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
public class General
{
    public static Vector anna_vektori(int indeksi, int ruudukon_leveys)
    {
        int x = indeksi;
        int y = x / ruudukon_leveys;
        x = x - y * ruudukon_leveys;
        return new Vector(x, y);
    }
    public static int anna_indeksi(int x_koordinaatti, int y_koordinaatti, int ruudukonKokoX)
    {
        return x_koordinaatti + y_koordinaatti * ruudukonKokoX;
    }
    public static void TallennaDoublet(double[] doublet, string tiedostonNimi)
    {

        using (StreamWriter writer = new StreamWriter(tiedostonNimi))
        {
            foreach (double t in doublet)
            {
                writer.Write(t);
                writer.Write('\n');
            }
        }
    }
    public static List<double> LueDoublet(string tiedostonNimi)
    {
        List<double> temp = new List<double>();
        string kentanDataMerkkijonona = System.IO.File.ReadAllText(tiedostonNimi);
        string[] pilkottuData = kentanDataMerkkijonona.Split('\n');
        for (int i = 0; i < pilkottuData.Length; i++)
        {

            bool onnistui = Double.TryParse(pilkottuData[i], out double t);
            if (onnistui)
            {
                temp.Add(t);
            }

        }
        return temp;
    }
}
public class StaattinenObjecti : GameObject
{
    private KappaleenTila tila = KappaleenTila.Tyhja;
    private bool nakyva = false;
    private bool onValittu = false;
    private static Image nuoli = Image.FromFile("arrow.png");
    private static Image nuoliValittu = Image.FromFile("arrow_green.png");
    private double asteluku = 0;
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
        this.RotateImage = true;
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
            this.asteluku = 0;
            this.Angle = Angle.FromDegrees(0);
        }
        else if (tila == KappaleenTila.LopetusPiste)
        {
            tila = KappaleenTila.Tyhja;
        }
        
        MuutaKappaleenTila();
    }

    public KappaleenTila GetKappaleenTila()
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
    public void KasvataSuuntaLaskuria(double aste)
    {
        this.asteluku = (this.asteluku + aste) % 360.0;
        this.Angle = Angle.FromDegrees(this.asteluku);
        
    }
    public void VahennaSuuntaLaskuria(double aste)
    {
        this.asteluku = this.asteluku - aste;
        if (asteluku < 0)
        {
            asteluku = 360 + asteluku;
        }
        this.Angle = Angle.FromDegrees(this.asteluku);
        System.Console.WriteLine(asteluku);
    }

    public void AsetaOhjaimet(HarjoitustyoEditori editori)
    {
        
            editori.Keyboard.Listen(Key.Add,
                     ButtonState.Down,
                     delegate () {
                         if (this.onValittu == true && this.tila == KappaleenTila.Impulssi)
                         {
                             KasvataSuuntaLaskuria(5);
                             //System.Console.WriteLine("käännä");
                         }


                     },
                     "Käännä"
            );
        editori.Keyboard.Listen(Key.Subtract,
         ButtonState.Down,
         delegate () {
             if (this.onValittu == true && this.tila == KappaleenTila.Impulssi)
             {
                 VahennaSuuntaLaskuria(5);
                 //System.Console.WriteLine("käännä");
             }


         },
         "Käännä"
);

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
    private Kentta ekaKentta = new Kentta("ekaKentta.png", "kentta1.txt", "test");

    List<StaattinenObjecti> kentanTekoLista = new List<StaattinenObjecti>(); // ruudukonKokoX * ruudukonKokoY);
    public override void Begin()
    {
        TilaToChar.Add(KappaleenTila.Tyhja, 'I');
        TilaToChar.Add(KappaleenTila.TormaysPalikka, 'V');
        TilaToChar.Add(KappaleenTila.AloitusPiste, 'S');
        TilaToChar.Add(KappaleenTila.LopetusPiste, 'E');
        TilaToChar.Add(KappaleenTila.Impulssi, 'N');
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
        int indeksi = General.anna_indeksi(x, y, ruudukonKokoX);
        double r = (double)kentanLeveys * 2.0 / (double)ruudukonKokoX;
        double xKoordinaatti = 0.5 * r + (double)x * r - kentanLeveys;
        double yKoordinaatti = 0.5 * r + (double)y * r - kentanKorkeus;
        StaattinenObjecti kappale = new StaattinenObjecti(r, r, Shape.Rectangle);
        kappale.AsetaOhjaimet(this);
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

    public void CreateMapData(string tilaTiedosto, string kulmatTiedosto)
    {
        List<double> temp = new List<double>();
        // Tekee v:tta ja i:ta sen mukaan onko jokin näkyvää vai ei.
        StringBuilder sp = new StringBuilder();

        // Käy läpi kentantekolistan.
        foreach (var kentanPalanen in kentanTekoLista)
        {
            sp.Append(TilaToChar[kentanPalanen.GetKappaleenTila()]);
            if (KappaleenTila.Impulssi == kentanPalanen.GetKappaleenTila()) 
            {
                temp.Add(kentanPalanen.Angle.Degrees);
            }

        }
        System.IO.File.WriteAllText(tilaTiedosto, sp.ToString());
        General.TallennaDoublet(temp.ToArray(), kulmatTiedosto);
    }
    public void LoadMapData(string kenttaDatanTiedostonNimi, string kulmatTiedosto)
    {
        if (this.ladattu == false)
        {
            string kentanDataMerkkijonona = System.IO.File.ReadAllText(kenttaDatanTiedostonNimi);

            int doubleLaskuri = 0;

            List<double> kulmatLista = General.LueDoublet(kulmatTiedosto);
            for (int i = 0; i < kentanDataMerkkijonona.Length; i++)
            {
         
                    // TODO default.
                    KappaleenTila kappaleenTila = TilaToChar.FirstOrDefault(x => x.Value == kentanDataMerkkijonona[i]).Key;
                    kentanTekoLista[i].AsetaLaskurinArvo(kappaleenTila);
                    if (KappaleenTila.Impulssi == kappaleenTila)
                    {
                      kentanTekoLista[i].Angle = Angle.FromDegrees(kulmatLista[doubleLaskuri]);
                      doubleLaskuri++;

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
                     LoadMapData(ekaKentta.AnnakenttaDatatiedostonNimi(), "test");
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

        Keyboard.Listen(Key.S,
                        ButtonState.Down,
                        delegate() {
                            CreateMapData(ekaKentta.AnnakenttaDatatiedostonNimi(), "test");


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
    private string kulmaTiedosto;

    public Kentta(string taustaKuvanNimi, string kenttaDatatiedostonNimi, string kulmaTiedosto)
    {
        this.taustaKuvanNimi = taustaKuvanNimi;
        this.kenttaDatatiedostonNimi = kenttaDatatiedostonNimi;
        this.kulmaTiedosto = kulmaTiedosto;
    }

    public string AnnaTaustaKuvanNimi()
    {
        return this.taustaKuvanNimi;
    }
    public string AnnakenttaDatatiedostonNimi()
    {
        return this.kenttaDatatiedostonNimi;
    }
    public string AnnaKulmatDatatiedostonNimi()
    {
        return this.kulmaTiedosto;
    }
}

