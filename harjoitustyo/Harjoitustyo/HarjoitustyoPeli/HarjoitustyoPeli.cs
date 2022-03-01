using Jypeli;
using Jypeli.Assets;
using Jypeli.Controls;
using Jypeli.Widgets;
using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.IO;

public class HarjoitustyoPeli : PhysicsGame
{

    static int ruudukonKokoX = 128;
    static int ruudukonKokoY = 64;
    static int kentanLeveys = 1000;
    static int kentanKorkeus = 500;
    List<PhysicsObject> kentanTekoLista = new List<PhysicsObject>();

    List<PhysicsObject> viholliset = new List<PhysicsObject>();
    
    public override void Begin()
    {
        Kentta ekaKentta = new Kentta("ekaKentta.png", "kentta1.txt");

        LoadMapData("kentta1.txt");
        Level.Background.Image = Image.FromFile(ekaKentta.AnnaTaustaKuvanNimi());
        SetWindowSize(kentanLeveys, kentanKorkeus);
        // Camera.ZoomToLevel();

        PhoneBackButton.Listen(ConfirmExit, "Lopeta peli");
        Keyboard.Listen(Key.Escape, ButtonState.Pressed, ConfirmExit, "Lopeta peli");

        //MuutaKentanReunatNakyvyys(true);
        for (int i = 0; i < 80; i++)
        {
            LuoPallo(RandomGen.NextDouble(-900, -890), RandomGen.NextDouble(-490, -480), 10.0, 10.0, Color.Red);
        }
    }

    public void LuoPallo(double x, double y, double w, double h, Color c)
    {
        PhysicsObject kappale = new PhysicsObject(w, h, Shape.Circle);
        kappale.Position = new Vector(x, y);
        kappale.Color = c;
        // kappale.MakeStatic();
        Vector impulssi = new Vector(RandomGen.NextDouble(0, 200), RandomGen.NextDouble(0, 200));
        //Vector impulssi = new Vector(20, 500);
        kappale.Hit(impulssi * kappale.Mass);
        kappale.Restitution = 2.1;
        // kappale.Acceleration = kappale.Velocity * 0.5;
        // AddCollisionHandler(kappale,KasittelePallonTormays);
        viholliset.Add(kappale);
        Add(kappale);
    }
    //public void MuutaKentanReunatNakyvyys(bool visibility)
    //{
    //    foreach(var kappale in kentanTekoLista)
    //    {
    //        kappale.IsVisible = visibility;
    //    }
    //}
    public void LuoTormausPallo(double width, double height, double x, double y, Shape s, Color c)
    {
        //GameObject pallo = new GameObject(width, height, x, y);
        PhysicsObject pallo = new PhysicsObject(width, height, x, y);
        pallo.Shape = s;
        pallo.IsVisible = false;
        kentanTekoLista.Add(pallo);
        pallo.MakeStatic();
        Add(pallo);
    }
    public void LoadMapData(string kentta)
    {
        
        
            string text = System.IO.File.ReadAllText(kentta);
        //System.Console.WriteLine(text);

        //  
        // "iiiiiiviviviiiiiviviviviviiv..."
        // 
        int laskuri = 0;
            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] == 'v')
                {
                  // if (laskuri == 230) break;
                  laskuri++;
                  Vector pallonSijainti = anna_vektori(i, ruudukonKokoX);

                  // säde
                  double r = (double)kentanLeveys * 2.0 / (double)ruudukonKokoX;
                  System.Console.WriteLine("laskuri == {0}. i == {1}. text.Length == {2}", laskuri, i, text.Length);
                  LuoTormausPallo(r, r, pallonSijainti.X, pallonSijainti.Y, Shape.Circle, Color.White);
                }

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

