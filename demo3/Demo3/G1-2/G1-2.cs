using Jypeli;
using Jypeli.Assets;
using Jypeli.Controls;
using Jypeli.Widgets;
using System;
using System.Collections.Generic;

public class G1_2 : PhysicsGame
{
/// <summary>
/// Ohjelma tekee laatikoita, jotka ovat toistensa yläviistoon ja myös pienenevät
/// </summary>
    public override void Begin()
    {
        PhoneBackButton.Listen(ConfirmExit, "Lopeta peli");
        Keyboard.Listen(Key.Escape, ButtonState.Pressed, ConfirmExit, "Lopeta peli");
        Camera.ZoomToLevel(); 

        int n = 10;
        double laatikonKoko = 80;

        double koordinaattiNytten = 0; 

        for (int i = 0; i < n; i++)
        {
            double uudenLaatikonKoko = (double)(n + 1 - i) / n * laatikonKoko;
            PiirraNelio(this, koordinaattiNytten, koordinaattiNytten, uudenLaatikonKoko);
            
            double seuraavanLaatikonKoko = (double)(n + 1 - (i + 1)) / n * laatikonKoko;
            koordinaattiNytten += uudenLaatikonKoko - (uudenLaatikonKoko - seuraavanLaatikonKoko)/2.0;
        }
    }

    /// <summary>
    /// Aliohjelma piirtää ruutuun yhden neliön, jonka
    /// keskipiste on (x, y).
    /// </summary>
    /// <param name="peli">Peli, johon neliö piirretään</param>
    /// <param name="x">Neliön keskipisteen x-koordinaatti.</param>
    /// <param name="y">Neliön keskipisteen y-koordinaatti.</param>
    /// <param name="koko">Neliön koko.</param>
    public static void PiirraNelio(PhysicsGame peli,
                                   double x, double y, double koko)
    {
        GameObject suorakulmio = new GameObject(koko, koko, Shape.Rectangle);
        suorakulmio.X = x;
        suorakulmio.Y = y;
        peli.Add(suorakulmio);
    }
}