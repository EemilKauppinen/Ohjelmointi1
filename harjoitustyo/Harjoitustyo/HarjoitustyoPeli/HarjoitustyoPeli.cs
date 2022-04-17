using Jypeli;
using Jypeli.Assets;
using Jypeli.Controls;
using Jypeli.Widgets;
using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.IO;


/// @author  Eemil Kauppinen
/// @version 4.4.2022
/// 
/// <summary>
/// Määrittelee panosten ominaisuudet
/// </summary>
public class Panos
{
    /// <summary>
    /// Panoksen tuottama vahinko
    /// </summary>
    private int damage = 0;

    /// <summary>
    /// Panoksen lento nopeus
    /// </summary>
    private int nopeus = 0;

    /// <summary>
    /// Panoksen fysiikka objekti
    /// </summary>
    private PhysicsObject fysiikkaAmmus;

    /// <summary>
    /// Rakentaja panokselle.
    /// </summary>
    /// <param name="width">Panoksen leveys</param>
    /// <param name="height">Panoksen korkeus</param>
    /// <param name="x">Panoksen x-koordinaatti</param>
    /// <param name="y">Panoksen y-koordinaatti</param>
    /// <param name="s">Panoksen muoto</param>
    /// <param name="c">Panoksen väri</param>
    /// <param name="damage">Panoksen vahinko</param>
    /// <param name="nopeus">Panoksen nopeus</param>
    /// <param name="suunta">Panoksen suunta</param>
    /// <param name="elinAika">Panoksen elinaika</param>
    public Panos(double width, double height, double x, double y, Shape s, Color c, int damage, int nopeus, Vector suunta, TimeSpan elinAika)
    {
        PhysicsObject fysiikkaAmmus = new PhysicsObject(width, height, x, y);
        fysiikkaAmmus.Color = c;
        fysiikkaAmmus.Shape = s;
        this.damage = damage;
        fysiikkaAmmus.Hit(suunta*nopeus);
        fysiikkaAmmus.LifetimeLeft = elinAika; //TimeSpan.FromSeconds(5.0);
        this.fysiikkaAmmus = fysiikkaAmmus;
        this.nopeus = nopeus;
    }


    /// <summary>
    /// Palauttaa ammuksen physics objektin.
    /// </summary>
    /// <returns>ammuksen physics objekti</returns>
    public PhysicsObject GetPhysicsObject()
    {
        return this.fysiikkaAmmus;
    }


    /// <summary>
    /// Palauttaa panoksen tekemän vahingon.
    /// </summary>
    /// <returns>panoksen tekemä vahinko</returns>
    public int GetDamage()
    {
        return this.damage;
    }

}
/// @author  Eemil Kauppinen
/// @version 4.4.2022
/// 
/// <summary>
/// Määrittelee tykkien ominaisuudet
/// </summary>


public class Tykki
{
    /// <summary>
    /// tykin raha-arvo
    /// </summary>
    private int raha_arvo = 10;

    /// <summary>
    /// tykin elämä
    /// </summary>
    private int elama = 0;

    /// <summary>
    /// tykin ampuma etäisyys
    /// </summary>
    private int range = 0;

    /// <summary>
    /// tykin ajastin
    /// </summary>
    private Timer ajastin = null;

    /// <summary>
    /// Tavallisen tykin kuva.
    /// </summary>
    private static Image tykkiKuva = Image.FromFile("сannon1.png");

    /// <summary>
    /// Arnold tykki kuva.
    /// </summary>
    private static Image arnoldKuva = Image.FromFile("minigun2.png");

    private PhysicsObject fysiikkaObjekti;
    /// <summary>
    /// Rakentaja tykille.
    /// </summary>
    /// <param name="width">tykin leveys</param>
    /// <param name="height">tykin korkeus</param>
    /// <param name="x">tykin x-koordinaatti</param>
    /// <param name="y">tykin y-koordinaatti</param>
    /// <param name="s">tykin muoto</param>
    /// <param name="c">tykin väri</param>
    /// <param name="raha_arvo">tykin raha-arvo</param>
    /// <param name="elama">tykin elämä</param>
    /// <param name="range">tykin ampuma etäisyys</param>
    public Tykki(double width, double height, double x, double y, Shape s, Color c, int raha_arvo, int elama, int range)
    {
        PhysicsObject fysiikkaObjekti = new PhysicsObject(width, height, x, y);
        fysiikkaObjekti.Color = c;
        fysiikkaObjekti.Shape = s;
        fysiikkaObjekti.MakeStatic();
        this.raha_arvo = raha_arvo;
        this.elama = elama;
        this.range = range;
        this.fysiikkaObjekti = fysiikkaObjekti;
    }


    /// <summary>
    /// Luo ajastimen tykille.
    /// </summary>
    /// <param name="ajastin">tykin ajastin, joka kertoo milloin tykki ampuu</param>
    public void SetAjastin(Timer ajastin)
    {
        this.ajastin = ajastin;
    }


    /// <summary>
    /// Palauttaa ajastimen.
    /// </summary>
    /// <returns>ajastin</returns>
    public Timer GetAjastin()
    {
        return this.ajastin;
    }


    /// <summary>
    /// Aiheuttaa vahinkoa tykille.
    /// </summary>
    /// <returns>Jäljelle jäänyt elämä</returns>
    public int AiheutaVahinko(int vahingonMaara)
    {
        this.elama = this.elama - vahingonMaara;
        return this.elama;
    }


    /// <summary>
    /// Palauttaa tykin raha-arvon.
    /// </summary>
    /// <returns>tykin raha-arvo</returns>
    public int GetRahaArvo()
    {

        return this.raha_arvo;
    }


