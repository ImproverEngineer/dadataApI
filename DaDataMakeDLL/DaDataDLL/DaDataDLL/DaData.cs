using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DaData.Client;

namespace DaDataDLL
{
    public class DaData
    {   
        SuggestClient api;
        string Error = null;
        public DaData(string token)
        {          
            var url = "https://suggestions.dadata.ru/suggestions/api/4_1/rs"; // может менятся в последний раз менялся в 2020 году
            this.api = new SuggestClient(token, url);            
        }
        /// <summary>
        /// получаеам по INN
        /// </summary>
        /// <param name="INN"></param>
        public List<Organisation> QueryByInn(string INN)
        {
            try
            {
                List<Organisation> organisations = new List<Organisation>();
                var response = api.QueryParty(INN);
                foreach (var organisation in response.suggestions)
                {
                    
                    DateTime reg_date;
                    if (organisation.data.state.registration_date == null)
                        reg_date = new DateTime(1970, 1, 1, 0, 0, 0, 0);
                    else
                        reg_date = new DateTime(1970, 1, 1, 0, 0, 0, 0).AddMilliseconds(long.Parse(organisation.data.state.registration_date));
                    DateTime actualDate;
                    if (organisation.data.state.actuality_date == null)
                        actualDate = new DateTime(1970, 1, 1, 0, 0, 0, 0);
                    else
                        actualDate = new DateTime(1970, 1, 1, 0, 0, 0, 0).AddMilliseconds(long.Parse(organisation.data.state.actuality_date));
                    string status;
                    if (organisation.data.state.status != null)
                    {
                        status = organisation.data.state.status.ToString();
                    } else
                    {
                        status = "unkom";
                    }

                    organisations.Add(new Organisation
                    {
                        name_organisation = organisation.value,
                        addres = organisation.data.address.value,
                        inn = organisation.data.inn,
                        kpp = organisation.data.kpp,
                        ogrn = organisation.data.ogrn,
                        type = organisation.data.type.ToString(),
                        okpo = organisation.data.okpo,
                        opf_short = organisation.data.opf.@short,
                        status_organisation = status,
                        registration_date = reg_date,
                        actual_data_organisation = actualDate
                    });
                }

                return organisations;
            }
            catch(Exception ex)
            {
               string message = ex.Message;
                return null;
            }
        }

        //получить в том порядке как из Egrul в GZAgr
        public List<EgrulResponse> QueryByInnAsEgulResponse(string INN)
        {
            List<EgrulResponse> egrulResponses = new List<EgrulResponse>();
            try
            {
                var response = api.QueryParty(INN);
                foreach (var organisation in response.suggestions)
                {

                    DateTime reg_date;
                    if (organisation.data.state.registration_date == null)
                        reg_date = new DateTime(1970, 1, 1, 0, 0, 0, 0);
                    else
                        reg_date = new DateTime(1970, 1, 1, 0, 0, 0, 0).AddMilliseconds(long.Parse(organisation.data.state.registration_date));
                    DateTime actualDate;
                    if (organisation.data.state.actuality_date == null)
                        actualDate = new DateTime(1970, 1, 1, 0, 0, 0, 0);
                    else
                        actualDate = new DateTime(1970, 1, 1, 0, 0, 0, 0).AddMilliseconds(long.Parse(organisation.data.state.actuality_date));
                    string status;
                    if (organisation.data.state.status != null)
                    {
                        status = organisation.data.state.status.ToString();
                    }
                    else
                    {
                        status = "unkom";
                    }
                    egrulResponses.Add(new EgrulResponse()
                    { // записываем в том порядке как в GZAgr
                        fullName = organisation.data.name.full, // полное наименование 
                        shortName = organisation.data.name.@short, // короткое наименование
                        inn = organisation.data.inn,
                        kpp = organisation.data.kpp,
                        ogrn = organisation.data.ogrn,
                        opf_short = organisation.data.okpo,
                        ogrnip = "",
                        email = organisation.data.management.post,
                        codeOkved = organisation.data.okved_type,
                        fullAddress = organisation.data.address.value,
                        index = organisation.data.address.data.postal_code,
                        region = organisation.data.address.data.region,
                        codeRegion = organisation.data.address.data.region_fias_id,
                        rayon = organisation.data.address.data.city_area,
                        city = organisation.data.address.data.city,
                        street = organisation.data.address.data.street,
                        house = organisation.data.address.data.house,
                        flat = organisation.data.address.data.flat,
                        country = organisation.data.address.data.country,
                        okato = organisation.data.address.data.okato,
                        oktmo = organisation.data.address.data.oktmo,
                        kladr = organisation.data.address.data.kladr_id,
                        status_organisation = status,
                        registration_date = reg_date,
                        actual_data_organisation = actualDate 
                    }) ; 
                }
                return egrulResponses;
            }
            catch (Exception ex)
            {
                this.Error = ex.Message;
                return null;
            }
        }

        public string getPartyQuery(string Party) 
        {
            string index = "";
            var response = api.QueryParty(Party);
            foreach (var r in response.suggestions) 
            {
               index = r.data.management.post;            
            }
            return index;
        }
    }

    public class Organisation
    {
        public string name_organisation { get; set; } = null;
        public string addres { get; set; } = null;
        public string inn { get; set; } = null;
        public string kpp { get; set; } = null;
        public string ogrn { get; set; } = null;
        public string type { get; set; } = null;
        public string okpo { get; set; } = null;
        public string opf_short { get; set; } = null;
        public string status_organisation { get; set; } = null;
        public DateTime registration_date { get; set; }
        public DateTime actual_data_organisation { get; set; }
    }

    #region берем наименование по новому со всеми полями которые присутствуют в GZAGR. 
    public class EgrulResponse : Organisation
    {
        //привести к значению null  в случае если не 
        public string fullName { get; set; } = null;
        public string shortName { get; set; } = null;
        public string ogrnip { get; set; } = null;
        public string email { get; set; } = null;
        public string codeOkved { get; set; } = null;
        public string fullAddress { get; set; } = null;
        public string index { get; set; } = null;
        public string region { get; set; } = null;
        public string codeRegion { get; set; } = null;
        public string rayon { get; set; } = null;
        public string city { get; set; } = null;
        public string street { get; set; } = null;
        public string house { get; set; } = null;
        public string flat { get; set; } = null;
        public string country { get; set; } = null;
        public string okato { get; set; } = null;
        public string oktmo { get; set; } = null;
        public string kladr { get; set; } = null;
    }
    #endregion

}
