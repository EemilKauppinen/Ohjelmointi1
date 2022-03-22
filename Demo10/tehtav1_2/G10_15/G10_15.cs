using Jypeli;
using Jypeli.Assets;
using Jypeli.Controls;
using Jypeli.Widgets;
using System;
using System.Collections.Generic;

public class G10_15 : PhysicsGame
{
    public override void Begin()
    {
        Level.Background.Image = Image.FromFile("testi.png");

        byte[] bittitaulukko = Level.Background.Image.GetByteArray();

        for (int i = 0; i < bittitaulukko.Length; i += 4)
        {
            byte r = bittitaulukko[i];
            byte g = bittitaulukko[i + 1];
            byte b = bittitaulukko[i + 2];
            byte a = bittitaulukko[i + 3];
            string binary = Convert.ToString(bittitaulukko[i],2);

            System.Console.WriteLine();
        }

        PhoneBackButton.Listen(ConfirmExit, "Lopeta peli");
        Keyboard.Listen(Key.Escape, ButtonState.Pressed, ConfirmExit, "Lopeta peli");
    }


}