    /// <summary>
    /// Luo tykin, joka ampuu ajoittain pienen panoksen.
    /// </summary>
    /// <param name="x">tykin x-koordinaatti</param>
    /// <param name="y">tykin y-koordinaatti</param>
    /// <param name="peli">viite peliin</param>
    /// <param name="onTykkiTyyppi">boolin arvo joka kertoo luodaanko physics objekti vaiko ei.</param>
    /// <returns>tykki</returns>
    public static Tykki LuoTavallinenTykki(double x, double y, HarjoitustyoPeli peli, bool onTykkiTyyppi)
    {
        Tykki tykki = new Tykki(15, 15, x, y, Shape.Rectangle, Color.Azure, 25, 50, 300);

        // TODO: toisteista koodia, refakturoi jos on aikaa.
        if (onTykkiTyyppi)
        {
            return tykki;
        }
        tykki.GetPhysicsObject().Image = Tykki.tykkiKuva;

        Timer ajastin = Timer.CreateAndStart(3, () =>
        {
            Vector tykinPositio = tykki.GetPhysicsObject().Position;
            Vector vihollisenSijainti = peli.EtsiLahinVihollinen(tykinPositio);
            double tykinJaVihollisenVälinenSijainti = Vector.Distance(vihollisenSijainti, tykinPositio);
            if (tykinJaVihollisenVälinenSijainti <= tykki.range)
            {
                // Lasketaan tykin panoksen suunta.
                Vector suunta = (vihollisenSijainti - tykinPositio) / tykinJaVihollisenVälinenSijainti;
                Vector impulssi = suunta*4;
                peli.LuoPanos(tykki.GetPhysicsObject().Position, suunta, new Panos(10, 10, tykinPositio.X, tykinPositio.Y, Shape.Circle, Color.Charcoal, 20, 11, impulssi, TimeSpan.FromSeconds(5.0)));
            }
            
            

        }
        );
        tykki.SetAjastin(ajastin);
        



        return tykki;
    }


    /// <summary>
    /// Luo Arnold Schwarzenegger minigun tykin.
    /// </summary>
    /// <param name="x">tykin x-koordinaatti</param>
    /// <param name="y">tykin y-koordinaatti</param>
    /// <param name="peli">viite peliin</param>
    /// <param name="onTykkiTyyppi">boolin arvo joka kertoo luodaanko physics objekti vaiko ei.</param>
    /// <returns>tykki</returns>
    public static Tykki LuoArnold(double x, double y, HarjoitustyoPeli peli, bool onTykkiTyyppi)
    {
        Tykki tykki = new Tykki(30, 30, x, y, Shape.Rectangle, Color.Black, 100, 50, 150);

        // TODO: toisteista koodia, refakturoi jos on aikaa.
        if (onTykkiTyyppi)
        {
            return tykki;
        }

        tykki.GetPhysicsObject().Image = Tykki.arnoldKuva;
        
        Timer ajastin = Timer.CreateAndStart(0.5, () =>
        {
            Vector tykinPositio = tykki.GetPhysicsObject().Position;
            Vector vihollisenSijainti = peli.EtsiLahinVihollinen(tykinPositio);
            double tykinJaVihollisenVälinenSijainti = Vector.Distance(vihollisenSijainti, tykinPositio);
            if (tykinJaVihollisenVälinenSijainti <= tykki.range)
            {
                // Lasketaan tykin panoksen suunta.
                Vector suunta = (vihollisenSijainti - tykinPositio) / tykinJaVihollisenVälinenSijainti;
                Vector impulssi = suunta*5;
                peli.LuoPanos(tykki.GetPhysicsObject().Position, suunta, new Panos(6, 3, tykinPositio.X, tykinPositio.Y, Shape.Circle, Color.Black, 2, 9, impulssi, TimeSpan.FromSeconds(2.0)));
            }



        }
        );
        tykki.SetAjastin(ajastin);
        return tykki;
    }


    /// <summary>
    /// Luo tykin joka ampuu nopeasti, mutta ei kauas.
    /// </summary>
    /// <param name="x">tykin x-koordinaatti</param>
    /// <param name="y">tykin y-koordinaatti</param>
    /// <param name="peli">viite peliin</param>
    /// <param name="onTykkiTyyppi">boolin arvo joka kertoo luodaanko physics objekti vaiko ei.</param>
    /// <returns>tykki</returns>
    public static Tykki LuoLiekinHeitin(double x, double y, HarjoitustyoPeli peli, bool onTykkiTyyppi)
    {
        Tykki tykki = new Tykki(15, 15, x, y, Shape.Rectangle, Color.Red, 150, 50, 100);

        // TODO: toisteista koodia, refakturoi jos on aikaa.
        if (onTykkiTyyppi)
        {
            return tykki;
        }
        Timer ajastin = Timer.CreateAndStart(0.1, () =>
        {
            Vector tykinPositio = tykki.GetPhysicsObject().Position;
            Vector vihollisenSijainti = peli.EtsiLahinVihollinen(tykinPositio);
            double tykinJaVihollisenVälinenSijainti = Vector.Distance(vihollisenSijainti, tykinPositio);
            if (tykinJaVihollisenVälinenSijainti <= tykki.range)
            {
                // Lasketaan tykin panoksen suunta.
                Vector suunta = (vihollisenSijainti - tykinPositio) / tykinJaVihollisenVälinenSijainti;
                Vector impulssi = suunta * 0.5;
                
                peli.LuoPanos(tykki.GetPhysicsObject().Position, suunta, new Panos(3, 3, tykinPositio.X, tykinPositio.Y, Shape.Circle, Color.Red, 3, 4, impulssi, TimeSpan.FromSeconds(0.2)));
            }



        }
        );
        tykki.SetAjastin(ajastin);
        return tykki;
    }


    /// <summary>
    /// Luo tykin joka ampuu hitaasti ja kauas.
    /// </summary>
    /// <param name="x">tykin x-koordinaatti</param>
    /// <param name="y">tykin y-koordinaatti</param>
    /// <param name="peli">viite peliin</param>
    /// <param name="onTykkiTyyppi">boolin arvo joka kertoo luodaanko physics objekti vaiko ei.</param>
    /// <returns>tykki</returns>
    public static Tykki LuoSniperTykki(double x, double y, HarjoitustyoPeli peli, bool onTykkiTyyppi)
    {
        Tykki tykki = new Tykki(15, 15, x, y, Shape.Rectangle, Color.Green, 75, 50, 750);

        // TODO: toisteista koodia, refakturoi jos on aikaa.
        if (onTykkiTyyppi)
        {
            return tykki;
        }
        Timer ajastin = Timer.CreateAndStart(8, () =>
        {
            Vector tykinPositio = tykki.GetPhysicsObject().Position;
            Vector vihollisenSijainti = peli.EtsiLahinVihollinen(tykinPositio);
            double tykinJaVihollisenVälinenSijainti = Vector.Distance(vihollisenSijainti, tykinPositio);
            if (tykinJaVihollisenVälinenSijainti <= tykki.range)
            {
                // Lasketaan tykin panoksen suunta.
                Vector suunta = (vihollisenSijainti - tykinPositio) / tykinJaVihollisenVälinenSijainti;
                Vector impulssi = suunta * 6;
                
                peli.LuoPanos(tykki.GetPhysicsObject().Position, suunta, new Panos(15, 10, tykinPositio.X, tykinPositio.Y, Shape.Circle, Color.Charcoal, 35, 30, impulssi, TimeSpan.FromSeconds(10.0)));
                //peli.LuoPanos(tykki.GetPhysicsObject().Position, suunta, Panos.LuoSnipePanos(tykinPositio.X, tykinPositio.Y, impulssi * 6));
            }



        }
        );
        tykki.SetAjastin(ajastin);
        return tykki;
    }


