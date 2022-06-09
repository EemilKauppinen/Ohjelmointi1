using System.Collections.Generic;
using System.Text;

//class Testailua
//{

//    //    //public static void Main()
//    //    {
//    //        List<int> list = new List<int>();
//    //        list.Add(1);
//    //        list.Add(0);
//    //        list.Add(14);
//    //        list.Add(9);
//    //        list.Add(6);
//    //        list.Add(9);
//    //        list.Add(11);
//    //        list.Add(24);
//    //        list.Add(98);
//    //        EtsiSuurinNumero(list);

//for (int i = 0; // 1.
//     i < 10;    // 2.
//     i++)       // 4.
//{
//    Console.WriteLine(i); // 3. 
//}

//List<int> list = new List<int>(); list.Add(1); list.Add(2);
//foreach (int listanAlkio in list)
//{
//    Console.WriteLine(listanAlkio); 
//    // Käy tietorakenten läpi alusta loppuun.
//}

//int j = 0; // 1.
//while (j < 10) // 2.
//{
//    // while luuppiin ei mennä jos ehto ei ole tosi.
//    Console.WriteLine(j); // 3. 
//    j++; // 4.
//}

//int å = 0; // 1.
//do
//{   // do while luuppi suoritetaan aina vähintään kerran.
//    Console.WriteLine(å); // 3. 
//    å++; // 4.
//} while (å < 10); // 2.

//int ika = 144;
//switch (ika)
//{
//    case < 15:
//        Console.WriteLine("Nuorukainen.");
//        break;

//    case < 30:
//        Console.WriteLine("Melkein patu");
//        break;

//    case < 60:
//        Console.WriteLine("Patu.");
//        break;

//    default:
//        Console.WriteLine("Muumio.");
//        break;
//}

// If ja else if ovat toistensa poissulkevia

// if () // pelkät if lauseet eivät ole poissulkevia

// else if () // else if on muussatapauksessa poissulkeva

// else // Muussa tapauksessa



/// <summary> // kertoo yleisesti ja lyhyesti aliohjelman toiminnan
/// funktio, joka palauttaa int-lukuja sisältävän taulukon pisimmän aidosti kasvavan osajonon pituuden.
/// </summary> 
/// <param name="intTaulukko">Taulukollinen numeroita joita testataan</param> // parametri lista
/// <returns>palauttaa int-lukuja sisältävän taulukon pisimmän aidosti kasvavan osajonon pituuden.</returns> // kuvaus mitä ohjelma paluttaa
/// <example> // testit
/// <pre name="test">
/// PisinNouseva(new int[]{ 1,5,12,89,64,77,1000}) === 4; 
/// PisinNouseva(new int[]{ 0,0,0,0,0,0,0}) === 1; 
/// PisinNouseva(new int[]{ -1,-7,0,8,7,6}) === 3; 
/// PisinNouseva(new int[]{ 1}) === 1; 
/// </pre>
/// </example> 

//    //    }
//    //    public static int EtsiSuurinNumero(List<int> numeroLista)
//    //    {
//    //        int isoinNumero = 0;

//    //        int tamanHetkinenNumero = numeroLista[0];

//    //        for (int i = 0; i < numeroLista.Count; i++)
//    //        {
//    //            tamanHetkinenNumero = numeroLista[i];
//    //            if (tamanHetkinenNumero > isoinNumero)
//    //            {
//    //                isoinNumero = tamanHetkinenNumero;
//    //            }

//    //        }

//    //        System.Console.WriteLine(isoinNumero);
//    //        return isoinNumero;
//    //    }

//    //}
//}
class Testailua2
{

    public static void Main()
    {
        //    //List<String> jonot = new List<String>() { "Antti", "Jussi", "Viljo", "Aati", "Henna" };
        //    //Testailua2.PoistaSisaltavat(jonot, "tti");
        //    //// jonot-lista sisältäisi nyt alkiot "Jussi", "Viljo", "Aati", "Henna"

        //    //List<int> luvut = new List<int>() { 1,2,3,4,5,6,7,8,9 };

        //    //luvut.RemoveAll(luku => luku == 6);

        //    //for (int i = 0; i < luvut.Count; i++)
        //    //{
        //    //    System.Console.WriteLine(luvut[i]);
        //    //}
        //    //MinuutitAjaksi(150);
        //    //AikaMinuuteiksi("09:43");
        //    //AikaMinuuteiksi("00:01");
        //    //AikaMinuuteiksi("04:00");
        //    //AikaMinuuteiksi("23:59");
        //    //LisaaAikaa("16:01", "00:05");


        int ika = 20;
        ika++; // 21
        ika--; // 20
        ika += 5; // 25
        ika *= 2; // 50
        ika -= 10; // 40
        ika /= 2; // 20
        ika %= 9; // 2

        string[] taulukko = new string[10];

        rekursio(20);
    }

