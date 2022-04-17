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
    
/// <summary>
/// Enum eri kappaleen tiloille.
/// </summary>
public enum KappaleenTila
{
    Tyhja,           // 0
    TormaysPalikka,  // 1
    AloitusPiste,    // 2
    LopetusPiste,    // 3
    Impulssi,        // 4
    Laiton           // 5
}


/// @author  Eemil Kauppinen
/// @version 4.4.2022
/// 
/// <summary>
/// Koostettu yleisiä apu funktioita.
/// </summary>
public class General
{
    /// <summary>
    /// Muuttaa indeksin ruudukon positioksi.
    /// </summary>
    /// <param name="indeksi">Yksiulotteinen indeksi</param>
    /// <param name="ruudukon_leveys">Kentän ruutujen määrä x suunnassa.</param>
    /// <returns>Palauttaa ruudukon x,y koordinaatin.</returns>
    public static Vector anna_vektori(int indeksi, int ruudukon_leveys)
    {
        int x = indeksi;
        int y = x / ruudukon_leveys;
        x = x - y * ruudukon_leveys;
        return new Vector(x, y);
    }


    /// <summary>
    /// Antaa ruudukon x,y koordinaateista indeksin "listasta". 
    /// </summary>
    /// <param name="x_koordinaatti">ruudukon x_koordinaatti</param>
    /// <param name="y_koordinaatti">ruudukon y_koordinaatti</param>
    /// <param name="ruudukonKokoX">Kentän ruutujen määrä x suunnassa.</param>
    /// <returns></returns>
    public static int anna_indeksi(int x_koordinaatti, int y_koordinaatti, int ruudukonKokoX)
    {
        return x_koordinaatti + y_koordinaatti * ruudukonKokoX;
    }


    /// <summary>
    /// Tallentaa doublet tiedostoon.
    /// </summary>
    /// <param name="doublet">taulukon doublet jotka halutaan tallentaa.</param>
    /// <param name="tiedostonNimi">tiedosto johon tallennetaan.</param>
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


    /// <summary>
    /// Lataa doublet tiedostosta.
    /// </summary>
    /// <param name="tiedostonNimi">tiedoston nimi, josta doublet ladataan.</param>
    /// <returns>tiedostosta luetut doublet.</returns>
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


/// @author  Eemil Kauppinen
/// @version 4.4.2022
/// 
/// <summary>
/// Pitää sisällään kentän kentän datan tiedostot.
/// </summary>
public class StaattinenObjecti : GameObject
{   
    /// <summary>
    /// Kappaleen tila.
    /// </summary>
    private KappaleenTila tila = KappaleenTila.Tyhja;

    /// <summary>
    /// Muuttuja sille onko nuoli valittu.
    /// </summary>
    private bool onValittu = false;

    /// <summary>
    /// Nuolen kuva
    /// </summary>
    private static Image nuoli = Image.FromFile("arrow.png");

    /// <summary>
    /// Valitun nuolen kuva
    /// </summary>
    private static Image nuoliValittu = Image.FromFile("arrow_green.png");

    /// <summary>
    /// Nuolen kulma
    /// </summary>
    private double asteluku = 0;


    /// <summary>
    /// Rakentaja.
    /// </summary>
    /// <param name="animation">animaatio</param>
    public StaattinenObjecti(Animation animation) : base(animation)
    {
       
        this.IsVisible = false;
    }


    /// <summary>
    /// Rakentaja.
    /// </summary>
    /// <param name="layout">Rajapinta.</param>
    public StaattinenObjecti(ILayout layout) : base(layout)
    {
        
        this.IsVisible = false;
    }


    /// <summary>
    /// Rakentaja.
    /// </summary>
    /// <param name="width">objektin leveys</param>
    /// <param name="height">objektin korkeus</param>
    public StaattinenObjecti(double width, double height) : base(width, height)
    {
        
        this.IsVisible = false;
    }