    /// <summary>
    /// Palauttaa tykin physics objektin.
    /// </summary>
    /// <returns>tykin physics objekti</returns>
    public PhysicsObject GetPhysicsObject()
    {
        return this.fysiikkaObjekti;
    }

}
/// @author  Eemil Kauppinen
/// @version 4.4.2022
/// 
/// <summary>
/// Määrittelee vihollisen ominaisuudet
/// </summary>


public class Vihollinen
{
    /// <summary>
    /// vihollisen raha-arvo
    /// </summary>
    private int raha_arvo = 10;
    /// <summary>
    /// vihollisen elämä
    /// </summary>
    private int elama = 0;
    /// <summary>
    /// vihollisen vahinko
    /// </summary>
    private int vahinko = 0;

    /// <summary>
    /// Vihollisen kuva.
    /// </summary>
    private static Image zombieKuva = Image.FromFile("ufo.png");

    private PhysicsObject tormausKappale;
    /// <summary>
    /// Rakentaja viholliselle.
    /// </summary>
    /// <param name="width">vihollisen leveys</param>
    /// <param name="height">vihollisen korkeus</param>
    /// <param name="x">vihollisen x-koordinaatti</param>
    /// <param name="y">vihollisen y-koordinaatti</param>
    /// <param name="s">vihollisen muoto</param>
    /// <param name="c">vihollisen väri</param>
    /// <param name="raha_arvo">vihollisen raha-arvo</param>
    /// <param name="elama">vihollisen elämä</param>
    public Vihollinen(double width, double height, double x, double y, Shape s, Color c, int raha_arvo, int elama, int vahinko)
    {
        PhysicsObject tormausKappale = new PhysicsObject(width, height, x, y);
        tormausKappale.Color = c;
        tormausKappale.Shape = s;
        tormausKappale.KineticFriction = 5.5;
        tormausKappale.Restitution = 0.5;
        tormausKappale.MaxVelocity = 50.0;
        // tormausKappale.Velocity = new Vec;
        tormausKappale.Image = Vihollinen.zombieKuva;
        tormausKappale.RotateImage = true;
        this.raha_arvo = raha_arvo;
        this.elama = elama;
        this.vahinko = vahinko;
        this.tormausKappale = tormausKappale;   
    }


    /// <summary>
    /// Palauttaa vihollisen physics objektin.
    /// </summary>
    /// <returns>vihollisen physics objektin</returns>
    public PhysicsObject GetTormausKappale()
    {
        return this.tormausKappale;
    }


    /// <summary>
    /// Palauttaa vihollisen aiheuttaman vahingon.
    /// </summary>
    /// <returns>vihollisen aiheuttaman vahingon</returns
    public int GetVahinko()
    {
        return this.vahinko;
    }


    /// <summary>
    /// Palauttaa raha-arvon
    /// </summary>
    /// <returns>raha-arvo</returns>
    public int GetRaha_Arvo()
    {
        return this.raha_arvo;
    }


    /// <summary>
    /// Vähentää itseltään elämää.
    /// </summary>
    /// <param name="vahinko">paljoko ottaa vahinkoa</param>
    /// <returns>jäljelle jäävä elämä</returns>
    public int TeeVahinko(int vahinko)
    {
        
        this.elama -= vahinko;
        return this.elama;
    }

}


/// @author  Eemil Kauppinen
/// @version 4.4.2022
/// 
/// <summary>
/// Määrittelee kentän ruudun ominaisuudet
/// </summary>


public class StaattinenKenttaObjekti // : PhysicsObject
{
    private PhysicsObject kenttaObjekti;
    private KappaleenTila tila = KappaleenTila.Tyhja;


    /// <summary>
    /// Rakentaja staattiselle kenttä objektille.
    /// </summary>
    /// <param name="width">Leveys</param>
    /// <param name="height">Korkeus</param>
    /// <param name="x">x-koordinaatti</param>
    /// <param name="y">y-koordinaatti</param>
    /// <param name="s">Muoto</param>
    public StaattinenKenttaObjekti(double width, double height, double x, double y, Shape s) 
    {
        PhysicsObject jsfkka = new PhysicsObject(width, height, x , y);
        this.kenttaObjekti = jsfkka;
        kenttaObjekti.Color = Color.Black;
        jsfkka.MakeStatic();
        jsfkka.Shape = s;
        AsetaKappaleenTila(this.tila);
    }


    /// <summary>
    /// Palautaa staattisen kenttä objektin fysiikka kappaleen.
    /// </summary>
    /// <returns>fysiikka kappale</returns>
    public PhysicsObject GetPhysicsObject()
    {
        return this.kenttaObjekti;
    }
    

    /// <summary>
    /// Asettaa kentän kappaleelle tilan.
    /// </summary>
    /// <param name="tila">uusi kappaleentila</param>
    public void AsetaKappaleenTila(KappaleenTila tila)
    {
        // if ((int)KappaleenTila.Laiton <= (int)tila || (int)tila < 0)
        // {
        //    throw new ArgumentException("Laskurin arvo oli ihan puuta-heinää");
        //}
        this.tila = tila;
        MuutaKappaleenTila();

    }


    /// <summary>
    /// Muuttaa kappaleentilaa. 
    /// Tyhjä -> TormaysPalikka -> AloitusPiste -> LopetusPiste -> Tyhjä.
    /// </summary>
    public void MuutaKappaleenTila()
    {
        if (tila == KappaleenTila.Tyhja)
        {
            this.kenttaObjekti.IsVisible = false;
        }
        if (tila == KappaleenTila.TormaysPalikka)
        {
            this.kenttaObjekti.IsVisible = false;
        }
        if (tila == KappaleenTila.AloitusPiste)
        {
            this.kenttaObjekti.IsVisible = false;
            this.kenttaObjekti.Color = Color.Red;
        }
        if (tila == KappaleenTila.LopetusPiste)
        {
            this.kenttaObjekti.IsVisible = false;
            this.kenttaObjekti.Color = Color.Green;
        }
    }