    public static void rekursio(int luku)
    {
        if (luku == 10)
        {
            System.Console.WriteLine("Kymppi tuli. Katkaistaan rekursio.");
            return;
        }
        else
        {
            System.Console.WriteLine("Luku on {0}. Jatketaan.", luku);
        }
        int uusi_luku = luku - 1;
        rekursio(uusi_luku);
    }
    /// <summary> Antaa uuden kellonajan siten että vanhaan aikaan
    /// (aika-parametri) lisätään jokin aika (lisays-parametri). Huomaa, että käytössä
    /// on 24 tunnin kello.</summary>
    /// <param name="aika">Alkuperäinen kellonaika (muodossa hh:mm)</param>
    /// <param name="lisays">Lisäys (muodossa hh:mm)</param>
    /// <returns>Uusi kellonaika (muodossa hh:mm)</returns>
    /// <example>
    /// <pre name="test">
    /// Aika.LisaaAikaa("16:01", "00:05") === "16:06";
    /// Aika.LisaaAikaa("16:01", "00:00") === "16:01";
    /// Aika.LisaaAikaa("16:59", "00:11") === "17:10";
    /// Aika.LisaaAikaa("00:01", "01:01") === "01:02";
    /// Aika.LisaaAikaa("12:00", "00:00") === "12:00";
    /// Aika.LisaaAikaa("00:00", "00:01") === "00:01";
    /// Aika.LisaaAikaa("00:00", "00:00") === "00:00";
    /// </pre>
    /// </example>
    public static String LisaaAikaa(String aika, String lisays)
    {

        int lisattavaAika = AikaMinuuteiksi(aika) + AikaMinuuteiksi(lisays);
        //MinuutitAjaksi(lisattavaAika);
        String uusiaika = MinuutitAjaksi(lisattavaAika);
        return uusiaika;
    }



    /// <summary>Muuttaa annetut minuutit kellonajaksi.</summary>
    /// <param name="minuutit">Aika minuutteina (kokonaisluku).</param>
    /// <returns>Kellonaika merkkijonona.</returns>
    /// <example>
    /// <pre name="test">
    /// Tentti.MinuutitAjaksi(1) === "00:01";
    /// Tentti.MinuutitAjaksi(240) === "04:00";
    /// Tentti.MinuutitAjaksi(780) === "13:00";
    /// Tentti.MinuutitAjaksi(1439) === "23:59";
    /// Tentti.MinuutitAjaksi(1440) === "00:00"; // Jos kaksi viimeistä testiä
    /// Tentti.MinuutitAjaksi(1441) === "00:01"; // menee läpi niin BONUS +1 p.
    /// </pre>
    /// </example>
    public static String MinuutitAjaksi(int minuutit)
    {

        StringBuilder kellonAika = new StringBuilder();
        int paivienLukumaara = minuutit/1440;
        int vuoroKaudenminuuttienMaara = minuutit - 1440*paivienLukumaara;
        int tuntienMaara = vuoroKaudenminuuttienMaara/60;
        int minuuttienMaara = vuoroKaudenminuuttienMaara - tuntienMaara*60;

        //System.Console.WriteLine(kellonAika.ToString()); 
        //System.Console.WriteLine(minuuttienMaara);
        //System.Console.WriteLine(tuntienMaara);
        //return kellonAika.ToString();
        System.Console.WriteLine(String.Format("{0,2:00}:{1,2:00}", tuntienMaara, minuuttienMaara));
        return String.Format("{0,2:00}:{1,2:00}", tuntienMaara, minuuttienMaara);
    }


