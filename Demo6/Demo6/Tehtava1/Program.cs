
public class Tehtava1
{

    static public void Main()
    {
        TeeTaulukko(5);

    }
    public static int[] TeeTaulukko(int luku)
    {
        // Luo 5 paikkasen int luku taulukon.

        //  0  1  2  3  4  
        // {0, 0, 0, 0, 0}
        //
        int[] array1 = new int[luku];

        for (int i = luku; i > 0; i--)
        {
            array1[i-1] = i;
        
        }
        // {1, 0, 0, 0, 0}
        return array1;
    }
}