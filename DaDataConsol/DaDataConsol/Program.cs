using DaData.Client;
internal class Program
{
    private static void Main(string[] args)
    {
        var token = "e2b850d5b2bf12b272b5e2ba475239a45aa3b6c6";
        var url = "https://suggestions.dadata.ru/suggestions/api/4_1/rs";
        var api = new SuggestClient(token, url);
        var query = "москва серпухов";
        //var response = api.QueryAddress(query);
        var response = api.QueryParty("7813252159");
        //  Console.WriteLine(string.Join("\n", response.suggestions));

        foreach (var organisation in response.suggestions)
        {

            Console.WriteLine("name organisation: " + organisation.value.ToString());
            Console.WriteLine("addres: " + organisation.data.address);
            Console.WriteLine("INN: " + organisation.data.inn);
            Console.WriteLine("KPP: " + organisation.data.kpp);
            Console.WriteLine("OGRN: " + organisation.data.ogrn);
            Console.WriteLine("DATA TYPE: " + organisation.data.type);
            Console.WriteLine("OKPO: " + organisation.data.okpo);
            //Console.WriteLine("OPF full: " + organisation.data.opf.full);
            //Console.WriteLine("OPF code: " + organisation.data.opf.code);
            Console.WriteLine("OPF short: " + organisation.data.opf.@short);
            Console.WriteLine("STATE: " + organisation.data.state.status);
            if (organisation.data.state.registration_date != null)
                Console.WriteLine("STATE: " + new DateTime(1970, 1, 1, 0, 0, 0, 0).AddMilliseconds(long.Parse(organisation.data.state.registration_date)));
            if (organisation.data.state.actuality_date != null)
                Console.WriteLine("STATE ACTUAL DATE:" + new DateTime(1970, 1, 1, 0, 0, 0, 0).AddMilliseconds(long.Parse(organisation.data.state.actuality_date)));
            Console.WriteLine();
        }
        Console.ReadKey();
    }
}