    /// <summary>Muuttaa merkkijonona annetun ajan (hh:mm) minuuteiksi.
    /// Käytetään edelleen 24 tunnin kelloa. </summary>
    /// <param name="aika">Aika merkkijonona.</param>
    /// <returns>Aika minuutteina.</returns>
    /// <example>
    /// <pre name="test">
    /// Tentti.AikaMinuuteiksi("00:01") === 1;
    /// Tentti.AikaMinuuteiksi("04:00") === 240;
    /// Tentti.AikaMinuuteiksi("23:59") === 1439;
    /// Tentti.AikaMinuuteiksi("00:00") === 0;
    /// </pre>
    /// </example>
    public static int AikaMinuuteiksi(String aika)
    {
        string[] taulukko = aika.Split(':');
        int tunnit = int.Parse(taulukko[0]);
        int minuutit = int.Parse(taulukko[1]);
        int lopullinenAika = minuutit + tunnit*60;
        System.Console.WriteLine(lopullinenAika);
        //System.Console.WriteLine(tunnit);
        //System.Console.WriteLine(minuutit);
        return lopullinenAika;
    }








    


















    //public static int mun_funktio(int x)
    //{
    //    int temppi = x + 1;
    //    return temppi * 5;
    //}

    //public static bool sisaltaa(string st, string poistettava)
    //{
    //    return st.Contains(poistettava);
    //}

    //public static List<string> PoistaSisaltavat(List<string> lista, string poistettava)
    //{


    //    Func<int, int> mun_funktio = x => { int temppi = x + 1; return temppi * 5; };

    //    System.Console.WriteLine(mun_funktio(4));

    //    // lista.RemoveAll(str => str.Contains(poistettava));
    //     

    //    for (int i = 0; i < lista.Count; i++)
    //    {
    //        System.Console.WriteLine(lista[i]);
    //    }
    //    return lista;
    //}

















    //    public static void tulostaLuvut()
    //    {

    //        for (int i = 1; i < 101; i++)
    //        {
    //            Console.Write(i + " ");
    //            if (i % 7 == 0 && i % 4 == 0)
    //            {
    //                Console.Write("HipHei");
    //            }
    //            else if (i % 4 == 0)
    //            {
    //                Console.Write("Hip");
    //            }
    //            else if (i % 7 == 0)
    //            {
    //                Console.Write("Hei");
    //            }
    //            Console.WriteLine("");
    //        }

    //    }
}

//public class KayttajaTunnus
//{
//    public static void Main()
//    {
//        System.Console.WriteLine(TeeTunnus("Yrjö", "Åkerman"));
//        System.Console.WriteLine(TeeTunnus("Arnold", "Schwarzenegger"));
//    }

//    /// <summary>
//    /// Muuttaa kirjaimen käyttäjätunnukseen kelpaavaksi.
//    /// Kirjain muutetaan pieneksi ja skandeista poistetaan pisteet
//    /// Tuntemattomat kirjaimet ja mahdolliset välimerkit
//    /// korvataan 1-merkillä.
//    /// </summary>
//    /// <param name="c">muutettava kirjain</param>
//    /// <returns>käyttäjätunnukseen kelpaava kirjain</returns>
//    /// <example>
//    /// MuutaKirjain(' ') === '1';
//    /// MuutaKirjain('2') === '1';
//    /// MuutaKirjain('A') === 'a';
//    /// MuutaKirjain('B') === 'b';
//    /// MuutaKirjain('a') === 'a';
//    /// MuutaKirjain('Ä') === 'a';
//    /// MuutaKirjain('Ö') === 'o';
//    /// MuutaKirjain('ö') === 'o';
//    /// </example>
//    public static char MuutaKirjain(char c)
//    {
//        String mitka = "åäö";
//        String miksi = "aao";
//        char lc = Char.ToLower(c);
//        int i = mitka.IndexOf(lc);
//        if (i >= 0) return miksi[i];
//        if (lc < 'a') return '1';
//        if (lc > 'z') return '1';
//        return lc;
//    }
//    /// <summary>
//    /// Aliohjelma muuttaa nimen käyttäjätunnukseksi.
//    /// Käyttäjätunnus muodostetaan yhdistämällä käyttäjän
//    /// etunimen ensimmäinen kirjain ja sukunimen ensimmäiset
//    /// seitsemän kirjainta, jolloin käyttäjätunnuksesta
//    /// tulee 8 kirjainta pitkä.
//    /// Jos sukunimessä ei ole kahdeksaa merkkiä,
//    /// sukunimi otetaan mukaan kokonaisuudessaan.
//    /// Kirjaimet ä, ö ja å korvataan kirjaimilla a, o ja a.
//    /// Käyttäjätunnus sisältää ainoastaan pieniä kirjaimia.
//    /// </summary>
//    /// <param name="etunimi">muutettavan nimen etunimi</param>
//    /// <param name="sukunimi">muutettvan nimen sukunimi</param>
//    /// <returns>nimien perusteella muodostettu käyttäjätunnus</returns>
//    public static String TeeTunnus(String etunimi, String sukunimi)
//    {