    /// <summary>
    /// Palautaa kentänkappaleen sijainnin peli alueella.
    /// </summary>
    /// <returns>kentänkappaleen sijainti</returns>
    public Vector GetPosition()
    {
        return kenttaObjekti.Position;
    }


    /// <summary>
    /// Palauttaa kentän kappaleen tilan
    /// </summary>
    /// <returns>kentän kappaleen tila</returns>
    public KappaleenTila GetKappaleenTila()
    {
        return this.tila;
    }
}
/// @author  Eemil Kauppinen
/// @version 4.4.2022
/// 
/// <summary>
/// Varsinainen peli
/// </summary>


public class HarjoitustyoPeli : PhysicsGame
{

    /// <summary>
    /// Mistä indeksistä ladataan kenttä kentät listasta.
    /// </summary>
    private static int kentanNumero = 0;

    /// <summary>
    /// Avaimena on kappaleentila ja sitä vastaava char. TODO: mieti toista ratkaisua.
    /// </summary>
    private Dictionary<KappaleenTila, char> TilaToChar = new Dictionary<KappaleenTila, char>();

    /// <summary>
    /// Laskuri vihollisten määrälle.
    /// </summary>
    private IntMeter vihollisLaskuri = new IntMeter(0, 0, 1000);

    /// <summary>
    /// Laskuri vihollisten spawnaamiselle.
    /// </summary>
    private IntMeter spawnausLaskuri = new IntMeter(1000, 0, 1000);

    /// <summary>
    /// Ruutuen määrä leveyssuunnassa
    /// </summary>
    private static int ruudukonKokoX = 128;

    /// <summary>
    /// Ruutuen määrä pystysuunnassa
    /// </summary>
    private static int ruudukonKokoY = 64;

    /// <summary>
    /// Kentän leveys
    /// </summary>
    private static int kentanLeveys = 1000;

    /// <summary>
    /// Kentän korkeus
    /// </summary>
    private static int kentanKorkeus = 500;

    /// <summary>
    /// Laskuri pelaajan elämistä.
    /// </summary>
    private IntMeter pisteLaskuri;

    /// <summary>
    /// Laskuri pelaajan rahoista.
    /// </summary>
    private IntMeter rahaLaskuri;

    /// <summary>
    /// tykkiTyyppi on valittu tykki tyyppi, jonka voi sijoittaa kenttään.
    /// </summary>
    private Tykki tykkiTyyppi = null;

    /// <summary>
    /// Tykki numerolla ilmaistaan mikä tykki halutaan laittaa. 1 perustykki, 2 minigun, 3 liekinheitin, 4 sniper.
    /// </summary>
    private int tykkiNumero = 0;

    /// <summary>
    /// Lista kentällä olevista staattisista kappaleista.
    /// </summary>
    private List<StaattinenKenttaObjekti> kentanTekoLista = new List<StaattinenKenttaObjekti>();

    /// <summary>
    /// Lista kentällä olevista aloituspisteistä.
    /// </summary>
    private List<StaattinenKenttaObjekti> aloitusPisteet = new List<StaattinenKenttaObjekti>();

    /// <summary>
    /// Lista tällähetkellä kentällä olevista vihollisista.
    /// </summary>
    private List<Vihollinen> viholliset = new List<Vihollinen>();

    /// <summary>
    /// Lista tällähetkellä kentällä olevista tykeistä.
    /// </summary>
    private List<Tykki> tykit = new List<Tykki>();

    /// <summary>
    /// Lista tällähetkellä kentällä olevista panoksista.
    /// </summary>
    private List<Panos> panokset = new List<Panos>();

    /// <summary>
    /// Lista tehdyistä kentistä.
    /// </summary>
    private List<Kentta> kentat = new List<Kentta>();

    /// <summary>
    /// Lista tällähetkellä kentällä olevista impulsseista.
    /// </summary>
    private List<Vector> impulssit = new List<Vector>();

    /// <summary>
    /// Alustaa pelin toiminnan.
    /// </summary>


