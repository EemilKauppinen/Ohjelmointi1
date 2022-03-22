using Jypeli;
using Jypeli.Assets;
using Jypeli.Controls;
using Jypeli.Widgets;
using System;
using System.Collections.Generic;

public class tehtava1_2 : PhysicsGame
{
    public override void Begin()
    {
        Pallo(this, 0, 0, 100, Math.PI / 2);
    }

    /// <summary>
    /// Tehdään pallo, ja sen yläpuolelle kaksi muuta palloa
    /// rekursiivisesti.
    /// </summary>
    /// <param name="peli">peli johon pallo lisätään</param>
    /// <param name="x">Pallon kp x-koordinaatti</param>
    /// <param name="y">Pallon kp y-koordinaatti</param>
    /// <param name="sade">Pallon sade</param>
    /// <param name="suunta">Suunta radiaaneina</param>
    public static void Pallo(Game peli, double x, double y,
                             double sade, double suunta)
    {
        if (sade < 5)
        {
            return;
        }
        GameObject pallo = new GameObject(sade*2, sade*2, Shape.Circle, x, y);
        pallo.Color = RandomGen.NextColor();
        peli.Add(pallo);   
        // pallo pisteeseen (x, y)
        // jos pallon sade < PIENIN_SADE lopeta

        // määrittele uusi säde itse ...
        double r = sade * 0.5;
        // määrittele suunnat itse ...
        double vSuunta = suunta + Math.PI / 4;
        double oSuunta = suunta - Math.PI / 4;

        Pallo(peli, x + Math.Cos(vSuunta) * (sade + r),
                    y + Math.Sin(vSuunta) * (sade + r),
                    r, vSuunta);

        Pallo(peli, x + Math.Cos(oSuunta) * (sade + r),
                    y + Math.Sin(oSuunta) * (sade + r),
                    r, oSuunta);
    }

}

