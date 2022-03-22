
public class TulostaSummat
{

    public static void Main()
    {
        int[] luvut = new int[] { 2, 3, 9, -5 };

        for (int i = 0; i < luvut.Length; i++)
        {
            

            for (int j = 0; j < luvut.Length; j++)
            {

                if (i != j)
                {

                    System.Console.WriteLine("{0} ja {1}, summa on: {2}", luvut[i],luvut[j] , luvut[i] + luvut[j]);


                }

            }
        }
    }

}