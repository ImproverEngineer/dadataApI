using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DaDataDLL;
using static DaDataDLL.DaData;

namespace ConsoleDaData
{
    class Program
    {
        static void Main(string[] args)
        {
            string inn = "5904002096";//"7813252159";
            DaData daData = new DaData("e2b850d5b2bf12b272b5e2ba475239a45aa3b6c6");
            // shortInformation(daData, inn);
             getlongInformation(daData, inn);
    
            Console.ReadKey();
        }

        #region короткая информация о компании
        public static void shortInformation(DaData daData, string inn) 
        {
            List<Organisation> organisation = daData.QueryByInn("7813252159"); //Проверяю по ИНН Гознак

            if (organisation != null)
            {
                foreach (Organisation o in organisation)
                {
                    Console.WriteLine(o.name_organisation);
                    Console.WriteLine(o.addres);
                    Console.WriteLine(o.inn);
                    Console.WriteLine(o.kpp);
                    Console.WriteLine(o.ogrn);
                    Console.WriteLine(o.opf_short);
                    Console.WriteLine(o.status_organisation);
                    Console.WriteLine(o.type);
                    Console.WriteLine(o.registration_date);
                    Console.WriteLine(o.actual_data_organisation);                    
                    Console.WriteLine();
                }
            }
        }
        #endregion

        public static void getlongInformation(DaData daData, string inn) 
        {
         List<EgrulResponse> egrulResponses = daData.QueryByInnAsEgulResponse(inn);
            if (egrulResponses != null) 
            {
                foreach (EgrulResponse e in egrulResponses) 
                {                    
                    Console.WriteLine("Full name: {0}", e.fullName);
                    Console.WriteLine("Short_naem: {0}",    e.shortName);
                    Console.WriteLine("Opf: {0}",e.opf_short);
                    Console.WriteLine("INN: {0}",   e.inn);
                    Console.WriteLine("KPP: {0}",   e.kpp);
                    Console.WriteLine("OGRN: {0}",  e.ogrn);
                    Console.WriteLine("OGRNIP: {0}",    e.ogrnip);
                    Console.WriteLine("EMAIL: {0}", e.email);
                    Console.WriteLine("CodeOkved: {0}", e.codeOkved);
                    Console.WriteLine("Full adres: {0}", e.fullAddress);
                    Console.WriteLine("INDEX: {0}", e.index);
                    Console.WriteLine("Region: {0}", e.region);
                    Console.WriteLine("CodeRegion: {0}",    e.codeRegion);
                    Console.WriteLine("Район: {0}", e.rayon);
                    Console.WriteLine("Город: {0}", e.city);
                    Console.WriteLine("Улица: {0}", e.street);
                    Console.WriteLine("Дом: {0}",   e.house);
                    Console.WriteLine("Квартира: {0}",  e.flat);
                    Console.WriteLine("Страна: {0}",    e.country);
                    Console.WriteLine("OKATO: {0}", e.okato);
                    Console.WriteLine("OKTMO: {0}", e.oktmo);
                    Console.WriteLine("KLADR: {0}", e.kladr);
                    Console.WriteLine(" ");
                }
            }

        }

    }
}