//        StringBuilder kauttajaTunnus = new StringBuilder();

//        kauttajaTunnus.Append(MuutaKirjain(etunimi[0]));

//        for (int i = 0; i < sukunimi.Length; i++)
//        {
//            if (i > 6)
//            {
//                break;
//            }
//            kauttajaTunnus.Append(MuutaKirjain(sukunimi[i]));
//        }
//            return kauttajaTunnus.ToString();
//    }

//}
public class testailua3
{

    //public static void Main(String[] args)
    //{
    //    //int[] luvut = { 5, 9, 3, 7, 8 };
    //    //int[] luvutKaannetty = KaannaTaulukko(luvut);
    //    //System.Console.WriteLine(Palindromi("kissa"));
    //    //System.Console.WriteLine(Palindromi("saippuakauppias"));
    //    //System.Console.WriteLine(Palindromi("Innostunutsonni"));
    //}

    //public static int[] KaannaTaulukko(int[] luvut)
    //{

    //    int[] kaantaisetLuvut = new int[luvut.Length];
    //    for (int i = 0; i < luvut.Length; i++)
    //    {

    //        kaantaisetLuvut[i] = luvut[luvut.Length - i -1];

    //    }
    //    for (int i = 0; i < kaantaisetLuvut.Length;i++)
    //    {
    //        System.Console.WriteLine(kaantaisetLuvut[i]);
    //    }




    //        return kaantaisetLuvut;
    //}

    //public static bool Palindromi(string jono) {
    //    for (int i = 0; i < jono.Length; i++) {
    //        if (Char.ToLower(jono[i]) == (Char.ToLower(jono[jono.Length-1-i]))) { continue; }
    //        else { return false; } 
    //    }
    //}



    //    return true;
}

//public class Tehtava4
//{
//    /// <summary>
//    /// Kysytään käyttäjältä ympyrän säde. Tulostetaan pinta-ala.
//    /// </summary>
//    public static void Main()
//    {
//      //System.Console.WriteLine("Anna ympyrän säde > ");
//      //String luku = System.Console.ReadLine();
//      //double sade = double.Parse(luku);
//      //System.Console.WriteLine(Math.PI*Math.Pow(sade, 2));

//      //for (int i = 1; i < 129; i *= 2)
//      //{
//      //  System.Console.WriteLine(i);
//      //}

//      int j = 1;
//      while (j < 100)
//    {

//        double k = Math.Pow(-1, j+1);

//        System.Console.WriteLine(k*j);

//        j++;
//    }

//    }

//}
public class Testaulua5
{
    /// <summary>
    /// Luodaan taulukko ja kutsutaan funktiota.
    /// </summary>
    //public static void Main()
    //{
    //    Random r = new Random();
    //    int[] luvut = new int[r.Next(10, 21)];
    //    for (int i = 0; i < luvut.Length; i++)
    //    {
    //        luvut[i] = r.Next(-10, 11);


    //    }
    //    //for (int i = 0; i < luvut.Length; i++)
    //    //{
    //    //    System.Console.WriteLine(luvut[i]);
    //    //}
    //    jarjesta(luvut);


    //}

    public static int[] jarjesta(int[] taulukko)
    {
        bool onVaihdettu = true;

        int[] jarjestetty = new int[taulukko.Length];

        for (int i = 0; i < taulukko.Length; i++)
        {
            jarjestetty[i] = taulukko[i];
        }

        while(onVaihdettu == true)
        {
            onVaihdettu = false;
            for (int i = 0; i < taulukko.Length-1; i++)
            {
                
                if (jarjestetty[i] < jarjestetty[i+1])
                {
                    // käännä

                    int temp = jarjestetty[i];

                    jarjestetty[i] = jarjestetty[i+1];

                    jarjestetty[i+1] = temp;

                    onVaihdettu = true;

                }
                
            }

            
        }

        for (int i = 0; i < jarjestetty.Length; i++)
        {
            System.Console.WriteLine(jarjestetty[i]);
        }
        return taulukko;
    }



}



