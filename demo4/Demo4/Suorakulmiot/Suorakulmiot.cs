using System;
using System.Collections.Generic;
using Jypeli;
using Jypeli.Assets;
using Jypeli.Controls;
using Jypeli.Widgets;

/// @author  Eemil Kauppinen
/// @version 2022
///
/// <summary>
/// Pelin tarkoituksena on tuhota kaikki pallot.
/// Kun peli-pallo osuu muihin palloihin kentän yläpuolella pallo räjähtää.
/// 
/// </summary>
public class Suorakulmiot : PhysicsGame
{
    int pisteet = 30;
    PhysicsObject peli_pallo;
    Vector nopeusYlos = new Vector(0, 200);
    Vector nopeusAlas = new Vector(0, -200);
    Vector nopeusVasen = new Vector(-200,0);
    Vector nopeusOikea = new Vector(200,0);

    public override void Begin()
    {
        Level.Size = new Vector(1300, 700); // asettaa pelialueen koon
        SetWindowSize(1300, 700); // asettaa ikkunan koon
        Level.CreateBorders(); // luodaan kentälle reunat
        Camera.ZoomToLevel(); // zoomaa kameran siten että vain pelialue on näkyvillä, eikä muuta

        Level.Background.Color = Color.Black;

        for (int i = 0; i < pisteet; i++)
        {
            double sade = RandomGen.NextDouble(5, 30);
            LuoPallo(
                //this,
                RandomGen.NextDouble(Level.Left, Level.Right), // Kentän vasen ja oikea reuna
                RandomGen.NextDouble(Level.Bottom, Level.Top), // alareuna, yläreuna
                sade, // leveys
                sade // korkeus
                );

        }
        LuoPeliPallo();
        PhoneBackButton.Listen(ConfirmExit, "Lopeta peli");
        //Keyboard.Listen(Key.Escape, ButtonState.Pressed, ConfirmExit, "Lopeta peli");
        AsetaOhjaimet();
    }
   /// <summary>
   /// Tapahtuma metodi sille kun pallo osuu peli-palloon.
   /// </summary>
   /// <param name="pallo">Pallo johon tämä tapahtuma metodi on rekisteröity</param>
   /// <param name="kohde">Objekti johon pallo törmää</param>
    void KasittelePallonTormays(PhysicsObject pallo, PhysicsObject kohde)
    {
        // Pallo törmää peli-pallon kanssa.
        if (kohde == peli_pallo)
        {
            // Jos pallo on törmäys hetkellä kentän puolivälin yläpuolella.
            if (pallo.Y > 0.0)
            {
                // Poistetaan tämä pallo ja vähennetään pisteitä yhdellä.
                Remove(pallo);
                pisteet = pisteet-1;
                
                // Jos pisteet on nolla jypeli tulostaa voitto
                if (pisteet == 0)
                {
                    MessageDisplay.Add("Voitto");
                }
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
    public void LuoPallo(double x, double y, double w, double h)
    {
        PhysicsObject kappale = new PhysicsObject(w, h, Shape.Circle);
        kappale.Position = new Vector(x, y);
        kappale.Color = RandomGen.NextColor();
        Vector impulssi = new Vector(RandomGen.NextDouble(-20, 20), RandomGen.NextDouble(-20, 20)) ;
        kappale.Hit(impulssi * kappale.Mass);
        AddCollisionHandler(kappale, KasittelePallonTormays);
        this.Add(kappale);
    }
    /// <summary>
    /// Luo liikutettava peli-pallo
    /// </summary>
    public void LuoPeliPallo()
    {
        peli_pallo = new PhysicsObject(40, 40, Shape.Circle);
        peli_pallo.Position = new Vector(0, 0);
        peli_pallo.Color = Color.White;
        Add(peli_pallo);
    }

    /// <summary>
    /// Asettaa liikutettavalle peli-pallolle nopeuden.
    /// </summary>
    /// <param name="peli_pallo">Liikutettava pallo</param>
    /// <param name="nopeus">pallolle annettava nopeus vektori</param>
    void AsetaNopeus(PhysicsObject peli_pallo, Vector nopeus)
    {
        if ((nopeus.Y < 0) && (peli_pallo.Bottom < Level.Bottom))
        {
            peli_pallo.Velocity = Vector.Zero;
            return;
        }
        if ((nopeus.Y > 0) && (peli_pallo.Top > Level.Top))
        {
            peli_pallo.Velocity = Vector.Zero;
            return;
        }

        peli_pallo.Velocity = nopeus;
    }

    /// <summary>
    /// Asettaa näppäimet ja niihin liittyvät tapahtumat.
    /// </summary>
    void AsetaOhjaimet()
    {
        Keyboard.Listen(Key.Up, ButtonState.Down, AsetaNopeus, "Liikuta palloa ylös", peli_pallo, nopeusYlos);
        Keyboard.Listen(Key.Up, ButtonState.Released, AsetaNopeus, null, peli_pallo, Vector.Zero);
        Keyboard.Listen(Key.Down, ButtonState.Down, AsetaNopeus, "Liikuta palloa alas", peli_pallo, nopeusAlas);
        Keyboard.Listen(Key.Down, ButtonState.Released, AsetaNopeus, null, peli_pallo, Vector.Zero);

        Keyboard.Listen(Key.Left, ButtonState.Down, AsetaNopeus, "Liikuta palloa vasemmalle", peli_pallo, nopeusVasen);
        Keyboard.Listen(Key.Left, ButtonState.Released, AsetaNopeus, null, peli_pallo, Vector.Zero);
        Keyboard.Listen(Key.Right, ButtonState.Down, AsetaNopeus, "Liikuta palloa oikealle", peli_pallo, nopeusOikea);
        Keyboard.Listen(Key.Right, ButtonState.Released, AsetaNopeus, null, peli_pallo, Vector.Zero); 

        Keyboard.Listen(Key.Escape, ButtonState.Pressed, ConfirmExit, "Lopeta peli");
    }
}
