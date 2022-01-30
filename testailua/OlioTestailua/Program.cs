using System;
using System.Collections;
using System.Linq;

public class Program
{
    public static void Main(string[] args)
    {
        string[] nimet = new string[]
        {
            "Erkki",
            "Eki",
            "Julius",
            "Kake",
            "Keke",
            "Aziz"
        };

        string[] rodut = new string[]
        {
            "luppa korva",
            "rex",
            "sekarotunen",
            "mini janis",
            "mutantti",
            "villi"
        };

        string[] varit = new string[]
        {
            "valkoinen",
            "musta",
            "ruskea",
            "harmaa",
            "vihrea"
        };

        List<Pupu> puput = new List<Pupu>();

        Random rng = new Random();

        System.Console.WriteLine("Tulostetaan kaikki puput.");
        System.Console.WriteLine("-------------------------");

        for (int i = 0; i < 100; i++)
        {
            int pupun_ika = rng.Next(0, 80);
            string pupun_nimi = nimet[rng.Next(0, nimet.Length)];
            string pupun_rotu = rodut[rng.Next(0, rodut.Length)];
            string vari = varit[rng.Next(0, varit.Length)];
            Pupu isa_janis = new Pupu(pupun_ika, pupun_nimi, pupun_rotu, vari);
            puput.Add(isa_janis);
            System.Console.Write("{0}. ", i);
            isa_janis.tulosta_tiedot();
        }



        double voittoAika100m = 9.12;
        List<Pupu> vihreat_mutantit = puput.Where(
                        pupu => pupu.get_vari().Equals("vihrea") && pupu.get_rotu().Equals("mutantti")
        ).ToList();

        System.Console.WriteLine("\nTulostetaan vihreat mutantit.");
        System.Console.WriteLine("--------------------");

        for (int i = 0; i<vihreat_mutantit.Count; i++)
        {
            vihreat_mutantit[i].tulosta_tiedot();
        }
    }
}

public class Pupu
{
    private int ika = 0;      // this.ika
    private string nimi = ""; // this.nimi
    private string rotu = ""; // this.rotu
    private string vari = ""; // this.vari

    public static int Joop()
    {
        return 123;
    }

    public Pupu(int ika, string nimi, string rotu, string vari)
    {
        if (ika < 0)
        {
            throw new ArgumentException("Ika ei voi olla negatiivinen.");
        }
        this.ika = ika;
        this.nimi = nimi;
        this.rotu = rotu;
        this.vari = vari;
    }

    public int get_ika()
    {
        return this.ika;
    }

    public string get_rotu()
    {
        return this.rotu;
    }

    public string get_vari()
    {
        return this.vari;
    }

    public void set_ika(int ika)
    {
        if (ika < 0)
        {
            throw new ArgumentException("Ika ei voi olla negatiivinen.");
        }
        this.ika = ika;
    }

    /// <summary>
    /// Tulostaa pupun tiedot.
    /// </summary>
    public void tulosta_tiedot()
    {
        System.Console.WriteLine("Pupun tiedot: nimi == {0,-12}, ika == {1,-12}, rotu == {2,-12}, vari == {3,-12}", this.nimi, this.ika, this.rotu, this.vari);
    }
}