    /// <summary>
    /// Rakentaja.
    /// </summary>
    /// <param name="width">objektin leveys</param>
    /// <param name="height">objektin korkeus</param>
    public StaattinenObjecti(double width, double height, Shape shape) : base(width, height, shape)
    {
        
        this.IsVisible = false;
        this.RotateImage = true;
    }


    /// <summary>
    /// Rakentaja.
    /// </summary>
    /// <param name="width">objektin leveys</param>
    /// <param name="height">objektin korkeus</param>
    /// <param name="x">objektin x-koordinaatti</param>
    /// <param name="y">objektin y-koordinaatti</param>
    public StaattinenObjecti(double width, double height, double x, double y) : base(width, height, x, y)
    {
        
        this.IsVisible = false;
    }


    /// <summary>
    /// Rakentaja.
    /// </summary>
    /// <param name="width">objektin leveys</param>
    /// <param name="height">objektin korkeus</param>
    /// <param name="shape">objektin muoto</param>
    /// <param name="x">objektin x-koordinaatti</param>
    /// <param name="y">objektin y-koordinaatti</param>
    public StaattinenObjecti(double width, double height, Shape shape, double x, double y) : base(width, height, shape, x, y)
    {
       
        this.IsVisible = false;
    }


    /// <summary>
    /// Muuttaa staattisen objektin kappaleen tilaa seuraavasti.
    /// Tyhjä -> Törmäys palikka -> Aloitus piste -> Lopetus piste -> Impulssi -> Tyhjä.
    /// </summary>
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

        PäivitäKappaleenTila();
    }


    /// <summary>
    /// Palauttaa staattisen kappaleen tilan.
    /// </summary>
    /// <returns>staattisen objektin kappaleen tila.</returns>
    public KappaleenTila GetKappaleenTila()
    {
        return this.tila;
    }


    /// <summary>
    /// Määrittää onko staattiselle kappale valittu vai ei.
    /// </summary>
    /// <param name="valittu">true = valittu, false = ei ole valittu</param>
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


    /// <summary>
    /// Päivittää staattisen objektin tilaa, sen mukaan mikä tila sillä on.
    /// </summary>
    public void PäivitäKappaleenTila()
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


    /// <summary>
    /// Asettaa staattiselle objektille tilan.
    /// </summary>
    /// <param name="tila">tila, joka halutaan antaa.</param>
    /// <exception cref="ArgumentException">Jos tila on laiton ohjelma heittää poikkeuksen.</exception>
    public void AsetaKappaleenTila(KappaleenTila tila)
    {
        if ((int)KappaleenTila.Laiton <= (int)tila || (int)tila < 0)
        {
            throw new ArgumentException("Laskurin arvo oli ihan puuta-heinää");
        }
        this.tila = tila;
        PäivitäKappaleenTila();


    }


    /// <summary>
    /// Kasvattaa suuntalaskuria.
    /// </summary>
    /// <param name="aste">aste kuinka paljon halutaan kasvattaa.</param>
    public void KasvataSuuntaLaskuria(double aste)
    {
        this.asteluku = (this.asteluku + aste) % 360.0;
        this.Angle = Angle.FromDegrees(this.asteluku);
        
    }


    /// <summary>
    /// Vähentää suuntalaskuria.
    /// </summary>
    /// <param name="aste">aste kuinka paljon halutaan vähentää.</param>
    public void VahennaSuuntaLaskuria(double aste)
    {
        this.asteluku = this.asteluku - aste;
        if (asteluku < 0)
        {
            asteluku = 360 + asteluku;
        }
        this.Angle = Angle.FromDegrees(this.asteluku);
    }


