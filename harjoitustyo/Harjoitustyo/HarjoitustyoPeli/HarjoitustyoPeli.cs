using Jypeli;
using Jypeli.Assets;
using Jypeli.Controls;
using Jypeli.Widgets;
using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.IO;

public class Tykki
{
    int raha_arvo = 10;
    int elama = 0;

    PhysicsObject fysiikkaObjekti;

    public Tykki(double width, double height, double x, double y, Shape s, Color c, int raha_arvo, int elama)
    {
        PhysicsObject fysiikkaObjekti = new PhysicsObject(width, height, x, y);
        fysiikkaObjekti.Color = c;
        fysiikkaObjekti.Shape = s;
        fysiikkaObjekti.MakeStatic();
        this.raha_arvo = raha_arvo;
        this.elama = elama;
        this.fysiikkaObjekti = fysiikkaObjekti;


    }
    public int GetRahaArvo()
    {

        return this.raha_arvo;
    }
    public static Tykki LuoTavallinenTykki(double x, double y)
    {
        Tykki tykki = new Tykki(15, 15, x, y, Shape.Rectangle, Color.Azure, 25, 50);
        return tykki;
    }
    public PhysicsObject GetPhysicsObject()
    {
        return this.fysiikkaObjekti;
    }


}
public class Vihollinen
{
    int raha_arvo = 10;
    int elama = 0;

    PhysicsObject tormausKappale;
    public Vihollinen(double width, double height, double x, double y, Shape s, Color c, int raha_arvo, int elama)
    {
        PhysicsObject tormausKappale = new PhysicsObject(width, height, x, y);
        tormausKappale.Color = c;
        tormausKappale.Shape = s;
        this.raha_arvo = raha_arvo;
        this.elama = elama;
        this.tormausKappale = tormausKappale;

        
      
    }

    public PhysicsObject GetTormausKappale()
    {
        return this.tormausKappale;
    }
    public int GetElama()
    {
        return this.elama;
    }
    public int GetRaha_Arvo()
    {
        return this.raha_arvo;
    }
    public static Vihollinen LuoVoimakkaampiVihollinen(double x, double y)
    {
        Vihollinen vihreaVihollinen = new Vihollinen(15, 15, x, y, Shape.Circle, Color.Green, 20, 50);
        return vihreaVihollinen;
    }
    public static Vihollinen LuoPieniVihollinen(double x, double y)
    {
        Vihollinen vihreaVihollinen = new Vihollinen(10, 10, x, y, Shape.Circle, Color.Red, 10, 20);
        return vihreaVihollinen;
    }
    public static Vihollinen LuoTankkiVihollinen(double x, double y)
    {
        Vihollinen TankkiVihollinen = new Vihollinen(25, 25, x, y, Shape.Circle, Color.Black, 50, 200);
        return TankkiVihollinen;
    }
}
public class StaattinenKenttaObjekti // : PhysicsObject
{
    PhysicsObject kenttaObjekti;
    KappaleenTila tila = KappaleenTila.Tyhja;


    public StaattinenKenttaObjekti(double width, double height, double x, double y, Shape s) // : base(width, height, x, y)
    {
        PhysicsObject jsfkka = new PhysicsObject(width, height, x , y);
        this.kenttaObjekti = jsfkka;
        // kenttaObjekti.Shape = Shape.Circle;
        // kenttaObjekti.IsVisible = false;
        kenttaObjekti.Color = Color.Black;
        jsfkka.MakeStatic();
        jsfkka.Shape = s;
        AsetaLaskurinArvo(this.tila);
    }

    public PhysicsObject GetPhysicsObject()
    {
        return this.kenttaObjekti;
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
    public Vector GetPosition()
    {
        return kenttaObjekti.Position;
    }
    public KappaleenTila PalautaKappaleenTila()
    {
        return this.tila;
    }
}

public class HarjoitustyoPeli : PhysicsGame
{
    Dictionary<KappaleenTila, char> TilaToChar = new Dictionary<KappaleenTila, char>();
    static int ruudukonKokoX = 128;
    static int ruudukonKokoY = 64;
    static int kentanLeveys = 1000;
    static int kentanKorkeus = 500;

    IntMeter pisteLaskuri;
    IntMeter rahaLaskuri;

    /// <summary>
    /// tykkiTyyppi on valittu tykki tyyppi, jonka voi sijoittaa kenttään.
    /// </summary>
    Tykki tykkiTyyppi = null;

    List<StaattinenKenttaObjekti> kentanTekoLista = new List<StaattinenKenttaObjekti>();

    List<StaattinenKenttaObjekti> aloitusPisteet = new List<StaattinenKenttaObjekti>();

    List<Vihollinen> viholliset = new List<Vihollinen>();

    List<Tykki> tykit = new List<Tykki>();

