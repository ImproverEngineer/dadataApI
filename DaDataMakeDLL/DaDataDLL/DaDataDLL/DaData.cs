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
    }
}