    /// <summary>
    /// Asettaa + ja - nuolten kääntelyä varten.
    /// </summary>
    /// <param name="editori">Viite editoriin</param>
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

/// @author  Eemil Kauppinen
/// @version 4.4.2022
/// 
/// <summary>
/// Peli kentän lataamiseen, tallentamiseen ja editoimiseen.
/// 
/// Esimerkki pelikentän rakenteesta (ruudukko joka mätsätään pelialueen päälle).
/// 
/// +---+---+---+---+---+---+---+---+---+---+---+---+
/// | V | V | V | V | V | V | V | V | I | I | I | I |
/// +---+---+---+---+---+---+---+---+---+---+---+---+
/// | V | S | N | N | N | N | N | V | V | V | V | V |
/// +---+---+---+---+---+---+---+---+---+---+---+---+
/// | V | N | N | V | N | N | N | N | N | N | E | I |
/// +---+---+---+---+---+---+---+---+---+---+---+---+
/// | V | V | V | I | I | I | I | I | I | I | I | I |
/// +---+---+---+---+---+---+---+---+---+---+---+---+
/// 
/// 
/// V = Törmäyspalikka
/// S = AloitusPiste
/// E = LopetusPIste
/// N = Impulssi
/// I = Tyhja
/// 
/// 
/// </summary>
public class HarjoitustyoEditori : PhysicsGame
{
    /// <summary>
    /// Avaimena on kappaleentila ja sitä vastaava char. TODO: mieti toista ratkaisua.
    /// </summary>
    private Dictionary<KappaleenTila, char> TilaToChar = new Dictionary<KappaleenTila,char>();

    /// <summary>
    /// Ruudukon määrä x suunnassa.
    /// </summary>
    private static int ruudukonKokoX = 128;

    /// <summary>
    /// Ruudukon määrä y suunnassa.
    /// </summary>
    private static int ruudukonKokoY = 64;

    /// <summary>
    /// Pelikentän leveys.
    /// </summary>
    private static int kentanLeveys = 1000;

    /// <summary>
    /// Pelikentän korkeus.
    /// </summary>
    private static int kentanKorkeus = 500;

    /// <summary>
    /// Pitää tietoa siitä onko kenttää mahdollista ladata.
    /// </summary>
    private bool ladattu = false;

    //private Kentta ekaKentta = new Kentta("ekaKentta.png", "kentta1.txt", "test");

    /// <summary>
    /// Kyseinen kenttä, joka ladataan.
    /// </summary>
    private Kentta ekaKentta = new Kentta("ruohoKentta.jpg", "ruohoData.txt", "ruohoKulmat.txt");

    /// <summary>
    /// Lista ruudukon datasta.
    /// </summary>
    List<StaattinenObjecti> kentanTekoLista = new List<StaattinenObjecti>(); 

    /// <summary>
    /// Alustaa kenttä editorin toiminnan.
    /// </summary>
    public override void Begin()
    {
        TilaToChar.Add(KappaleenTila.Tyhja, 'I');
        TilaToChar.Add(KappaleenTila.TormaysPalikka, 'V');
        TilaToChar.Add(KappaleenTila.AloitusPiste, 'S');
        TilaToChar.Add(KappaleenTila.LopetusPiste, 'E');
        TilaToChar.Add(KappaleenTila.Impulssi, 'N');

        AsetaOhjaimet();

        SetWindowSize(kentanLeveys, kentanKorkeus); 
  

        Level.Background.Image = Image.FromFile(ekaKentta.AnnaTaustaKuvanNimi());

        AlustaKentta();

    }


    /// <summary>
    /// Muuttaa kaikki "nuolet" valitut falseksi, jotta ei olisi montaa noulta valittu.
    /// </summary>
    /// <param name="objekti">Kyseinen "nuoli"</param>
    public void AktivoiImpulssiPalikka(StaattinenObjecti objekti)
    {
        foreach(var kentanPalanen in kentanTekoLista)
        {
            kentanPalanen.OnValittu(false);

        }
        objekti.OnValittu(true);
    }