    public override void Begin()
    {
        this.tykkiTyyppi = Tykki.LuoTavallinenTykki(0, 0);

        LuoYlaPalkki();
        
        TilaToChar.Add(KappaleenTila.Tyhja, 'I');
        TilaToChar.Add(KappaleenTila.TormaysPalikka, 'V');
        TilaToChar.Add(KappaleenTila.AloitusPiste, 'S');
        TilaToChar.Add(KappaleenTila.LopetusPiste, 'E');
        Kentta ekaKentta = new Kentta("ekaKentta.png", "kentta1.txt");

        LuoAikaValeinPalloja();

        GameObject kenttanLapinakuvatonTausta = new GameObject(kentanLeveys * 2, kentanKorkeus * 2, Shape.Rectangle,0,0);
        kenttanLapinakuvatonTausta.IsVisible = false;
        this.Add(kenttanLapinakuvatonTausta);

        

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

        LoadMapData("kentta1.txt");
        Level.Background.Image = Image.FromFile(ekaKentta.AnnaTaustaKuvanNimi());
        
        SetWindowSize(kentanLeveys, kentanKorkeus);

        KeraaAloitusPisteet();

        PhoneBackButton.Listen(ConfirmExit, "Lopeta peli");
        Keyboard.Listen(Key.Escape, ButtonState.Pressed, ConfirmExit, "Lopeta peli");

    }
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
    public void LuoTykki(Vector vektori)
    {
        

        Tykki tykki = Tykki.LuoTavallinenTykki(vektori.X, vektori.Y);
        this.Add(tykki.GetPhysicsObject());
        tykit.Add(tykki);
        
        
    }
    public void LuoAikaValeinPalloja()
    {
        Timer ajastin = Timer.CreateAndStart(2, SpawnaaVihollinen);
    }

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
    public void TuhoaVihollinen(Vihollinen vihollinen)
    {
        vihollinen.GetTormausKappale().Destroy();
        rahaLaskuri.AddValue(vihollinen.GetRaha_Arvo());
        viholliset.Remove(vihollinen);

    }

    /// <summary>
    /// Luo vihollis objekti haluttuun koortinaattiin kentälle.
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

        // 
        PhysicsObject kappale = vihollinen.GetTormausKappale();
        Vector impulssi = new Vector(RandomGen.NextDouble(0, 200), RandomGen.NextDouble(0, 200));
        kappale.Hit(impulssi * kappale.Mass);
        kappale.Restitution = 1.4;
        //Mouse.ListenOn(kappale, MouseButton.Left, ButtonState.Pressed, x => { TuhoaVihollinen(vihollinen); }, null, kappale);

        AddCollisionHandler(kappale, KasitteleVihollisenTormaus);
        this.Add(kappale);
    }
    public void KasitteleVihollisenTormaus(PhysicsObject kappale, PhysicsObject kohde)
    {
        foreach(var kentanPalanen in kentanTekoLista)
        {
            if (Object.ReferenceEquals(kentanPalanen.GetPhysicsObject(), kohde))
            {
                if (kentanPalanen.PalautaKappaleenTila()  == KappaleenTila.LopetusPiste)
                {
                    kappale.Destroy();
                    pisteLaskuri.AddValue(-1);
                    break;
                }
            }
        }
    }
    public void LuoYlaPalkki()
    {
        GameObject palkki = new GameObject(kentanLeveys, 125, Shape.Rectangle, 0, 500);
        Add(palkki);
        pisteLaskuri = LuoPisteLaskuri(-200, 300);
        LuoRahaLaskuri(400, 400, 100);
        pisteLaskuri.AddTrigger(0,
                         TriggerDirection.Down,
                         x => {
                             MessageDisplay.Add("Tappio");
                             foreach (var vihollinen in viholliset)
                             {
                                 vihollinen.GetTormausKappale().Stop();
                             }
                         });
    }
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


    public void LuoStaattinenKenttaObjekti(double width, double height, double x, double y, Shape s, Color c, KappaleenTila k)
    {
        //GameObject pallo = new GameObject(width, height, x, y);
        StaattinenKenttaObjekti sk = new StaattinenKenttaObjekti(width, height, x, y, s);
        sk.AsetaLaskurinArvo(k);
        kentanTekoLista.Add(sk);
        PhysicsObject obj = sk.GetPhysicsObject();
        Add(obj);

    }
    public void LoadMapData(string kentta)
    {
        
        
        string kentanDataMerkkijonona = System.IO.File.ReadAllText(kentta);

            for (int i = 0; i < kentanDataMerkkijonona.Length; i++)
            {
                var kappaleenTila = TilaToChar.FirstOrDefault(x => x.Value == kentanDataMerkkijonona[i]).Key;

                if (kappaleenTila == KappaleenTila.Tyhja)
                {
                  continue;
                }

                Vector pallonSijainti = anna_vektori(i, ruudukonKokoX);

                // säde
                double r = (double)kentanLeveys * 2.0 / (double)ruudukonKokoX;

                LuoStaattinenKenttaObjekti(r, r, pallonSijainti.X, pallonSijainti.Y, Shape.Circle, Color.White, kappaleenTila);


            }
    }

    public static Vector anna_vektori(int indeksi, int ruudukon_leveys)
    {
        int x = indeksi;
        int y = x / ruudukon_leveys;
        x = x - y * ruudukon_leveys;
        double r = (double)kentanLeveys * 2.0 / (double)ruudukonKokoX;
        double xKoordinaatti = 0.5 * r + (double)x * r - kentanLeveys;
        double yKoordinaatti = 0.5 * r + (double)y * r - kentanKorkeus;
        Vector joo = new Vector(xKoordinaatti, yKoordinaatti);
        System.Console.WriteLine("(x: {0}, y: {1}", joo.X, joo.Y);
        return new Vector(xKoordinaatti, yKoordinaatti);
    }
}

