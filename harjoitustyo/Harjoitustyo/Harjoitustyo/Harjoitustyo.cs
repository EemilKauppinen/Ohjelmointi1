using Jypeli;
using Jypeli.Assets;
using Jypeli.Controls;
using Jypeli.Widgets;
using System;
using System.Collections.Generic;
using System.Linq;

public class StaattinenObjecti : GameObject
{

    private bool nakyva = true;

    public StaattinenObjecti(Animation animation) : base(animation)
    {
    }

    public StaattinenObjecti(ILayout layout) : base(layout)
    {
    }

    public StaattinenObjecti(double width, double height) : base(width, height)
    {
    }

    public StaattinenObjecti(double width, double height, Shape shape) : base(width, height, shape)
    {
    }

    public StaattinenObjecti(double width, double height, double x, double y) : base(width, height, x, y)
    {
    }

    public StaattinenObjecti(double width, double height, Shape shape, double x, double y) : base(width, height, shape, x, y)
    {
    }

    public void muutaNakyvyys()
    {
        this.nakyva = !this.nakyva;
        this.IsVisible = this.nakyva;
    }
}

public class Harjoitustyo : PhysicsGame
{
    static int ruudukonKokoX = 30;
    static int ruudukonKokoY = 15;
    static int kentanLeveys = 500;
    static int kentanKorkeus = 250;
    List<PhysicsObject> kentanTekoLista = new List<PhysicsObject>(ruudukonKokoX * ruudukonKokoY);
    public override void Begin()
    {
        //new PhysicsObject(Animation animation)
        AsetaOhjaimet();

        //Level.CreateBorders();

        SetWindowSize(kentanLeveys, kentanKorkeus); // asettaa ikkunan koon
        Camera.ZoomToLevel();

        Level.Background.Image = Image.FromFile("testi.png"); ;

        //GameObject kappale = new GameObject(kentanLeveys, kentanKorkeus , Shape.Rectangle, 0.0, 0.0);

        //kappale.Color = Color.Red;
        //kappale.Image = Image.FromFile("testi.png");

        // Vector impulssi = new Vector(RandomGen.NextDouble(-20, 20), RandomGen.NextDouble(-20, 20));
        //kappale.Hit(impulssi * kappale.Mass);
        // AddCollisionHandler(kappale,KasittelePallonTormays);
        //Add(kappale);

        LuoTestiKentta();

        //LuoLiikkuvaPallo(30, 30, 30, 30, Color.Black);

        //LuoLiikkuvaPallo(0, 0, 30, 30, Color.Black);
        //LuoLiikkuvaPallo(-130, 50, 30, 30, Color.Black);
        //LuoLiikkuvaPallo(-200, -200, 30, 30, Color.Black);

    }



    private void LuoStaattinenObjekti(int x, int y, Shape s, Color c)
    {
        int indeksi = anna_indeksi(x, y);
        double r = (double)kentanLeveys * 2.0 / (double)ruudukonKokoX;
        double xKoordinaatti = 0.5 * r + (double)x * r - kentanLeveys;
        double yKoordinaatti = 0.5 * r + (double)y * r - kentanKorkeus;
        StaattinenObjecti kappale = new StaattinenObjecti(r, r, Shape.Circle);
        kappale.X = xKoordinaatti;
        kappale.Y = yKoordinaatti;
        kappale.Color = c;
        // kappale.MakeStatic();
        Mouse.ListenOn(kappale, MouseButton.Left, ButtonState.Pressed, AktivoiStaattinenPallo, null, kappale);
        //Vector impulssi = new Vector(RandomGen.NextDouble(-20, 20), RandomGen.NextDouble(-20, 20));
        //Vector impulssi = new Vector(20, 500);
        //kappale.Hit(impulssi * kappale.Mass);
        // AddCollisionHandler(kappale,KasittelePallonTormays);
        this.Add(kappale);
    }

    void AktivoiStaattinenPallo(StaattinenObjecti objekti)
    {
        objekti.muutaNakyvyys();

    }
    private void LuoTestiKentta()
    {
        for (int x = 0; x < ruudukonKokoX; x++)
        {
            for (int y = 0; y < ruudukonKokoY; y++)
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

    /* Funktiot. */

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

    /// <summary>
    /// Asettaa näppäimet ja niihin liittyvät tapahtumat.
    /// </summary>
    void AsetaOhjaimet()
    {
        

        //Keyboard.Listen(Key.Up, ButtonState.Down, AsetaNopeus, "Liikuta palloa ylös", peli_pallo, nopeusYlos);
        //Keyboard.Listen(Key.Up, ButtonState.Released, AsetaNopeus, null, peli_pallo, Vector.Zero);
        //Keyboard.Listen(Key.Down, ButtonState.Down, AsetaNopeus, "Liikuta palloa alas", peli_pallo, nopeusAlas);
        //Keyboard.Listen(Key.Down, ButtonState.Released, AsetaNopeus, null, peli_pallo, Vector.Zero);

        //Keyboard.Listen(Key.Left, ButtonState.Down, AsetaNopeus, "Liikuta palloa vasemmalle", peli_pallo, nopeusVasen);
        //Keyboard.Listen(Key.Left, ButtonState.Released, AsetaNopeus, null, peli_pallo, Vector.Zero);
        //Keyboard.Listen(Key.Right, ButtonState.Down, AsetaNopeus, "Liikuta palloa oikealle", peli_pallo, nopeusOikea);
        //Keyboard.Listen(Key.Right, ButtonState.Released, AsetaNopeus, null, peli_pallo, Vector.Zero);

        PhoneBackButton.Listen(ConfirmExit, "Lopeta peli");
        Keyboard.Listen(Key.Escape, ButtonState.Pressed, ConfirmExit, "Lopeta peli");
    }

}