    public override void Begin()
    {
        this.tykkiTyyppi = Tykki.LuoTavallinenTykki(0, 0, this, true);
        this.tykkiNumero = 1;

        // Luodaan palkki jossa on rahat ja elämät.
        LuoYlaPalkki();
        // Asettaa peliin liittyvät ohjaimet.
        AsetaOhjaimet();

        // Määritetään pelikentän kappaleelle tila ja sitä vastaava char. Käytetään kun tiedostosta ladataan char niin saadaan sitä vastaava tila. 
        // Tässä tapauksessa dictionary ei ole fiksu koska siinä haetaan avainta arvon perusteella.
        TilaToChar.Add(KappaleenTila.Tyhja, 'I');
        TilaToChar.Add(KappaleenTila.TormaysPalikka, 'V');
        TilaToChar.Add(KappaleenTila.AloitusPiste, 'S');
        TilaToChar.Add(KappaleenTila.LopetusPiste, 'E');
        TilaToChar.Add(KappaleenTila.Impulssi, 'N');

        // Määritellään ekakenttä.
        // Kentta ekaKentta = new Kentta("ekaKentta.png", "kentta1.txt", "test");
        Kentta ekaKentta = new Kentta("ruohoKentta.jpg", "ruohoData.txt", "ruohoKulmat.txt");
        Kentta toinenKentta = new Kentta("ekaKentta.png", "kentta1.txt", "test");

        // Laitetaan kentät listaan.
        kentat.Add(ekaKentta);
        kentat.Add(toinenKentta);

        // Luodaan näkymätön tausta koko pelikentälle, joka ottaa hiiren klikkauksia vastaan.
        // Se pitää olla jotta saa tapahtuman käsittelijän toimivaan hiirellä tykkien lisäämistä varten.
        GameObject kenttanLapinakuvatonTausta = new GameObject(kentanLeveys * 2, kentanKorkeus * 2, Shape.Rectangle,0,0);
        kenttanLapinakuvatonTausta.IsVisible = false;
        this.Add(kenttanLapinakuvatonTausta);

        // Alustaa kaikki impulssit nollaksi kentällä.
        for (int i = 0; i < ruudukonKokoX*ruudukonKokoY; i++)
        {
            impulssit.Add(Vector.Zero);
        }

        // Laitetaan hiiren vasemmalle näppäimelle tapahtuman käsittelijä tykin lisäämistä varten.
        Mouse.ListenOn(kenttanLapinakuvatonTausta, MouseButton.Left, ButtonState.Pressed, fykisksi_obojekti => 
        {
            Vector hiirenSijainti = Mouse.PositionOnWorld;

            // Rahat eivät riitä. Poistutaa.
            if (rahaLaskuri.Value < tykkiTyyppi.GetRahaArvo())
            {
                   MessageDisplay.Add("Rahat eivät riitä");
                   return;
            }

            // Tykkejä ei ole entuudestaan. Luodaan tykki ja poistutaan.
            if (tykit.Count == 0)
            {
                  LuoTykki(hiirenSijainti);
                  rahaLaskuri.Value -= tykkiTyyppi.GetRahaArvo();
                  return;
            }

            //Käydään tykkien ja reunojen etäisyyksiä läpi. Jos kerrankin etäisyys on liian pieni. Poistutaan.
            foreach (var reuna in kentanTekoLista)
            {
                Vector reunanSijainti = reuna.GetPhysicsObject().Position;
                double tykinEtaisyysReunaan = reunanSijainti.Distance(hiirenSijainti);

                if (tykinEtaisyysReunaan < reuna.GetPhysicsObject().Width)
                {
                    MessageDisplay.Add("Tykkejä ei saa laittaa kulku tielle");
                    return;
                }
            }

            //Käydään tykkejä ja etäisyyksiä läpi. Jos kerrankin etäisyys on liian pieni. Poistutaan.
            foreach (var tykki in tykit)
            {
                Vector tykinSijainti = tykki.GetPhysicsObject().Position;
                double tykinEtaisyysHiireen = tykinSijainti.Distance(hiirenSijainti);

                if (tykinEtaisyysHiireen < tykki.GetPhysicsObject().Width)
                {
                    MessageDisplay.Add("Tykkejä ei saa laitaa päällekkäin");
                    return;
                }
            }

            // Kaikki ok. Luodaan tykki.
            LuoTykki(hiirenSijainti);
            rahaLaskuri.Value -= tykkiTyyppi.GetRahaArvo();

        }, null, kenttanLapinakuvatonTausta);

        vaihdaKentta();

        // Asetetaan ikkunan koko.
        SetWindowSize(kentanLeveys, kentanKorkeus);

        // Käydään kentänteko listan läpi ja lisäätään aloituspisteet omaan listaansa.
        // KeraaAloitusPisteet();

        // Asetetaan laskurit alkuarvoonsa ja laitetaan vihollisten generointi käyntiin.
        LuoPeli();
    }


    /// <summary>
    /// Vaihtaa kentän.
    /// </summary>
    public void vaihdaKentta()
    {
        ResetoiPeli();

        foreach(var x in kentanTekoLista)
        {
            x.GetPhysicsObject().Destroy();
        }

        kentanTekoLista.Clear();

        impulssit.Clear();

        aloitusPisteet.Clear();

        // Alustaa kaikki impulssit nollaksi kentällä.
        for (int i = 0; i < ruudukonKokoX * ruudukonKokoY; i++)
        {
            impulssit.Add(Vector.Zero);
        }

        int kenttienLukuMaara = kentat.Count;

        kentanNumero = (kentanNumero + 1) % kenttienLukuMaara;

        Kentta kentta = kentat[kentanNumero];

        // Ladataan ja muodostetaan staattiset kappaleet ja impulssit kenttään.
        LoadMapData(kentta.AnnakenttaDatatiedostonNimi(), kentta.AnnaKulmatDatatiedostonNimi());

        // Asetetaan tausta kuva.
        Level.Background.Image = Image.FromFile(kentta.AnnaTaustaKuvanNimi());

        // Käydään kentänteko listan läpi ja lisäätään aloituspisteet omaan listaansa.
        KeraaAloitusPisteet();

        LuoPeli();

    }


    /// <summary>
    /// Etsii lähimmän vihollisen tykistä.
    /// </summary>
    /// <param name="position">Tykin sijainti</param>
    /// <returns>Lähimmän vihollisen sijainnin</returns>
    public Vector EtsiLahinVihollinen(Vector position)
    {
        double lyhinEtaisyys = ulong.MaxValue;
        Vector sijainti = new Vector(kentanLeveys*2, kentanKorkeus*2);
        
        for (int i = 0; i < viholliset.Count; i++)
        {
            
            double etaisyys = Vector.Distance(viholliset[i].GetTormausKappale().Position, position);

            // Löytyi uusi lyhyin etäisyys.
            if ( lyhinEtaisyys > etaisyys)
            {
                lyhinEtaisyys = etaisyys;
                sijainti = viholliset[i].GetTormausKappale().Position;
            }

        }

        return sijainti;
    }


    /// <summary>
    /// Luo panoksen kentälle, joka lähtee tykistä.
    /// </summary>
    /// <param name="panoksenSijainti"></param>
    /// <param name="suunta"></param>
    /// <param name="panos">Tykin panos</param>
    public void LuoPanos(Vector panoksenSijainti, Vector suunta, Panos panos)
    {
        
        Add(panos.GetPhysicsObject());
        AddCollisionHandler(panos.GetPhysicsObject(), KasitteleAmmuksenTormays);
        panos.GetPhysicsObject().CollisionIgnoreGroup = 1;
        panokset.Add(panos);

    }


    /// <summary>
    /// Käy kentänteko listan läpi ja lisää aloituspisteet omaan listaansa.
    /// </summary>
    public void KeraaAloitusPisteet()
    {
        for (int i = 0; i < kentanTekoLista.Count; i++)
        {
            // StaattinenKenttaObjekti temp = kentanTekoLista[i];

            if (kentanTekoLista[i].GetKappaleenTila() == KappaleenTila.AloitusPiste)
            {
                aloitusPisteet.Add(kentanTekoLista[i]);
            }
        }
    }


