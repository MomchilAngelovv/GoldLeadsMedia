namespace GoldLeadsMedia.CoreApi.Services.Partners
{
    using System.Collections.Generic;
    using GoldLeadsMedia.CoreApi.Services.Application.Common;
    using GoldLeadsMedia.CoreApi.Services.AsyncHttpClient;
    using GoldLeadsMedia.CoreApi.Services.Partners.Common;

    public class ProfitPixelsPartner : IPartner
    {
        private readonly ILeadsService leadsService;
        private readonly IAsyncHttpClient httpClient;

        public ProfitPixelsPartner(
            ILeadsService leadsService, 
            IAsyncHttpClient httpClient)
        {
            this.leadsService = leadsService;
            this.httpClient = httpClient;
        }

        public int FtdScan()
        {
            throw new System.NotImplementedException();
        }

        public int SendLeads(IEnumerable<string> leadIds)
        {
            foreach (var leadId in leadIds)
            {
                var lead = this.leadsService.GetBy(leadId);



            }

           


            var leadReq = new Dictionary<string, string>
            {
                ["FirstName"] = lead.Names,
                ["CountryCode"] = lead.CountryCode,
                ["Email"] = lead.Email,
                ["Language"] = "en",
                ["LastName"] = lead.Names,
                ["OfferId"] = lead.Offer_Id,
                ["PhoneNumber"] = lead.PhoneNumber,
                ["Password"] = "e54vwTCH9S", //TODO need to remove hard-coded password
            };

            var apiRes = new BaseResultModel();
            var url = "https://api.profitpixels.com/client/v5/leads";
            var headersReq = new Dictionary<string, string>();
            headersReq.Add("X-Auth-ClientId", "cc94150f-ca09-4950-a23d-bcea325b91f5");
            headersReq.Add("X-Auth-Key", "ac7e4690eebc45989d690ffce24cfd5e4923f2dd4ba540e98f637fc06743d5b3");
            var response = WebRequestManager.HttpPostFormEncoded(url, leadReq, headersReq);
            var profitPixelsResponse = JsonConvert.DeserializeObject<ProfitPixelsResultModel>(response.Message);


        }
    }
}
