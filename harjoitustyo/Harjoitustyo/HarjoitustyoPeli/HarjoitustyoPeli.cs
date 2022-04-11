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
    private int damage = 0;
    private int nopeus = 0;

    private PhysicsObject fysiikkaAmmus;
    /// <summary>
    /// 
    /// </summary>
    /// <param name="width"></param>
    /// <param name="height"></param>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="s"></param>
    /// <param name="c"></param>
    /// <param name="damage"></param>
    /// <param name="nopeus"></param>
    /// <param name="suunta"></param>
    /// <param name="elinAika"></param>
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
    public static Panos LuoTavallinenPanos(double x, double y, Vector suunta)
    {
        Panos panos = new Panos(10, 10, x, y, Shape.Circle, Color.Charcoal, 20, 11, suunta, TimeSpan.FromSeconds(5.0));
        return panos;
    }
    public static Panos LuoKuulaPanos(double x, double y, Vector suunta)
    {
        Panos panos = new Panos(6, 3, x, y, Shape.Circle, Color.Black, 2, 9, suunta, TimeSpan.FromSeconds(2.0));
        return panos;
    }
    public static Panos LuoTuliPanos(double x, double y, Vector suunta)
    {
        Panos panos = new Panos(3, 3, x, y, Shape.Circle, Color.Red, 3, 4, suunta, TimeSpan.FromSeconds(0.2));
        return panos;
    }
    public static Panos LuoSnipePanos(double x, double y, Vector suunta)
    {
        Panos panos = new Panos(15, 10, x, y, Shape.Circle, Color.Charcoal, 35, 30, suunta, TimeSpan.FromSeconds(10.0));
        return panos;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public PhysicsObject GetPhysicsObject()
    {
        return this.fysiikkaAmmus;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public int GetDamage()
    {
        return this.damage;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public int GetNopeus()
    {
        return this.nopeus;
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
    private int raha_arvo = 10;
    private int elama = 0;
    private int range = 0;
    private Timer ajastin = null;

    private PhysicsObject fysiikkaObjekti;
    /// <summary>
    /// 
    /// </summary>
    /// <param name="width"></param>
    /// <param name="height"></param>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="s"></param>
    /// <param name="c"></param>
    /// <param name="raha_arvo"></param>
    /// <param name="elama"></param>
    /// <param name="range"></param>
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
    public void SetAjastin(Timer ajastin)
    {
        this.ajastin = ajastin;
    }
    public Timer GetAjastin()
    {
        return this.ajastin;
    }


    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public int AiheutaVahinko(int vahingonMaara)
    {
        this.elama = this.elama - vahingonMaara;
        return this.elama;
    }
    public int GetRahaArvo()
    {

        return this.raha_arvo;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="peli"></param>
    /// <param name="onTykkiTyyppi"></param>
    /// <returns></returns>
    public static Tykki LuoTavallinenTykki(double x, double y, HarjoitustyoPeli peli, bool onTykkiTyyppi)
    {
        Tykki tykki = new Tykki(15, 15, x, y, Shape.Rectangle, Color.Azure, 25, 50, 300);
        if (onTykkiTyyppi)
        {
            return tykki;
        }
        
        Timer ajastin = Timer.CreateAndStart(3, () =>
        {
            Vector tykinPositio = tykki.GetPhysicsObject().Position;
            Vector vihollisenSijainti = peli.EtsiLahinVihollinen(tykinPositio);
            double tykinJaVihollisenVälinenSijainti = Vector.Distance(vihollisenSijainti, tykinPositio);
            if (tykinJaVihollisenVälinenSijainti <= tykki.range)
            {
                // Lasketaan tykin panoksen suunta.
                Vector suunta = (vihollisenSijainti - tykinPositio) / tykinJaVihollisenVälinenSijainti;
                Vector impulssi = suunta;

                peli.LuoPanos(tykki.GetPhysicsObject().Position, suunta, Panos.LuoTavallinenPanos(tykinPositio.X, tykinPositio.Y, impulssi * 4));
            }
            
            

        }
        );
        tykki.SetAjastin(ajastin);
        



        return tykki;
    }
    public static Tykki LuoArnold(double x, double y, HarjoitustyoPeli peli, bool onTykkiTyyppi)
    {
        Tykki tykki = new Tykki(30, 30, x, y, Shape.Rectangle, Color.Black, 100, 50, 150);
        if (onTykkiTyyppi)
        {
            return tykki;
        }
        Timer ajastin = Timer.CreateAndStart(0.5, () =>
        {
            Vector tykinPositio = tykki.GetPhysicsObject().Position;
            Vector vihollisenSijainti = peli.EtsiLahinVihollinen(tykinPositio);
            double tykinJaVihollisenVälinenSijainti = Vector.Distance(vihollisenSijainti, tykinPositio);
            if (tykinJaVihollisenVälinenSijainti <= tykki.range)
            {
                // Lasketaan tykin panoksen suunta.
                Vector suunta = (vihollisenSijainti - tykinPositio) / tykinJaVihollisenVälinenSijainti;
                Vector impulssi = suunta;

                peli.LuoPanos(tykki.GetPhysicsObject().Position, suunta, Panos.LuoKuulaPanos(tykinPositio.X, tykinPositio.Y, impulssi * 5));
            }



        }
        );
        tykki.SetAjastin(ajastin);
        return tykki;
    }
    public static Tykki LuoLiekinHeitin(double x, double y, HarjoitustyoPeli peli, bool onTykkiTyyppi)
    {
        Tykki tykki = new Tykki(15, 15, x, y, Shape.Rectangle, Color.Red, 150, 50, 100);
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
                Vector impulssi = suunta;

                peli.LuoPanos(tykki.GetPhysicsObject().Position, suunta, Panos.LuoTuliPanos(tykinPositio.X, tykinPositio.Y, impulssi * 0.5));
            }



        }
        );
        tykki.SetAjastin(ajastin);
        return tykki;
    }
    public static Tykki LuoSniperTykki(double x, double y, HarjoitustyoPeli peli, bool onTykkiTyyppi)
    {
        Tykki tykki = new Tykki(15, 15, x, y, Shape.Rectangle, Color.Green, 75, 50, 750);
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
                Vector impulssi = suunta;

                peli.LuoPanos(tykki.GetPhysicsObject().Position, suunta, Panos.LuoSnipePanos(tykinPositio.X, tykinPositio.Y, impulssi * 6));
            }



        }
        );
        tykki.SetAjastin(ajastin);
        return tykki;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public PhysicsObject GetPhysicsObject()
    {
        return this.fysiikkaObjekti;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public int GetRange()
    {
        return this.range;
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
    private int raha_arvo = 10;
    private int elama = 0;
    private int vahinko = 0;

    private PhysicsObject tormausKappale;
    /// <summary>
    /// 
    /// </summary>
    /// <param name="width"></param>
    /// <param name="height"></param>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="s"></param>
    /// <param name="c"></param>
    /// <param name="raha_arvo"></param>
    /// <param name="elama"></param>
    public Vihollinen(double width, double height, double x, double y, Shape s, Color c, int raha_arvo, int elama, int vahinko)
    {
        PhysicsObject tormausKappale = new PhysicsObject(width, height, x, y);
        tormausKappale.Color = c;
        tormausKappale.Shape = s;
        this.raha_arvo = raha_arvo;
        this.elama = elama;
        this.vahinko = vahinko;
        this.tormausKappale = tormausKappale;   
    }
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public PhysicsObject GetTormausKappale()
    {
        return this.tormausKappale;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns
    public int GetVahinko()
    {
        return this.vahinko;
    }
    
    public int GetElama()
    {
        return this.elama;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="elama"></param>
    public void SetElama(int elama)
    {
        this.elama = elama;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public int GetRaha_Arvo()
    {
        return this.raha_arvo;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="vahinko"></param>
    /// <returns></returns>
    public int TeeVahinko(int vahinko)
    {
        
        this.elama -= vahinko;
        // System.Console.WriteLine("TeeVahinko({0} == {1}", vahinko, this.elama);
        return this.elama;
    }
    public static Vihollinen LuoVoimakkaampiVihollinen(double x, double y)
    {
        Vihollinen vihreaVihollinen = new Vihollinen(15, 15, x, y, Shape.Circle, Color.Green, 20, 40, 20);
        return vihreaVihollinen;
    }
    public static Vihollinen LuoPieniVihollinen(double x, double y)
    {
        Vihollinen vihreaVihollinen = new Vihollinen(10, 10, x, y, Shape.Circle, Color.Red, 10, 20, 25);
        return vihreaVihollinen;
    }
    public static Vihollinen LuoTankkiVihollinen(double x, double y)
    {
        Vihollinen TankkiVihollinen = new Vihollinen(25, 25, x, y, Shape.Circle, Color.Black, 50, 100, 50);
        return TankkiVihollinen;
    }
}
/// @author  Eemil Kauppinen
/// @version 4.4.2022
/// 
/// <summary>
/// Määrittelee kentän reunan ominaisuudet
/// </summary>
public class StaattinenKenttaObjekti // : PhysicsObject
{
    private PhysicsObject kenttaObjekti;
    private KappaleenTila tila = KappaleenTila.Tyhja;

    /// <summary>
    /// 
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
        AsetaLaskurinArvo(this.tila);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public PhysicsObject GetPhysicsObject()
    {
        return this.kenttaObjekti;
    }
    

    /// <summary>
    /// 
    /// </summary>
    /// <param name="tila"></param>
    /// <exception cref="ArgumentException"></exception>
    public void AsetaLaskurinArvo(KappaleenTila tila)
    {
        if ((int)KappaleenTila.Laiton <= (int)tila || (int)tila < 0)
        {
            throw new ArgumentException("Laskurin arvo oli ihan puuta-heinää");
        }
        this.tila = tila;
        MuutaKappaleenTila();

    }
    /// <summary>
    /// 
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
    /// 
    /// </summary>
    /// <returns></returns>
    public Vector GetPosition()
    {
        return kenttaObjekti.Position;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public KappaleenTila PalautaKappaleenTila()
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
    Dictionary<KappaleenTila, char> TilaToChar = new Dictionary<KappaleenTila, char>();
    private static int ruudukonKokoX = 128;
    private static int ruudukonKokoY = 64;
    private static int kentanLeveys = 1000;
    private static int kentanKorkeus = 500;

    private IntMeter pisteLaskuri;
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
    /// 
    /// </summary>
    private List<Kentta> kentat = new List<Kentta>();
    /// <summary>
    /// 
    /// </summary>
    private List<Vector> impulssit = new List<Vector>();

    public override void Begin()
    {
        this.tykkiTyyppi = Tykki.LuoTavallinenTykki(0, 0, this, true);
        this.tykkiNumero = 1;

        LuoYlaPalkki();
        AsetaOhjaimet();

        TilaToChar.Add(KappaleenTila.Tyhja, 'I');
        TilaToChar.Add(KappaleenTila.TormaysPalikka, 'V');
        TilaToChar.Add(KappaleenTila.AloitusPiste, 'S');
        TilaToChar.Add(KappaleenTila.LopetusPiste, 'E');
        TilaToChar.Add(KappaleenTila.Impulssi, 'N');
        Kentta ekaKentta = new Kentta("ekaKentta.png", "kentta1.txt", "test");
        GameObject kenttanLapinakuvatonTausta = new GameObject(kentanLeveys * 2, kentanKorkeus * 2, Shape.Rectangle,0,0);
        kenttanLapinakuvatonTausta.IsVisible = false;
        this.Add(kenttanLapinakuvatonTausta);

        for (int i = 0; i < ruudukonKokoX*ruudukonKokoY; i++)
        {
            impulssit.Add(Vector.Zero);
        }
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


        LoadMapData(ekaKentta.AnnakenttaDatatiedostonNimi(), ekaKentta.AnnaKulmatDatatiedostonNimi());
        Level.Background.Image = Image.FromFile(ekaKentta.AnnaTaustaKuvanNimi());
        kentat.Add(ekaKentta);
        SetWindowSize(kentanLeveys, kentanKorkeus);
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

            if (kentanTekoLista[i].PalautaKappaleenTila() == KappaleenTila.AloitusPiste)
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

            Vector temp = aloitusPisteet[i].GetPosition();
            double vihollisenX = temp.X;
            double vihollisenY = temp.Y;
            LuoVihollinen(vihollisenX, vihollisenY);
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
           vihollinen = Vihollinen.LuoVoimakkaampiVihollinen(x, y);  
        }
        else if(vihollisenNumero >= 60 && vihollisenNumero < 95)
        {
            vihollinen = Vihollinen.LuoPieniVihollinen(x, y);
        }
        else if (vihollisenNumero >= 95 && vihollisenNumero < 100)
        {
            vihollinen = Vihollinen.LuoTankkiVihollinen(x, y);
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
                vihollinen.GetTormausKappale().Hit(impulssi * 1.1);
            }
        }
        );
        
        PhysicsObject kappale = vihollinen.GetTormausKappale();
        Vector impulssi = new Vector(RandomGen.NextDouble(0, 200), RandomGen.NextDouble(0, 200));
        kappale.Hit(impulssi * kappale.Mass);
        kappale.Restitution = 1.4;
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
                if (kentanPalanen.PalautaKappaleenTila()  == KappaleenTila.LopetusPiste)
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
    /// 
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
    public void LuoPeli()
    {
        ResetoiPeli();
        this.rahaLaskuri.Value = 5000;
        this.pisteLaskuri.Value = 20;
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
        LuoRahaLaskuri(400, 450, 5000);
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
        sk.AsetaLaskurinArvo(k);
        kentanTekoLista.Add(sk);
        PhysicsObject obj = sk.GetPhysicsObject();
        obj.CollisionIgnoreGroup = 1;
        Add(obj);

    }
    /// <summary>
    /// Lataa ja muodostaa staattiset kappaleet kenttään.
    /// Kentän data on tällä yksi merkkijono joka koostuu kirjaimista (I == tyhjä, V == staattinen törmäsykappale, S == aloitus piste, E == lopeus piste). 
    /// </summary>
    /// <param name="kentta">kentän data</param>
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
     
        Keyboard.Listen(Key.Ö,
         ButtonState.Down,
         LuoPeli,
         "aloitaPeli"
        );

        PhoneBackButton.Listen(ConfirmExit, "Lopeta peli");
        Keyboard.Listen(Key.Escape, ButtonState.Pressed, ConfirmExit, "Lopeta peli");
    }
}