    /// <summary>
    /// Luo kenttään ja tykkilistaan uuden tykin riippuen tykkin numerosta.
    /// </summary>
    /// <param name="tykinsijainti">tykinsijainti</param>
    public void LuoTykki(Vector tykinsijainti)
    {
        Tykki tykki = null;
        if (tykkiNumero == 1)
        {
            tykki = Tykki.LuoTavallinenTykki(tykinsijainti.X, tykinsijainti.Y, this, false);

        }
        else if (tykkiNumero == 2)
        {
            tykki = Tykki.LuoArnold(tykinsijainti.X, tykinsijainti.Y, this, false);

        }
        else if (tykkiNumero == 3)
        {
            tykki = Tykki.LuoLiekinHeitin(tykinsijainti.X, tykinsijainti.Y, this, false);

        }
        else if (tykkiNumero == 4)
        {
            tykki = Tykki.LuoSniperTykki(tykinsijainti.X, tykinsijainti.Y, this, false);

        }

        // TODO: heitä poikkeus tmv.
        if (tykki == null) return;

        this.Add(tykki.GetPhysicsObject());
        tykki.GetPhysicsObject().CollisionIgnoreGroup = 1;
        tykit.Add(tykki);
        
        
    }


    /// <summary>
    /// Luo ajastimen, joka generoi vihollisen kahden sekunnin välein. TODO: tuo aika parametrina.
    /// </summary>
    public void LuoVihollisenGenerointiAjastin()
    {
        Timer ajastin = Timer.CreateAndStart(2, SpawnaaVihollinen);

    }


    /// <summary>
    /// Luo aloitus pisteestä vihollisia.
    /// </summary>
    public void SpawnaaVihollinen()
    {
       for (int i = 0;i < aloitusPisteet.Count; i++)
        {
            if (spawnausLaskuri.MinValue != spawnausLaskuri.Value)
            {
                spawnausLaskuri.AddValue(-1);
                Vector temp = aloitusPisteet[i].GetPosition();
                double vihollisenX = temp.X;
                double vihollisenY = temp.Y;
                LuoVihollinen(vihollisenX, vihollisenY);
                vihollisLaskuri.AddValue(1);
            }
        }
       
    }


    /// <summary>
    /// Tuhoaa vihollisen pelistä sekä viholliset listasta.
    /// </summary>
    /// <param name="vihollinen">vihollinen</param>
    public void TuhoaVihollinen(Vihollinen vihollinen)
    {
        vihollinen.GetTormausKappale().Destroy();
        rahaLaskuri.AddValue(vihollinen.GetRaha_Arvo());
        viholliset.Remove(vihollinen);
        vihollisLaskuri.AddValue(-1);
        if (spawnausLaskuri.MinValue == spawnausLaskuri.Value && vihollisLaskuri.Value == 0)
        {
            ResetoiPeli();
            MessageDisplay.Add("Sinä voitit");
        }

    }


    /// <summary>
    /// Tuhoaa panoksen pelistä sekä panos listasta.
    /// </summary>
    /// <param name="panos">Tykin panos</param>
    public void TuhoaPanos(Panos panos)
    {
        panos.GetPhysicsObject().Destroy();
        panokset.Remove(panos);

    }

    /// <summary>
    /// Luo vihollis objekti haluttuun koortinaattiin kentälle.
    /// Siinä on hieman satunnaisuutta kun se arpoo vihollisia.
    /// </summary>
    /// <param name="x">x-koordinaatti</param>
    /// <param name="y">y-koordinaatti</param>
    public void LuoVihollinen(double x, double y)
    {

        Vihollinen vihollinen = null;

        int vihollisenNumero = RandomGen.NextInt(0, 100);
            
        if (vihollisenNumero >= 0 && vihollisenNumero < 60)
        {
           vihollinen = new Vihollinen(10, 10, x, y, Shape.Circle, Color.Red, 10, 20, 25);
        }
        else if(vihollisenNumero >= 60 && vihollisenNumero < 95)
        {
            vihollinen = new Vihollinen(15, 15, x, y, Shape.Circle, Color.Green, 20, 40, 20);
        }
        else if (vihollisenNumero >= 95 && vihollisenNumero < 100)
        {
            vihollinen = new Vihollinen(25, 25, x, y, Shape.Circle, Color.Black, 50, 100, 50);
        }

        viholliset.Add(vihollinen);

        Timer ajastin = Timer.CreateAndStart(0.1, () =>
        {
            foreach (var vihollinen in viholliset)
            {
                Vector vihollisenSijainti = vihollinen.GetTormausKappale().Position + new Vector(kentanLeveys, kentanKorkeus);
                Vector temp = new Vector(vihollisenSijainti.X / (kentanLeveys * 2) * ruudukonKokoX, vihollisenSijainti.Y / (kentanKorkeus * 2) * ruudukonKokoY);
                // System.Console.WriteLine("{0}", temp.ToString());
                int listanIndeksi = General.anna_indeksi((int)temp.X, (int)temp.Y, ruudukonKokoX);
                Vector impulssi = impulssit[listanIndeksi];
                vihollinen.GetTormausKappale().Hit(impulssi * 5.0); // 1.1
            }
        }
        );
        
        PhysicsObject kappale = vihollinen.GetTormausKappale();
        Vector impulssi = new Vector(RandomGen.NextDouble(0, 5), RandomGen.NextDouble(0, 5));
        kappale.Hit(impulssi * kappale.Mass);
        //kappale.Restitution = 1.4;
        //Mouse.ListenOn(kappale, MouseButton.Left, ButtonState.Pressed, x => { TuhoaVihollinen(vihollinen); }, null, kappale);

        AddCollisionHandler(kappale, KasitteleVihollisenTormaus);
        this.Add(kappale);
    }


    /// <summary>
    /// Tapahtuman käsittelijä vihollisen ja jonkun muun asian välillä.
    /// Jos vihollinen törmää lopetus pisteeseen se tuhoituu ja elämää häviää.
    /// </summary>
    /// <param name="vihollinen">Kentän vihollinen</param>
    /// <param name="kohde">Kappale johon törmättiin</param>
    public void KasitteleVihollisenTormaus(PhysicsObject vihollinen, PhysicsObject kohde)
    {
        Vihollinen v = null;

        // käydään vihollis lista läpi ja selvitetään mille viholliselle törmäyskappale kuuluu.
        foreach (var vihu in viholliset)
        {
            if (Object.ReferenceEquals(vihu.GetTormausKappale(), vihollinen))
            {
                v = vihu;
                break;
            }
        }

        if (v == null)
        {
            System.Console.WriteLine("vihollinen jäi nulliksi");
            return;
        }

        foreach(var kentanPalanen in kentanTekoLista)
        {
            if (Object.ReferenceEquals(kentanPalanen.GetPhysicsObject(), kohde))
            {
                if (kentanPalanen.GetKappaleenTila()  == KappaleenTila.LopetusPiste)
                {
                    vihollinen.Destroy();
                    pisteLaskuri.AddValue(-1);
                    break;
                }
            }
        }

        Tykki t = null;
        foreach(var tykki in tykit)
        {
            if (Object.ReferenceEquals(tykki.GetPhysicsObject(), kohde))
            {
                int vahinko = v.GetVahinko();
                int tykinUusiElama = tykki.AiheutaVahinko(vahinko);
                if (tykinUusiElama <= 0)
                {
                    tykki.GetPhysicsObject().Destroy();
                    t = tykki;
                    break;

                }
            }
        }
        if (t != null)
        {
            t.GetAjastin().Stop();
            tykit.Remove(t);
        }
    }


