using Jypeli;
using Jypeli.Assets;
using Jypeli.Controls;
using Jypeli.Widgets;
using System;
using System.Collections.Generic;

/// @author  Eemil Kauppinen
/// @version 23.1.2022
///
/// <summary>
/// Demo 2 Guru 1-2 tehtävä.
/// </summary>
public class G1_2 : PhysicsGame
{
    /// <summary>
    /// Luo suorakulmion.
    /// </summary>
    /// <param name="x">x koordinaatti</param>
    /// <param name="y">y koordinaatti</param>
    /// <param name="leveys">suorakulmion leveys</param>
    /// <param name="korkeus">suorakulmion korkeus</param>
    public void PiirraSuorakulmio(double x, double y, double leveys, double korkeus)
    {

        Color c = RandomGen.NextColor();
        PhysicsObject suorakulmio = new PhysicsObject(leveys, korkeus, Shape.Rectangle);
        suorakulmio.X = x;
        suorakulmio.Y = y;
        suorakulmio.Color = c;
        Add(suorakulmio);
        
    }

    public override void Begin()
    {
        // For silmukassa luo satunnaisesti 100 suorakulmiota.
        for (int i = 0; i < 100; i++)
        {
            double korkeus = RandomGen.NextDouble(25, 50);
            double leveys = RandomGen.NextDouble(25, 50);
            double x = RandomGen.NextDouble(-500, 500);
            double y = RandomGen.NextDouble(0, 500);
            PiirraSuorakulmio(x, y, leveys, korkeus);
        }
        Level.CreateBorders();
        Gravity = new Vector(0, -30);
        PhoneBackButton.Listen(ConfirmExit, "Lopeta peli");
        Keyboard.Listen(Key.Escape, ButtonState.Pressed, ConfirmExit, "Lopeta peli");

     
    }
}

