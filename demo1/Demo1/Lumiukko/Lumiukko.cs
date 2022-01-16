using Jypeli;
using Jypeli.Assets;
using Jypeli.Controls;
using Jypeli.Widgets;
using System;
using System.Collections.Generic;

public class Lumiukko : PhysicsGame
{
    public override void Begin()
    {
        // Kirjoita ohjelmakoodisi tähän

        PhoneBackButton.Listen(ConfirmExit, "Lopeta peli");
        Keyboard.Listen(Key.Escape, ButtonState.Pressed, ConfirmExit, "Lopeta peli");

        Camera.ZoomToLevel(); // tai Camera.ZoomToAllObjects();
        Level.Background.Color = Color.Black;

        /*
         * Luodaan lumiukko joka on kompastumassa.
         */

        GameObject alapallo = new GameObject(2 * 115, 2 * 115, Shape.Circle);
        alapallo.X = 60;
        alapallo.Y = Level.Bottom + 300;
        Add(alapallo);

        GameObject vartalo = new GameObject(2 * 75, 2 * 75, Shape.Circle);
        vartalo.X = 0;
        vartalo.Y = alapallo.Y + 100 + 50;
        Add(vartalo);

        GameObject paa = new GameObject(2 * 55, 2 * 55, Shape.Circle);
        paa.X = 0;
        paa.Y = vartalo.Y + 70 + 30;
        Add(paa);

        GameObject hattu = new GameObject(2 * 65, 2 * 10, Shape.Rectangle);
        hattu.Color = Color.Green;
        hattu.X = 0;
        hattu.Y = paa.Y + 55;
        Add(hattu);

        GameObject hattu2 = new GameObject(2 * 40, 2 * 40, Shape.Rectangle);
        hattu2.Color = Color.Green;
        hattu2.X = 0;
        hattu2.Y = paa.Y + 90;
        Add(hattu2);

        GameObject silma = new GameObject(2 * 5, 2 * 5, Shape.Circle);
        silma.Color = Color.Black;
        silma.X = 25;
        silma.Y = paa.Y + 10;
        Add(silma);

        GameObject silma2 = new GameObject(2 * 5, 2 * 5, Shape.Circle);
        silma2.Color = Color.Black;
        silma2.X = -25;
        silma2.Y = paa.Y + 10;
        Add(silma2);

        GameObject nena = new GameObject(2 * 5, 2 * 5, Shape.Circle);
        nena.Color = Color.Orange;
        nena.X = 0;
        nena.Y = paa.Y - 5;
        Add(nena);

        // Luo napit.
        for (int i=0; i < 6; i++)
        {
            GameObject nappi = new GameObject(2 * 5, 2 * 5, Shape.Circle);
            nappi.Color = Color.Black;
            nappi.X = vartalo.X + i*15;
            nappi.Y = vartalo.Y - i*40 + 35;
            Add(nappi);
        }


        GameObject kasi = new GameObject(2 * 5, 2 * 60, Shape.Rectangle);
        kasi.Color = Color.Brown;
        kasi.X = vartalo.X + 90;
        kasi.Y = vartalo.Y - 35;
        kasi.Angle = Jypeli.Angle.FromDegrees(44.0);
        Add(kasi);

        GameObject kasi2 = new GameObject(2 * 5, 2 * 60, Shape.Rectangle);
        kasi2.Color = Color.Brown;
        kasi2.X = vartalo.X - 90;
        kasi2.Y = vartalo.Y - 35;
        kasi2.Angle = Jypeli.Angle.FromDegrees(-44.0);
        Add(kasi2);




    }
}