    /// <summary>
    /// Luo kentälle staattisen objektin.
    /// </summary>
    /// <param name="x">kappaleen x-koordinaatti</param>
    /// <param name="y">kappaleen y-koordinaatti</param>
    /// <param name="s">kappaleen muoto</param>
    /// <param name="c">kappaleen väri</param>
    private void LuoStaattinenObjekti(int x, int y, Shape s, Color c)
    {
        // ruudukon x, y koordinaatti.
        int indeksi = General.anna_indeksi(x, y, ruudukonKokoX);

        // r = ruudukon sivun pituus jypelissä
        double r = (double)kentanLeveys * 2.0 / (double)ruudukonKokoX;

        // Lasketaan ruudukon x,y koordinaattien vastaavuus jypelin koordinaatteihin (ruudun keskipiste).
        double xKoordinaatti = 0.5 * r + (double)x * r - kentanLeveys;
        double yKoordinaatti = 0.5 * r + (double)y * r - kentanKorkeus;

        StaattinenObjecti kappale = new StaattinenObjecti(r, r, Shape.Rectangle);
        kappale.AsetaOhjaimet(this);
        kappale.X = xKoordinaatti;
        kappale.Y = yKoordinaatti;
        kappale.Color = c;

        // Rekisteröidään hiiren klikkauksen tapahtumat kyseiselle ruudulle.
        Mouse.ListenOn(kappale, MouseButton.Left, ButtonState.Pressed, MuutaKappaleenTilaaSeuraavaksi, null, kappale);
        Mouse.ListenOn(kappale, MouseButton.Right, ButtonState.Pressed, AktivoiImpulssiPalikka, null, kappale);
        this.Add(kappale);
        kentanTekoLista.Add(kappale);
    }


    /// <summary>
    /// Muuttaa kappaleen tilaa.
    /// </summary>
    /// <param name="objekti">Kappale, jonka tilaa halutaan muuttaa</param>
    void MuutaKappaleenTilaaSeuraavaksi(StaattinenObjecti objekti)
    {     
        objekti.KasvataKappaleenTilaa();
    }


    /// <summary>
    /// Alustaa kentän, eli laittaa jokaiseen ruutuun objektin, joka myöhemmin vasta muutetaan.
    /// </summary>
    private void AlustaKentta()
    {
        for (int y = 0; y < ruudukonKokoY; y++)
        {
            for (int x = 0; x < ruudukonKokoX; x++)
            {
                LuoStaattinenObjekti(x, y, Shape.Rectangle, Color.Black);
                
            }
        }

    }