    /// <summary>
    /// Käsittelee panoksen törmäyksen.
    /// Jos panos törmää viholliseen, se tuhoutuu ja vihollinen menettää elämää.
    /// Vihollinen tuhotaan jos elämät ovat alle nollan.
    /// </summary>
    /// <param name="ammus">Tykin panos</param>
    /// <param name="kohde">Kappale johon törmättiin</param>
    public void KasitteleAmmuksenTormays(PhysicsObject ammus, PhysicsObject kohde)
    {
        foreach (var vihollinen in viholliset)
        {
            
            if (Object.ReferenceEquals(vihollinen.GetTormausKappale(), kohde))
            {

                Panos tuhottava_panos = null;
                foreach (var panos in panokset)
                {
                    

                    if (Object.ReferenceEquals(panos.GetPhysicsObject(), ammus))
                    {
                        tuhottava_panos = panos;
                        break;
                    }
                }

                if (tuhottava_panos != null)
                {
                    TuhoaPanos(tuhottava_panos);

                    int vihollisenElama = vihollinen.TeeVahinko(tuhottava_panos.GetDamage());
                    
                    if (vihollisenElama <= 0)
                    {
                        TuhoaVihollinen(vihollinen);
                    }
                    break;
                }

            }
        }
    }


    /// <summary>
    /// Poistaa myös kaikki tykit, panokset ja viholliset.
    /// </summary>
    public void ResetoiPeli()
    {
        this.ClearTimers();
        foreach (var vihollinen in viholliset)
        {
            vihollinen.GetTormausKappale().Destroy();
        }
        viholliset.Clear();
        foreach(var tykki in tykit)
        {
            tykki.GetPhysicsObject().Destroy();
        }
        tykit.Clear();
        foreach(var panos in panokset)
        {
            panos.GetPhysicsObject().Destroy();
        }
        panokset.Clear();
        
    }


    /// <summary>
    /// Aloittaa uuden pelin, asettaa laskurit aloitus arvoon ja aloittaa vihollisten generoinin.
    /// </summary>
    public void LuoPeli()
    {
        ResetoiPeli();
        this.rahaLaskuri.Value = 200;
        this.pisteLaskuri.Value = 20;
        this.vihollisLaskuri.Value = 0;
        this.spawnausLaskuri.Value= 50;
        LuoVihollisenGenerointiAjastin();
    }


    /// <summary>
    /// Luo kentän yläosaan valkoisen palkin ja luo pistelaskurit sinne.
    /// </summary>
    public void LuoYlaPalkki()
    {
        GameObject palkki = new GameObject(kentanLeveys, 125, Shape.Rectangle, 0, 500);
        Add(palkki);
        pisteLaskuri = LuoPisteLaskuri(-400, 450);
        LuoRahaLaskuri(400, 450, 50);
        LuoSpawnausLaskuri(200, 450);
        LuoVihollisLaskuri();
        pisteLaskuri.AddTrigger(0,
                         TriggerDirection.Down,
                         x => {
                             MessageDisplay.Add("Tappio");
                             ResetoiPeli();
                         });
    }


    /// <summary>
    /// Luo piste laskurin pelaajan rahoille.
    /// </summary>
    /// <param name="x">Pistelaskurin x koordinaatti</param>
    /// <param name="y">Pistelaskurin y koordinaatti</param>
    /// <param name="raha">joku summa rahaa</param>
    void LuoRahaLaskuri(double x, double y, int raha)
    {
        this.rahaLaskuri = new IntMeter(raha);
        this.rahaLaskuri.MaxValue = 100000;
        Label naytto = new Label();
        naytto.BindTo(this.rahaLaskuri);
        naytto.X = x;
        naytto.Y = y;
        naytto.TextColor = Color.Blue;
        naytto.BorderColor = Level.Background.Color;
        naytto.Color = Level.Background.Color;
        Add(naytto);


    }


    /// <summary>
    /// Luo spawnaus laskurin.
    /// </summary>
    /// <param name="x">Pistelaskurin x koordinaatti</param>
    /// <param name="y">Pistelaskurin y koordinaatti</param>
    void LuoSpawnausLaskuri(double x, double y)
    {
        spawnausLaskuri = new IntMeter(10, 0, 1000);
        Label naytto = new Label();
        naytto.BindTo(spawnausLaskuri);
        naytto.X = x;
        naytto.Y = y;
        naytto.TextColor = Color.Green;
        naytto.BorderColor = Level.Background.Color;
        naytto.Color = Level.Background.Color;
        Add(naytto);


    }


    /// <summary>
    /// Luo vihollisLaskurin vihollisille.
    /// </summary>
    void LuoVihollisLaskuri()
    {
        vihollisLaskuri = new IntMeter(0, 0, 1000);

    }


    /// <summary>
    /// Luo piste laskurin pelaajan elämälle.
    /// </summary>
    /// <param name="x">Pistelaskurin x koordinaatti</param>
    /// <param name="y">Pistelaskurin y koordinaatti</param>
    /// <returns>Pistelaskuri</returns>
    IntMeter LuoPisteLaskuri(double x, double y)
    {
        IntMeter laskuri = new IntMeter(20);
        laskuri.MaxValue = 20;
        Label naytto = new Label();
        naytto.BindTo(laskuri);
        naytto.X = x;
        naytto.Y = y;
        naytto.TextColor = Color.Red;
        naytto.BorderColor = Level.Background.Color;
        naytto.Color = Level.Background.Color;
        Add(naytto);


        return laskuri;
    }


