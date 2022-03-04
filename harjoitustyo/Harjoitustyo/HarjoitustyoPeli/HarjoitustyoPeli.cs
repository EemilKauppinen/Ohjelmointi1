using Jypeli;
using Jypeli.Assets;
using Jypeli.Controls;
using Jypeli.Widgets;
using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.IO;

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

    public PhysicsObject GetKenttaObjekti()
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

    List<StaattinenKenttaObjekti> kentanTekoLista = new List<StaattinenKenttaObjekti>();

    List<Vihollinen> viholliset = new List<Vihollinen>();
    
    public override void Begin()
    {
        LuoYlaPalkki();
        TilaToChar.Add(KappaleenTila.Tyhja, 'I');
        TilaToChar.Add(KappaleenTila.TormaysPalikka, 'V');
        TilaToChar.Add(KappaleenTila.AloitusPiste, 'S');
        TilaToChar.Add(KappaleenTila.LopetusPiste, 'E');
        Kentta ekaKentta = new Kentta("ekaKentta.png", "kentta1.txt");

        LoadMapData("kentta1.txt");
        Level.Background.Image = Image.FromFile(ekaKentta.AnnaTaustaKuvanNimi());
        SetWindowSize(kentanLeveys, kentanKorkeus);

        PhoneBackButton.Listen(ConfirmExit, "Lopeta peli");
        Keyboard.Listen(Key.Escape, ButtonState.Pressed, ConfirmExit, "Lopeta peli");

        for (int i = 0; i < 80; i++)
        {
            LuoVihollinen(RandomGen.NextDouble(-900, -890), RandomGen.NextDouble(-490, -480), 10.0, 10.0, Color.Red);
        }

    }
    public void TuhoaVihollinen(Vihollinen vihollinen)
    {
        vihollinen.GetTormausKappale().Destroy();
        rahaLaskuri.AddValue(vihollinen.GetRaha_Arvo());
        viholliset.Remove(vihollinen);

    }
    public void LuoVihollinen(double x, double y, double w, double h, Color c)
    {


        Vihollinen vihollinen = null;

        int vihollisenNumero = RandomGen.NextInt(0, 100);
            
        if (vihollisenNumero >= 0 && vihollisenNumero < 60)
        {
           vihollinen = Vihollinen.LuoVoimakkaampiVihollinen(x, y);  
        }
        else if(vihollisenNumero >= 60 && vihollisenNumero < 95)
        {
            vihollinen = new Vihollinen(w, h, x, y, Shape.Circle, c, 10, 0);
        }
        else if (vihollisenNumero >= 95 && vihollisenNumero < 100)
        {
            vihollinen = Vihollinen.LuoTankkiVihollinen(x, y);
        }
        PhysicsObject kappale = vihollinen.GetTormausKappale();
        viholliset.Add(vihollinen);
        Vector impulssi = new Vector(RandomGen.NextDouble(0, 200), RandomGen.NextDouble(0, 200));
        kappale.Hit(impulssi * kappale.Mass);
        kappale.Restitution = 2.1;
        Mouse.ListenOn(kappale, MouseButton.Left, ButtonState.Pressed, x => { TuhoaVihollinen(vihollinen); }, null, kappale);
        AddCollisionHandler(kappale, KasitteleVihollisenTormaus);
        this.Add(kappale);
    }
    public void KasitteleVihollisenTormaus(PhysicsObject kappale, PhysicsObject kohde)
    {
        foreach(var kentanPalanen in kentanTekoLista)
        {
            if (Object.ReferenceEquals(kentanPalanen.GetKenttaObjekti(), kohde))
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
        Add(sk.GetKenttaObjekti());
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