    /// <summary>
    /// Tallentaa kentän tiedot tiedostoihin.
    /// </summary>
    /// <param name="tilaTiedosto">Kentän ruudun tilan tiedot.</param>
    /// <param name="kulmatTiedosto">Kentän impulssien kulmien tiedot.</param>
    public void CreateMapData(string tilaTiedosto, string kulmatTiedosto)
    {
        List<double> temp = new List<double>();
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


    /// <summary>
    /// Lataa ja luo kentälle staattiset objektit ja impulssit.
    /// </summary>
    /// <param name="kenttaDatanTiedostonNimi">Tiedoston nimi ruudukon datalle.</param>
    /// <param name="kulmatTiedosto">Tiedoston nimi ruudukon kulmien datalle.</param>
    public void LoadMapData(string kenttaDatanTiedostonNimi, string kulmatTiedosto)
    {
        if (this.ladattu == false)
        {
            // Käy kentän ruudukko datan läpi ja palauttaa sen merkkijonona.
            string kentanDataMerkkijonona = System.IO.File.ReadAllText(kenttaDatanTiedostonNimi);

            // Lataa kentän ruudukon "nuolien" impulssien kulmat.
            List<double> kulmatLista = General.LueDoublet(kulmatTiedosto);

            // Kertoo paikan mistä listan indeksistä impulssin kulma valitaan.
            int doubleLaskuri = 0;

            // Rakennetaan kenttään ruutu kerrallaan ruutu datat (tyhjä, aloitusruutu, impulssi, jne ,,,)    
            for (int i = 0; i < kentanDataMerkkijonona.Length; i++)
            {

                    KappaleenTila kappaleenTila = TilaToChar.FirstOrDefault(x => x.Value == kentanDataMerkkijonona[i]).Key;
                    kentanTekoLista[i].AsetaKappaleenTila(kappaleenTila);

                    // Jos kappaleen tila on impulssi, otetaan listasta kulma ja sijoitetaan anglelle.
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
        // L:lää painamalla lataa kentän, olettaa että tiedostot ovat olemassa.
        Keyboard.Listen(
                 Key.L,
                 ButtonState.Down,
                 delegate () {
                     LoadMapData(ekaKentta.AnnakenttaDatatiedostonNimi(), ekaKentta.AnnaKulmatDatatiedostonNimi());

                     // varmistaa sen että ladataan vain kerran vaikka nappula olisi pohjassa.
                     this.ladattu = true;


                 },
                 "Lataa"
        );
        // Kun L:llä painaminen loppuu ladattu muutetaan falseksi, jotta se voidaan ladata uudestaan.
        Keyboard.Listen(
            Key.L,
            ButtonState.Released,
            delegate () {
                this.ladattu = false;
         },
         "Lataa"
        );
        // S:ssä painamalla tallentaa kentän tämän hetkisen datan tiedostoihin.
        Keyboard.Listen(Key.S,
                        ButtonState.Down,
                        delegate() {
                            CreateMapData(ekaKentta.AnnakenttaDatatiedostonNimi(), ekaKentta.AnnaKulmatDatatiedostonNimi());


                        },
                        "Tallenna"
        );

        PhoneBackButton.Listen(ConfirmExit, "Lopeta peli");
        Keyboard.Listen(Key.Escape, ButtonState.Pressed, ConfirmExit, "Lopeta peli");
    }

}


/// @author  Eemil Kauppinen
/// @version 4.4.2022
/// 
/// <summary>
/// Pitää sisällään kentän kentän datan tiedostot.
/// </summary>
public class Kentta
{
    /// <summary>
    /// Tiedosto kentän tausta kuvasta.
    /// </summary>
    private string taustaKuvanNimi;

    /// <summary>
    /// Tiedosto kentän staattisista kappaleista.
    /// </summary>
    private string kenttaDatatiedostonNimi;

    /// <summary>
    /// Tiedosto kentän impulsseista.
    /// </summary>
    private string kulmaTiedosto;

    /// <summary>
    /// Rakentaja kentälle.
    /// </summary>
    /// <param name="taustaKuvanNimi">taustaKuvan tiedoston nimi</param>
    /// <param name="kenttaDatatiedostonNimi">kenttaData tiedoston nimi</param>
    /// <param name="kulmaTiedosto">impulssi tiedosto nimi</param>
    public Kentta(string taustaKuvanNimi, string kenttaDatatiedostonNimi, string kulmaTiedosto)
    {
        this.taustaKuvanNimi = taustaKuvanNimi;
        this.kenttaDatatiedostonNimi = kenttaDatatiedostonNimi;
        this.kulmaTiedosto = kulmaTiedosto;
    }


    /// <summary>
    /// Antaa tallennetun kentän taustakuvan tiedoston nimen.
    /// </summary>
    /// <returns></returns>
    public string AnnaTaustaKuvanNimi()
    {
        return this.taustaKuvanNimi;
    }

    /// <summary>
    /// Antaa tallennetun kentän staattisten keppaleiden tiedoston nimen.
    /// </summary>
    /// <returns></returns>
    public string AnnakenttaDatatiedostonNimi()
    {
        return this.kenttaDatatiedostonNimi;
    }


    /// <summary>
    /// Antaa tallennetun kentän ilmpulssi tiedoston nimen.
    /// </summary>
    /// <returns>kentän ilmpulssi tiedoston nimi</returns>
    public string AnnaKulmatDatatiedostonNimi()
    {
        return this.kulmaTiedosto;
    }
}

