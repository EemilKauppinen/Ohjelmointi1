using System;
using System.Collections.Generic;
using System.Linq;
public class Tehtava1
{
    public static void Main(string[] args)
    {
        var list1 = new List<int>();
        var list2 = new List<int>();
        //Enumerable.SequenceEqual(list1, list2);
        //PoistaPerattaiset(new int[] { 1, 2, 2, 0, 5, 7, 3, 4, 4, 4, 8 }.ToList());
        //Enumerable.SequenceEqual(new int[] { 1, 2, 2, 0, 5, 7, 3, 4, 4, 4, 8 }.ToList(), new int[] { 1, 2, 0, 5, 7, 3, 4, 8 }.ToList());
        //Enumerable.SequenceEqual(PoistaPerattaiset(new int[] { 1, 2, 2, 0, 5, 7, 3, 4, 4, 4, 8 }.ToList()), new int[] { 1, 2, 0, 5, 7, 3, 4, 8 }.ToList());
        PoistaPerattaiset((new int[] { 1, 2, 2, 0, 5, 7, 3, 4, 4, 4, 8 }).ToList()).SequenceEqual((new int[] { 1, 2, 0, 5, 7, 3, 4, 8 }).ToList());
    }
    /// <summary>
    /// Funktio, joka ottaa parametrina kokonaislukulistan, ja palauttaa uuden listan, josta on poistettu perättäiset samat alkiot.
    /// </summary>
    /// <param name="lukuLista">Alkuperäinen lista</param>
    /// <returns>uuden listan, josta on poistettu perättäiset samat alkiot.</returns>
    public static List<int> PoistaPerattaiset(List<int> lukuLista)
    {
        if (lukuLista.Count == 0)
        {
            return new List<int>();
        }
        List<int> lista = new List<int>();

        int temp = lukuLista[0];
        lista.Add(temp);

        for(int i = 0; i < lukuLista.Count; i++)
        {
            if (temp != lukuLista[i])
            {
                temp = lukuLista[i];
                lista.Add(lukuLista[i]);
            }  
        }
        return lista;
    }
}