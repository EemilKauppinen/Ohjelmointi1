using Jypeli;
using Jypeli.Assets;
using Jypeli.Controls;
using Jypeli.Widgets;
using System;
using System.Collections.Generic;

public class Tehtava4_5 : PhysicsGame
{
    public override void Begin()
    {
        SetWindowSize(800, 600);
        Level.Size = new Vector(800, 600);
        PhysicsObject[] pallot = TeeSatunnaisetPallot(20, 30.0);

        Keyboard.Listen(Key.Escape, ButtonState.Pressed, ConfirmExit, "Lopeta peli");

        PhysicsObject p = new PhysicsObject(30, 30, Shape.Circle);
        p.Position = new Vector(100, 100);
        p.Color = Color.Blue;
        Add(p);

        PhysicsObject lahinPallo = LahinPallo(pallot, p.Position);
        lahinPallo.Color = Color.Red;

        Mouse.Listen(
            MouseButton.Left,
            ButtonState.Pressed,
            delegate ()
            {
                Vector paikkaKentalla = Mouse.PositionOnWorld;
                p.Position = paikkaKentalla;
                foreach (var item in pallot)
                {
                    item.Color = Color.White;

                    PhysicsObject lahinPallo = LahinPallo(pallot, p.Position);
                    lahinPallo.Color = Color.Red;
                }
            },
            ""
        );
    }
    /// <summary>
    /// Funktio etsii ja palauttaa annettua pistettä lähimmän pallon, siis PhysicsObject-olion.
    /// </summary>
    /// <param name="pallot">Taulukollinen palloja</param>
    /// <param name="piste">Sinisen pallon koordinaatit</param>
    /// <returns>Lähimmän etäisyyden omaavan pallon.</returns>
    public PhysicsObject LahinPallo(PhysicsObject[] pallot, Vector piste)
    {
        // Tämänhetkinen lyhin etäisyys.
        double lyhinEtaisuus = Double.MaxValue;

        // Tämänhetkinen lyhyimän etäisyyden omaava pallo.
        PhysicsObject etaisyysPallo = null;

         foreach (var pallo in pallot)
         {

            double valiAikainenEtaisuus = piste.Distance(pallo.Position);

            if (valiAikainenEtaisuus < lyhinEtaisuus)
            {

                lyhinEtaisuus = valiAikainenEtaisuus;

                etaisyysPallo = pallo;
            }
         }

         return etaisyysPallo;     
    }

    /// <summary>
    /// Tekee palloja satunnaisiin paikkoihin
    /// </summary>
    /// <param name="montako">Montako palloa tehdään</param>
    /// <param name="koko">Pallon koko</param>
    /// <returns>Pallot</returns>
    private PhysicsObject[] TeeSatunnaisetPallot(int montako, double koko)
    {
        PhysicsObject[] pallot = new PhysicsObject[montako];
        for (int i = 0; i < montako; i++)
        {
            PhysicsObject p = new PhysicsObject(koko, koko, Shape.Circle);
            pallot[i] = p;
            p.Position = RandomGen.NextVector(Level.BoundingRect);
            Add(p);
        }
        return pallot;
    }
    
}

