using Jypeli;
using Jypeli.Assets;
using Jypeli.Controls;
using Jypeli.Widgets;
using System;
using System.Collections.Generic;
using System.Linq;

public class Harjoitustyo : PhysicsGame
{
    static int ruudukonKokoX = 8;
    static int ruudukonKokoY = 8;
    static int kentanLeveys = 1000;
    static int kentanKorkeus = 1000;
    List<PhysicsObject> kentanTekoLista = new List<PhysicsObject>(ruudukonKokoX * ruudukonKokoY);
    public override void Begin()
    {


        //Level.CreateBorders();

        SetWindowSize(kentanLeveys, kentanKorkeus); // asettaa ikkunan koon
        Camera.ZoomToLevel();

        //LuoPallo(0, 0, 50, 50);
        //LuoNelikulmio(50, 200, 50,100);

        PhoneBackButton.Listen(ConfirmExit, "Lopeta peli");
        Keyboard.Listen(Key.Escape, ButtonState.Pressed, ConfirmExit, "Lopeta peli");
        // LuoNelikulmio(0.0, 0, 350, 350);

        //for (int i = 0; i<10; i++)
        //{

        //    GameObject kappale = new GameObject(10*(10-i), 10*(10-i), Shape.Rectangle, i*5.0, i*5.0);
            

        //    kappale.Color = RandomGen.NextColor();
            
        //    Add(kappale);
        //}

        GameObject kappale = new GameObject(kentanLeveys, kentanKorkeus , Shape.Rectangle, 0.0, 0.0);

        kappale.Color = Color.Red;

        // Vector impulssi = new Vector(RandomGen.NextDouble(-20, 20), RandomGen.NextDouble(-20, 20));
        //kappale.Hit(impulssi * kappale.Mass);
        // AddCollisionHandler(kappale,KasittelePallonTormays);
        Add(kappale);

        LuoTestiKentta();

        LuoLiikkuvaPallo(30, 30, 30, 30, Color.Black);

        LuoLiikkuvaPallo(0, 0, 30, 30, Color.Black);
        LuoLiikkuvaPallo(-130, 50, 30, 30, Color.Black);
        LuoLiikkuvaPallo(-200, -200, 30, 30, Color.Black);

    }

    private void SijoitaAsiaKenttaan(int x, int y, Shape s, Color c)
    {
        int indeksi = anna_indeksi(x, y);
        // double r = (double)kentanLeveys / (double)ruudukonKokoX * 0.5;
        double r = (double)kentanLeveys / (double)ruudukonKokoX;
        double xKoordinaatti = 0.5*r + (double)x * r - kentanLeveys * 0.5;
        double yKoordinaatti = 0.5*r + (double)y * r - kentanKorkeus * 0.5;

        if (s == Shape.Rectangle)
        {
            LuoNelikulmio(xKoordinaatti, yKoordinaatti, r, r, c);
        }
        else
        {
            LuoPallo(xKoordinaatti, yKoordinaatti, r, r, c);
        }
    }

