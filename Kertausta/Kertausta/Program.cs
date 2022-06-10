using System.Text; // String builder.
using System.Linq;
public class Kertausta
{

    /// <summary>
    /// Luokkakohtainen muuttuja. static == muuttumaton.
    /// </summary>
    public static double mun_oma_pii = 3.14;

    public static void Matikkaa(double d)
    {
        double itseisarvo = Math.Abs(d);

        // Kahden luvun a ja b välinen etäisyys.
        // double itseisarvo = Math.Abs(a - b);

        double potenssiin2 = Math.Pow(d, 2.0);
        double pii = Math.PI;

        // Liukulukujen vertailu on vaarallista.
        double a = 0.0;

        for (int i = 0; i < 1000; i++)
        {
            a = a + 0.1;
        }

        if (Math.Abs(a - 100.0) < 0.001)
        {
            System.Console.WriteLine("Numero {0} On riittävän lähellä numeroa 100.", a);
        }
        else
        {
            System.Console.WriteLine("Numero {0} ei ole riittävän lähellä numeroa 100.", a);
        }
    }

    public static string JokatoinenPieneksiJokatoinenSuureksi(string s)
    {
        StringBuilder sb = new StringBuilder();

        for (int i = 0; i < s.Length; i++)
        {
            if (i % 2 == 0)
            {
                sb.Append(Char.ToLower(s[i]));
            }
            else
            {
                sb.Append(Char.ToUpper(s[i]));
            }
        }
        return sb.ToString();
    }

    public static int Muuntelua(double d)
    {
        // Muuttaa doublen intiksi.
        return (int)d;
    }

    public static void Iffittelya(int luku)
    {
        if (luku == 0 || luku > 5)
        {
            System.Console.WriteLine("Luku {0} on joko 0 TAI > 5", luku);
        }

        if (luku < 100 && (luku % 2 == 0))
        {
            System.Console.WriteLine("Luku {0} on < 100 JA se on parilinen.", luku);
        }

        if (luku < 100 && (luku % 2 != 0))
        {
            System.Console.WriteLine("Luku {0} on < 100 JA se on pariton.", luku);
        }

        if (luku == 555 || (luku > 3 && luku < 8))
        {
            System.Console.WriteLine("Luku {0} on 555 TAI luku on välillä 3 < x < 8.", luku);
        }

        // ! operaattori muuttaa truen falseksi, falsen trueksi. !false == true, !true == false
        if (!(luku >= 0))
        {

        }
    }

    public static string BuilderJuttuja()
    {
        Random rand = new Random();
        string[] verbit = new string[] { "ui", "tappelee", "vetää hirsiä", "luikkii pakoon", "syö" };
        string[] substantiivit = new string[] { "Eki", "Paavo", "Lissu" };
        string[] adjektiivit = new string[] { "punainen", "hiljainen", "possumainen", "hajamielinen" };

        StringBuilder sb = new StringBuilder();

        sb.Append(adjektiivit[rand.Next(0, adjektiivit.Length)]);
        sb.Append(' ');
        sb.Append(substantiivit[rand.Next(0, substantiivit.Length)]);
        sb.Append(' ');
        sb.Append(verbit[rand.Next(0, verbit.Length)]);
        sb.Append('.');

        return sb.ToString();
    }

    public static string[] Splittailua(string s)
    {


        // string[] splitattu = s.Split(new char[] { '.', ',' }, s);

        string[] splitattuPistePilkku = s.Split(new char[] { '.', ',' });
        string[] splitattuValiluonti = s.Split(' ');

        // "     erkki     ".Trim() => "erkki"
        System.Console.WriteLine("splitattuPistePilkku\n");
        for (int i=0; i<splitattuPistePilkku.Length; i++) { System.Console.WriteLine("{0} {1}", i, splitattuPistePilkku[i].Trim()); }
        System.Console.WriteLine("\nsplitattuValiluonti\n");
        for (int i=0; i<splitattuValiluonti.Length; i++) { System.Console.WriteLine("{0} {1}", i, splitattuValiluonti[i].Trim()); }

        return null;
    }

    public static int Parsettelua(string s)
    {
        // Yritetään lukea inttiä merkkijonosta. Jos ei pysty parsimaan, heittää poikkeuksen.
        //int parsittuInt = int.Parse(s);

        int tryParsittuInt;

        // Yrittää parsia inttiä tryParseInt muuttujaan. Palauttaa true, jos onnistui, muuten false.
        bool onnistuikoParsitus = int.TryParse(s, out tryParsittuInt);

        if (onnistuikoParsitus)
        {
            System.Console.WriteLine("Parsitun onnistui! Luku on == {0}", tryParsittuInt);
        }
        else
        {
            System.Console.WriteLine("Parsitus epäonnistui. Stringiä {0} ei voitu parstia intiksi", s);
        }

        return tryParsittuInt;
    }


