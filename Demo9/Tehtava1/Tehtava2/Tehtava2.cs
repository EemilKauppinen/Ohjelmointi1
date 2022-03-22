using Jypeli;
using Jypeli.Assets;
using Jypeli.Controls;
using Jypeli.Widgets;
using System;
using System.Collections.Generic;

public class Tehtava2 : PhysicsGame
{
    public override void Begin()
    {
        Vector piste = new Vector(0, 0);
        piste = PiirraNelio(piste);
        piste = PiirraNelio(piste);
        piste = PiirraNelio(piste);

        //TODO: Lisää punainen pallo pisteeseen (0, 0).
        LuoPallo(0, 0, 5);

        Camera.ZoomToAllObjects(100);
    }

    public void LuoPallo(int x, int y, int sade)
    {
        GameObject pallo = new GameObject(sade, sade, Shape.Circle, x, y);
        pallo.Color = Color.Red;
        Add(pallo, 1);
    }

    /// <summary>
    /// Aliohjelma piirtää ruutuun yhden neliön, jonka
    /// sivun pituus on 80 ja vasemman alakulman koordinaatti p.
    /// </summary>
    /// <param name="p">Neliön vasemman alanurkan koordinaatti.</param>
    /// <returns>Neliön oikean ylänurkan koordinaatti</returns>
    public Vector PiirraNelio(Vector p)
    {
        GameObject nelio = new GameObject(80, 80, Shape.Rectangle);
        nelio.Position = p + new Vector(40, 40);
        Add(nelio);

        double uudenNelionKeskiPisteX = p.X + 80;
        double uudenNelionKeskiPisteY = p.Y + 80;
        return new Vector (uudenNelionKeskiPisteX, uudenNelionKeskiPisteY);
    }

}

