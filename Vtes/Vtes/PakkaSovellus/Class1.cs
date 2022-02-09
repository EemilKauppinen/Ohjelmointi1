using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class Pelaaja
{
    private String nimi;
    private int aloitus_numero;

}

public class Pakka
{
    private String nimi;

    public Pakka(String nimi)
    {
        this.nimi = nimi;
    }

    public String Nimi {
        get { return this.nimi; }
        set
        {
            this.nimi = value;
        }     
    }
}

public class PakkaSovellus
{

    private Pakka[] pakat = new Pakka[] {
        new Pakka("Kiemura_Tassu"),
        new Pakka("Gangrel_Karhut"),
        new Pakka("Julius_Quijetus"),
        new Pakka("TremereAssamite"),
        new Pakka("Roteanpakka"),
        new Pakka("Aziz_Ja_Mato"),
        new Pakka("KiemuraTiimi"),
        new Pakka("DementiaMies"),
        new Pakka("JimmynPakka"),
        new Pakka("TraumaTiimi"),
        new Pakka("RaumaSeta"),
        new Pakka("PaakalloBleedaus"),
        new Pakka("HullutMalkaaviat"),
        new Pakka("PoliittinenBleedPakka"),
        new Pakka("RinssitToreadorJaBrujah"),
        new Pakka("GuruhiTiimi"),
        new Pakka("Oudot_Tzimisckit")
    };

    private string[] pelaajienNimet = new string[] {
      "Eemil",
      "Janne",
      "Ari"
    };


    public PakkaSovellus() { }

    public Pakka[] Pakat {
        get { return this.pakat; }
        set { this.pakat = value; }
    }

    public string[] Pelaajat {
        get { return this.pelaajienNimet; }
        set { this.pelaajienNimet = value; }
    }

    public static void Main(string[] args)
    {
        PakkaSovellus pakkaSovellus = new PakkaSovellus();

        Random rng = new Random();

        List<Pakka> pakkaLista = pakkaSovellus.Pakat.ToList();

        List<string> pelaajat = pakkaSovellus.Pelaajat.ToList();

        for (int i = 0; i < 3; i++) 
        {
            int a = rng.Next(0, pelaajat.Count);
            string pelaaja = pelaajat[a];
            pelaajat.RemoveAt(a);

            int b = rng.Next(0, pakkaLista.Count);
            Pakka pakka = pakkaLista[b];
            pakkaLista.RemoveAt(b);

            System.Console.WriteLine("{0} : {1} : {2}", i, pelaaja, pakka.Nimi );
            
        }


        //Pakka pakka = pakkaSovellus.Pakat[rng.Next(0, pakkaSovellus.Pakat.Length)];
        //Pakka pakka2 = pakkaSovellus.Pakat[rng.Next(0, pakkaSovellus.Pakat.Length)];
        //Pakka pakka3 = pakkaSovellus.Pakat[rng.Next(0, pakkaSovellus.Pakat.Length)];
        //System.Console.WriteLine("{0}. ", pakka.Nimi);
        //System.Console.WriteLine("{0}. ", pakka2.Nimi);
        //System.Console.WriteLine("{0}. ", pakka3.Nimi);
           
        //pakkaSovellus.Pakat[]
        //foreach (Pakka pakka in pakkaSovellus.Pakat)
        //{
        //    Console.WriteLine("{0}", pakka.Nimi);
        //}
        //// Pakka pakkanen = new Pakka("RaumaSeta");
        // Console.WriteLine("{0}", pakkanen.Nimi);
    }
}

