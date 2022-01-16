/// @author  Eemil Kauppinen
/// @version 15.1.2022
///
/// <summary>
/// C#-ohjelma, joka tulostaa kaikki ne kokonaisluvut jotka ovat pienempiä (tai yhtäsuuria) kuin 1000 ja joiden neliöjuuri on ko­konaisluku.
/// </summary>
public class Neliojuuret
{
    /// <summary>
    /// Pääohjelma.
    /// </summary>
    public static void Main()
    {
        /*
         * Kokonaisluku kerrottana itsellään ja josta otetaan neliöjuuri on kokonaisluku.
         *
            i   i*i
            0   0
            1 	1
            2 	4 	
            3 	9 	
            4 	16 	
            5 	25 	
            6 	36 	
            7 	49 	
            8 	64 	
            9 	81 	
            10 	100 
            11 	121 
            12 	144 
            13 	169 
            14 	196
            15 	225
            16 	256
            17 	289
            18 	324
            19 	361
            20 	400
            21 	441
            22 	484
            23 	529
            24 	576
            25 	625
            26 	676
            27 	729
            28 	784
            29 	841
            30 	900
            31 	961
        */

        // Alustetaan ensimmäinen luku nollaksi.
        int i = 0;

        // Luvut käydään luupissa läpi ja tulostaa ne kokonaisluvut, jotka ovat kerrottuna itsellään alle tuhat.
        // Kokonaisluku kerrottuna itsellään ja ottamalla neliöjuuri on kokonaisluku.
        while (i*i <= 1000)
        {
            System.Console.WriteLine(i*i);

            // Kasvatetaan i:n arvoa yhdellä.
            i++;
        }
    }
}
