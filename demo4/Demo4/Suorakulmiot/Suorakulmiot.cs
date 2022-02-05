using System;
using System.Collections.Generic;
using Jypeli;
using Jypeli.Assets;
using Jypeli.Controls;
using Jypeli.Widgets;

/// @author  Vesa Lappalainen, Antti-Jussi Lakanen
/// @version 2022
///
/// <summary>
/// Ohjelmassa pudotellaan suorakulmioita lattialle.
/// Harjoitellaan fysiikan käyttöä, toistolausetta (for),
/// sekä aliohjelmien luomista.
/// </summary>
public class Suorakulmiot : PhysicsGame
{
    public override void Begin()
    {
        Level.Size = new Vector(1300, 700); // asettaa pelialueen koon
        SetWindowSize(1300, 700); // asettaa ikkunan koon
        Level.CreateBorders(); // luodaan kentälle reunat
        Camera.ZoomToLevel(); // zoomaa kameran siten että vain pelialue on näkyvillä, eikä muuta

        Level.Background.Color = Color.Black;
        //Gravity = new Vector(0, -1000);

        for (int i = 0; i < 30; i++)
        {
            double sade = RandomGen.NextDouble(5, 30);
            PiirraSuorakulmio(
                this,
                RandomGen.NextDouble(Level.Left, Level.Right), // Kentän vasen ja oikea reuna
                RandomGen.NextDouble(Level.Bottom, Level.Top), // alareuna, yläreuna
                sade, // leveys
                sade // korkeus
                );
        }
        PiirraSuorakulmio(this, 0, 0, 40, 40, Color.White);
        PhoneBackButton.Listen(ConfirmExit, "Lopeta peli");
        Keyboard.Listen(Key.Escape, ButtonState.Pressed, ConfirmExit, "Lopeta peli");
    }

    /// <summary>
    /// Luodaan ja lisätään ruudulle annetun kokoinen
    /// suorakulmio annettuun paikkaan.
    /// </summary>
    /// <param name="peli">Peli, johon neliö piirretään</param>
    /// <param name="x">Kappaleen keskipisteen x-koordinaatti</param>
    /// <param name="y">Kappaleen keskipisteen y-koordinaatti</param>
    /// <param name="w">Kappaleen leveys.</param>
    /// <param name="h">Kappaleen korkeus.</param>
    public static void PiirraSuorakulmio(Game peli, double x, double y, double w, double h)
    {
        PhysicsObject kappale = new PhysicsObject(w, h, Shape.Circle);
        kappale.Position = new Vector(x, y);
        kappale.Color = RandomGen.NextColor();
        peli.Add(kappale);
    }

    public static void PiirraSuorakulmio(Game peli, double x, double y, double w, double h, Color color)
    {
        PhysicsObject kappale = new PhysicsObject(w, h, Shape.Circle);
        kappale.Position = new Vector(x, y);
        kappale.Color = color;
        peli.Add(kappale);
    }
}
