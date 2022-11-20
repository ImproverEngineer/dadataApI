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
            DaData daData = new DaData("e2b850d5b2bf12b272b5e2ba475239a45aa3b6c6");
           List<Organisation> organisation =  daData.QueryByInn("7813252159"); //Проверяю по ИНН Гознак

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
            Console.ReadKey();
        }
    }
}