    public static void OlioViitteenaSisaan(List<string> lista_stringeja)
    {
        // Käpälöidään listaa viitten kautta.
        lista_stringeja.Clear(); 
    } // lista ei kuole tässä.

    public static void InttiSisaan(int luku)
    {
        // Huomaa, että tämä on eri luku kuin mainissa oleva luku.
        //
        //  Mainin luku |100|
        //  Tämän funktion luku |100|
        //
        luku = 5455;
    } // luku kuolee tässä.

    public static string[] ListaKikkailua(List<string> lista_stringeja)
    {
        // Tehdään jotain jokaiselle listan stringillä.
        foreach (string s in lista_stringeja)
        {
            System.Console.WriteLine("{0} ja sen pituus on {1}", s, s.Length);
        }

        // Etsitään listasta jotain.
        System.Console.WriteLine("Erkki löytyy listasta == {0}", lista_stringeja.Contains("Erkki"));

        // Lisätään Jussi listaan indeksiin 1. Pidä huoli että indeksi on rajoissa, muuten räjähtää.
        lista_stringeja.Insert(1, "Jussi");

        // Korvataan "Erkki" "Eki" merkkijonolla.
        lista_stringeja[0] = "Eki";

        // Tulostetaan listan sisältö päin vastaisessa järjestyksessä.
        for (int i = lista_stringeja.Count - 1; i >= 0; i--)
        {
            System.Console.WriteLine(lista_stringeja[i]);
        }

        // Etsii ne merkkijonot joiden pituus on 4 ja luo niistä uuden listan.
        List<string> lista4 = lista_stringeja.FindAll(s => s.Length == 4);

        System.Console.WriteLine("Tulostetaan lista4");
        int j = 0;
        while (j < lista4.Count)
        {
            System.Console.WriteLine(lista4[j]);
            j++;
        }

        // Muuttaa jokaisen stringin pituudeksi ja luo näistä uuden listan.
        List<int> stringien_pituudet = lista_stringeja.Select(s => s.Length).ToList();

        System.Console.WriteLine("Stringien pituudet.");
        foreach (var pituus in stringien_pituudet)
        {
            System.Console.WriteLine(pituus);
        }

        return lista_stringeja.ToArray();
    }

    public static int Rekursio(int i)
    {
        // Lopetus ehto. Ilman sitä rekursio ei lopu koskaan.
        if (i < 10)
        {
            return i;
        }
        return i + Rekursio(i - 1);
    }

    public static void Main(string[] args)
    {

        // Lista pelleilyä.

        List<string> stringeja = new List<string>();
        stringeja.Add("Erkki");
        stringeja.Add("Pupu");

        ListaKikkailua(stringeja);

        // Tämän jälkeen lista on tyhjä.
        OlioViitteenaSisaan(stringeja);
        System.Console.WriteLine(stringeja.Count);

        int luku = 100;
        // luku ei muutu koska luku kopioituu aliohjelmaan.
        InttiSisaan(luku);

        System.Console.WriteLine(luku);

        // Parsitaan.

        Parsettelua("123");
        Parsettelua("erkki");

        // Splittailua
        Splittailua("Erkki ajaa mopolla. Se kulkee lujaa vaihtia, mutta pensa loppui.");

        for (int i = 0; i < 10; i++)
        {
            System.Console.WriteLine(BuilderJuttuja());
        }

        int[,] taalukko = new int[10,5];

        for (int i = 0; i < taalukko.GetLength(0); i++)
        {
            System.Console.WriteLine("\n");
            for (int j = 0; j < taalukko.GetLength(1); j++)
            {
                taalukko[i,j] = i;
                System.Console.Write(" ({0},{1}) ", i, j);
            }
        }

        Iffittelya(123);
        Iffittelya(6);
        Iffittelya(555);

        // 20 + 19 + 18 + ... + 10
        // System.Console.WriteLine(Rekursio(100000)); // Stackoverflow.

        // 1440 % 3000 = 1440 * 2 + 120

        System.Console.WriteLine(JokatoinenPieneksiJokatoinenSuureksi("Eki juo olutta ja kiroilee."));

        Matikkaa(123432.2);


        // Luetaan käyttäjältä syöte.
        System.Console.Write("kerro hetusi:");
        string syote = System.Console.ReadLine();
        System.Console.WriteLine("Hetusi on {0}", syote);

        // String formatointia.
        int luku3 = 3;

        string formatoituLuku = luku3.ToString("0000");
        // 0003
        System.Console.WriteLine(formatoituLuku);

    }






}