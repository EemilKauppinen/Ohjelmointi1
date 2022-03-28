
public class Tehtava1
{

    public static void Main(string[] args)
    {
        int[] kg = { -5, -2, 1, 2, -1, 9, -1, -3, -4, 0, 13, 4 };
        ToiseksiSuurin(kg);
    }

    public static int ToiseksiSuurin(int[] luvut)
    {
        if (luvut == null)
        {
            throw new ArgumentNullException("Ei voi olla null.");
        }

        if (luvut.Length < 2)
        {
            throw new ArgumentNullException("Pitaa olla vahintaan kaksi lukua.");
        }

        int suurin = luvut[0];

        int toisiksiSuurin = luvut[1]; 

        if (suurin < toisiksiSuurin)
        {
            int temp = suurin;
            suurin = toisiksiSuurin;
            toisiksiSuurin = temp;
        }

        for (int i = 2; i < luvut.Length; i++)
        {

            if (luvut[i] >= suurin)
            {
                toisiksiSuurin = suurin;
                suurin = luvut[i];

            }
            else if (luvut[i] >= toisiksiSuurin)
            {
                toisiksiSuurin = luvut[i];

            }


        }



        return toisiksiSuurin;
    }
}



