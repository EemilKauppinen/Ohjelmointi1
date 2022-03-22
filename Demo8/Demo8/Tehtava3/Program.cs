public class Tehtava3
{
    public static void Main()
    {

    }
    //public static string VuodenaikaIf(int kuukaudenNumero)
    //{
    //    if ((kuukaudenNumero > 0 && kuukaudenNumero < 3) || kuukaudenNumero == 12)
    //    {
    //        return "Talvi";
    //    }
    //    else if (kuukaudenNumero > 2 && kuukaudenNumero < 6)
    //    {
    //        return "Kevät";
    //    }
    //    else if (kuukaudenNumero > 5 && kuukaudenNumero < 9)
    //    {
    //        return "Kesä";
    //    }
    //    else if (kuukaudenNumero > 8 && kuukaudenNumero < 12)
    //    {
    //        return "Syksy";
    //    }
    //    return "";
    //}
    //public static string VoudenaikaSwitch(int kuukaudenNumero)
    //{
    //    String result = "";

    //    switch (kuukaudenNumero) 
    //    {
    //        case 1:
    //            result = "Talvi";
    //            break;

    //        case 2:
    //            result = "Talvi";
    //            break;

    //        case 3:
    //            result = "Kevät";
    //            break;

    //        case 4:
    //            result = "Kevät";
    //            break;

    //        case 5:
    //            result = "Kevät";
    //            break;

    //        case 6:
    //            result = "Kesä";
    //            break;

    //        case 7:
    //            result = "Kesä";
    //            break;

    //        case 8:
    //            result = "Kesä";
    //            break;

    //        case 9:
    //            result = "Syksy";
    //            break;

    //        case 10:
    //            result = "Syksy";
    //            break;

    //        case 11:
    //            result = "Syksy";
    //            break;

    //        case 12:
    //            result = "Talvi";
    //            break;

    //        default:
                
    //            break;
    //    }


    //    return result;
    //}
    public static String VuodenaikaTaulukko(int kuukaudenNumero)
    {

        string[] vuodenAjat = new string[]
        {
            "",
            "Talvi",
            "Talvi",
            "Kevät",
            "Kevät",
            "Kevät",
            "Kesä",
            "Kesä",
            "Kesä",
            "Syksy",
            "Syksy",
            "Syksy",
            "Talvi"

        };


        if (kuukaudenNumero < 0 || kuukaudenNumero >= vuodenAjat.Length)
        {
            return "";
        }
        return vuodenAjat[kuukaudenNumero];
    }
}