    private void LuoTestiKentta()
    {

        SijoitaAsiaKenttaan(0, 0, Shape.Circle, Color.Blue);
        SijoitaAsiaKenttaan(1, 0, Shape.Rectangle, Color.Green);
        SijoitaAsiaKenttaan(2, 0, Shape.Rectangle, Color.Black);
        SijoitaAsiaKenttaan(3, 0, Shape.Circle, Color.Blue);
        SijoitaAsiaKenttaan(4, 0, Shape.Rectangle, Color.Green);
        SijoitaAsiaKenttaan(5, 0, Shape.Rectangle, Color.Black);
        SijoitaAsiaKenttaan(6, 0, Shape.Circle, Color.Blue);
        SijoitaAsiaKenttaan(7, 0, Shape.Rectangle, Color.Green);

        SijoitaAsiaKenttaan(0, 1, Shape.Circle, Color.Blue);
        //SijoitaAsiaKenttaan(1, 1, Shape.Rectangle, Color.Green);
        //SijoitaAsiaKenttaan(2, 1, Shape.Rectangle, Color.Black);
        //SijoitaAsiaKenttaan(3, 1, Shape.Circle, Color.Blue);
        SijoitaAsiaKenttaan(4, 1, Shape.Rectangle, Color.Green);
        //SijoitaAsiaKenttaan(5, 1, Shape.Rectangle, Color.Black);
        SijoitaAsiaKenttaan(6, 1, Shape.Circle, Color.Blue);
        SijoitaAsiaKenttaan(7, 1, Shape.Rectangle, Color.Green);

        SijoitaAsiaKenttaan(0, 2, Shape.Circle, Color.Blue);
        SijoitaAsiaKenttaan(1, 2, Shape.Rectangle, Color.Green);
        SijoitaAsiaKenttaan(2, 2, Shape.Rectangle, Color.Black);
        //SijoitaAsiaKenttaan(3, 2, Shape.Circle, Color.Blue);
        SijoitaAsiaKenttaan(4, 2, Shape.Rectangle, Color.Green);
        //SijoitaAsiaKenttaan(5, 2, Shape.Rectangle, Color.Black);
        SijoitaAsiaKenttaan(6, 2, Shape.Circle, Color.Blue);
        SijoitaAsiaKenttaan(7, 2, Shape.Rectangle, Color.Green);

        SijoitaAsiaKenttaan(0, 3, Shape.Circle, Color.Blue);
        SijoitaAsiaKenttaan(1, 3, Shape.Rectangle, Color.Green);
        SijoitaAsiaKenttaan(2, 3, Shape.Rectangle, Color.Black);
        //SijoitaAsiaKenttaan(3, 3, Shape.Circle, Color.Blue);
        SijoitaAsiaKenttaan(4, 3, Shape.Rectangle, Color.Green);
        //SijoitaAsiaKenttaan(5, 3, Shape.Rectangle, Color.Black);
        SijoitaAsiaKenttaan(6, 3, Shape.Circle, Color.Blue);
        SijoitaAsiaKenttaan(7, 3, Shape.Rectangle, Color.Green);

        SijoitaAsiaKenttaan(0, 4, Shape.Circle, Color.Blue);
        SijoitaAsiaKenttaan(1, 4, Shape.Rectangle, Color.Green);
        //SijoitaAsiaKenttaan(2, 4, Shape.Rectangle, Color.Black);
        //SijoitaAsiaKenttaan(3, 4, Shape.Circle, Color.Blue);
        //SijoitaAsiaKenttaan(4, 4, Shape.Rectangle, Color.Green);
        //SijoitaAsiaKenttaan(5, 4, Shape.Rectangle, Color.Black);
        SijoitaAsiaKenttaan(6, 4, Shape.Circle, Color.Blue);
        SijoitaAsiaKenttaan(7, 4, Shape.Rectangle, Color.Green);

        SijoitaAsiaKenttaan(0, 5, Shape.Circle, Color.Blue);
        //SijoitaAsiaKenttaan(1, 5, Shape.Rectangle, Color.Green);
        //SijoitaAsiaKenttaan(2, 5, Shape.Rectangle, Color.Black);
        SijoitaAsiaKenttaan(3, 5, Shape.Circle, Color.Blue);
        SijoitaAsiaKenttaan(4, 5, Shape.Rectangle, Color.Green);
        SijoitaAsiaKenttaan(5, 5, Shape.Rectangle, Color.Black);
        SijoitaAsiaKenttaan(6, 5, Shape.Circle, Color.Blue);
        SijoitaAsiaKenttaan(7, 5, Shape.Rectangle, Color.Green);

        SijoitaAsiaKenttaan(0, 6, Shape.Circle, Color.Blue);
        SijoitaAsiaKenttaan(1, 6, Shape.Rectangle, Color.Green);
        SijoitaAsiaKenttaan(2, 6, Shape.Rectangle, Color.Black);
        SijoitaAsiaKenttaan(3, 6, Shape.Circle, Color.Blue);
        SijoitaAsiaKenttaan(4, 6, Shape.Rectangle, Color.Green);
        SijoitaAsiaKenttaan(5, 6, Shape.Rectangle, Color.Black);
        SijoitaAsiaKenttaan(6, 6, Shape.Circle, Color.Blue);
        SijoitaAsiaKenttaan(7, 6, Shape.Rectangle, Color.Green);

        SijoitaAsiaKenttaan(0, 7, Shape.Circle, Color.Blue);
        SijoitaAsiaKenttaan(1, 7, Shape.Rectangle, Color.Green);
        SijoitaAsiaKenttaan(2, 7, Shape.Rectangle, Color.Black);
        SijoitaAsiaKenttaan(3, 7, Shape.Circle, Color.Blue);
        SijoitaAsiaKenttaan(4, 7, Shape.Rectangle, Color.Green);
        SijoitaAsiaKenttaan(5, 7, Shape.Rectangle, Color.Black);
        SijoitaAsiaKenttaan(6, 7, Shape.Circle, Color.Blue);
        SijoitaAsiaKenttaan(7, 7, Shape.Rectangle, Color.Green);


        //this.kentanTekoLista = new PhysicsObject[]
        //{
        //    // LuoNelikulmio()

        //}.ToList();
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

}