    /// <summary>
    /// Luo kentälle seuraavanlaisia objektin: reuna, aloituspiste, loputuspiste, tyhjä
    /// </summary>
    /// <param name="width">Leveys</param>
    /// <param name="height">Korkeus</param>
    /// <param name="x">x koordinaatti</param>
    /// <param name="y">y koordinaatti</param>
    /// <param name="s">Kappaleen muoto</param>
    /// <param name="c">Kappaleen väri</param>
    /// <param name="k">Kappaleen tila, eli vaikka aloituspiste</param>
    public void LuoStaattinenKenttaObjekti(double width, double height, double x, double y, Shape s, Color c, KappaleenTila k)
    {
        //GameObject pallo = new GameObject(width, height, x, y);
        StaattinenKenttaObjekti sk = new StaattinenKenttaObjekti(width, height, x, y, s);
        sk.AsetaKappaleenTila(k);
        kentanTekoLista.Add(sk);
        PhysicsObject obj = sk.GetPhysicsObject();
        obj.CollisionIgnoreGroup = 1;
        Add(obj);

    }


    /// <summary>
    /// Lataa ja muodostaa staattiset kappaleet ja impulssit kenttään.
    /// Kentän data on tällä yksi merkkijono joka koostuu kirjaimista (I == tyhjä, V == staattinen törmäsykappale, S == aloitus piste, E == lopeus piste). 
    /// </summary>
    /// <param name="kentta">kentän data</param>
    /// <param name="kulmatTiedosto">impulssin kulmien data</param>
    public void LoadMapData(string kentta, string kulmatTiedosto)
    {

        int doubleLaskuri = 0;

        List<double> kulmatLista = General.LueDoublet(kulmatTiedosto);

            
            string kentanDataMerkkijonona = System.IO.File.ReadAllText(kentta);

            for (int i = 0; i < kentanDataMerkkijonona.Length; i++)
            {
                var kappaleenTila = TilaToChar.FirstOrDefault(x => x.Value == kentanDataMerkkijonona[i]).Key;

                if (kappaleenTila == KappaleenTila.Tyhja)
                {
                  continue;
                }
                if (kappaleenTila == KappaleenTila.Impulssi)
                {

                    Vector vector = Vector.FromAngle(Angle.FromDegrees(kulmatLista[doubleLaskuri]));
                    //System.Console.WriteLine("vector = {0}", vector.ToString());
                    impulssit[i] = vector * 0.3;
                    doubleLaskuri++;

                    continue;
                }
                
                Vector pallonSijainti = laskeSijaintiRuudukolla(i, ruudukonKokoX);

                // säde
                double r = (double)kentanLeveys * 2.0 / (double)ruudukonKokoX;

                LuoStaattinenKenttaObjekti(r, r, pallonSijainti.X, pallonSijainti.Y, Shape.Circle, Color.White, kappaleenTila);

            }
    }


    /// <summary>
    /// Funktio, joka laskee yksiulotteisen "taulukon" indeksin perusteella paikan kaksi ulotteisessa ruudukossa.
    /// 
    /// Esimerkiksi jos ruudukon leveys on 7 niin esimerkiksi
    /// laskeSijaintiRuudukolla(11, 7) == Vector(4, 1).
    /// 
    /// Tätä voi hyödyntää esimerkiksi niin että mäpätään yksiulotteisen taulukon indeksi kaksiulotteisen ruudukon indeksiin.
    /// 
    /// +--+--+--+--+--+--+--+
    /// |14|15|16|17|18|19|20|
    /// +--+--+--+--+--+--+--+
    /// | 7| 8| 9|10|11|12|13|
    /// +--+--+--+--+--+--+--+
    /// | 0| 1| 2| 3| 4| 5| 6|
    /// +--+--+--+--+--+--+--+
    /// 
    /// |-- ruudukon leveys -|
    /// 
    /// </summary>
    /// <param name="indeksi">Annettu yksiulotteinen indeksi</param>
    /// <param name="ruudukon_leveys">Kaksiulottisen ruudukon leveys.</param>
    /// <returns>(x,y) koordinaatti ruudukolla.</returns>
    public static Vector laskeSijaintiRuudukolla(int indeksi, int ruudukon_leveys)
    {
        Vector v = General.anna_vektori(indeksi, ruudukon_leveys);
        double r = (double)kentanLeveys * 2.0 / (double)ruudukonKokoX;
        double xKoordinaatti = 0.5 * r + (double)v.X * r - kentanLeveys;
        double yKoordinaatti = 0.5 * r + (double)v.Y * r - kentanKorkeus;
        return new Vector(xKoordinaatti, yKoordinaatti);
    }


    /// <summary>
    /// Asettaa peliin liittyvät ohjaimet
    /// </summary>
    void AsetaOhjaimet()
    {
        Keyboard.Listen(Key.D1,
                 ButtonState.Down,
                 delegate () {
                     
                     tykkiTyyppi = Tykki.LuoTavallinenTykki(0,0,this,true);
                     tykkiNumero = 1;
                     MessageDisplay.Add("TavallinenTykki Valittu");
                 },
                 "PerusTykki"
        );
        Keyboard.Listen(Key.D2,
            ButtonState.Released,
            delegate () {
                tykkiTyyppi = Tykki.LuoArnold(0, 0, this, true);
                tykkiNumero = 2;
                MessageDisplay.Add("Minigun Valittu");
            },
         "Minigun"
        );

        Keyboard.Listen(Key.D3,
                        ButtonState.Down,
                        delegate () {

                            tykkiTyyppi = Tykki.LuoLiekinHeitin(0, 0, this, true);
                            tykkiNumero = 3;
                            MessageDisplay.Add("Liekinheitin Valittu");
                        },
                        "LiekinHeitin"
        );

        Keyboard.Listen(Key.D4,
                ButtonState.Down,
                delegate () {

                    tykkiTyyppi = Tykki.LuoSniperTykki(0, 0, this, true);
                    tykkiNumero = 4;
                    MessageDisplay.Add("Sniper Valittu");

                },
                "Sniper"
        );
     
        Keyboard.Listen(Key.U,
         ButtonState.Down,
         LuoPeli,
         "aloitaPeli"
        );

        ;

        Keyboard.Listen(Key.V,
         ButtonState.Down,
         vaihdaKentta,
         "vaihda kenttä"
        );

        PhoneBackButton.Listen(ConfirmExit, "Lopeta peli");
        Keyboard.Listen(Key.Escape, ButtonState.Pressed, ConfirmExit, "Lopeta peli");
    }
}

