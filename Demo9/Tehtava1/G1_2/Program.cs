public class G1_2
{

    public static void Main(string[] args)
    {
        int[] snvjklb = ToistotonTaulukko(new int[] { 1, 2, 3, 34, 34, 2, 1, 34, 1, 1, 1});

        for (int i = 0; i < snvjklb.Length; i++)
        {
            System.Console.Write(snvjklb[i]);
        }
    
    }
    public static int[] ToistotonTaulukko(int[] alkuperainenTaulukko)
    {
        Dictionary<int, int> temp = new Dictionary<int, int>();

        for (int i = 0; i < alkuperainenTaulukko.Length; i++)
        {
            bool onko_avainta = temp.ContainsKey(alkuperainenTaulukko[i]);
            
            if (onko_avainta)
            {
                temp[alkuperainenTaulukko[i]] += 1;
            }
            else
            {
                temp.Add(alkuperainenTaulukko[i], 1);
            }
        }

        var lajiteltu = from avain_arvo_pari in temp
                        orderby avain_arvo_pari.Value
                        ascending select avain_arvo_pari;

        List<int> list = new List<int>();

        foreach (var avain_arvo_pari in lajiteltu)
        {

            list.Add(avain_arvo_pari.Key);
        }


        return list.ToArray();

    }

}