namespace GoldLeadsMedia.CoreApi.Services.Partners
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;
    using GoldLeadsMedia.CoreApi.Models.ServicesModels.InputModels;
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

        public async Task<int> FtdScanAsync(DateTime from, DateTime to)
        {
            var startDate = from.ToString("yyyy-MM-ddThh:mm:ss");
            var endDate = to.ToString("yyyy-MM-ddThh:mm:ss");

            var queryParameters = new
            {
                StartDate = startDate,
                EndDate = endDate,
                DepositsOnly = true
            };

            var headers = new Dictionary<string, string>
            {
                ["X-Auth-ClientId"] = "cc94150f-ca09-4950-a23d-bcea325b91f5",
                ["X-Auth-Key"] = "ac7e4690eebc45989d690ffce24cfd5e4923f2dd4ba540e98f637fc06743d5b3"
            };

            //this.httpClient.GetAsync<int>($"https://api.profitpixels.com/client/v5/leads?StartDate={startDate}&EndDate={endDate}&DepositsOnly=true",null, headers)
            var response = await this.httpClient.GetAsync<ProfitPixelsFtdScanResponse>($"https://api.profitpixels.com/client/v5/leads", queryParameters, headers);

            var ftdCounter = 0;
            if (response.Success)
            {
                foreach (var ftdData in response.ResponseData)
                {
                    var lead = this.leadsService.GetBy(ftdData.LeadId, true);

                    var ftd = await this.leadsService.FtdBecomeUpdateLeadAsync(lead, ftdData.FtdDateTime, ftdData.CallStatus);
                    ftdCounter++;
                }
            }
            else
            {
                 
            }

            return ftdCounter;
        }
        public async Task<int> SendLeadsAsync(IEnumerable<string> leadIds, string partnerId, string partnerOfferId)
        {
            var failedLeadsCount = 0;
            var url = "https://api.profitpixels.com/client/v5/leads";

            foreach (var leadId in leadIds)
            {
                var lead = this.leadsService.GetBy(leadId);

                var requestBody = new
                {
                    lead.FirstName,
                    lead.LastName,
                    lead.Email,
                    Language = lead.Country.IsoCode, //TODO This may need some changes due to different partner requierments For now I put country code as language BUT need to think something,
                    OfferId = partnerOfferId,
                    lead.PhoneNumber,
                    lead.Password
                };

                var headers = new Dictionary<string, string>
                {
                    ["X-Auth-ClientId"] = "cc94150f-ca09-4950-a23d-bcea325b91f5", //TODO AppSettings.Production
                    ["X-Auth-Key"] = "ac7e4690eebc45989d690ffce24cfd5e4923f2dd4ba540e98f637fc06743d5b3" //TODO AppSettings.Production
                };

                var response = await this.httpClient.PostAsync<ProfitPixelsSendLeadResponse>(url, requestBody, headers);

                if (response.Success)
                {
                    await this.leadsService.SendSuccessUpdateLeadAsync(lead, partnerId, response.ResponseData.LeadId);
                }
                else
                {
                    var serviceModel = new LeadsRegisterErrorInputServiceModel
                    {
                        LeadId = lead.Id,
                        PartnerId = partnerId,
                        ErrorMessage = response.Message,
                    };

                    await this.leadsService.RegisterErrorAsync(serviceModel);
                    failedLeadsCount++;
                }
            }

            return failedLeadsCount;
        }

        private class ProfitPixelsSendLeadResponse
        {
            public bool Success { get; set; }
            public string Message { get; set; }
            public string RequestId { get; set; }
            public ResponseData ResponseData { get; set; }
        }

        private class ProfitPixelsFtdScanResponse
        {
            public bool Success { get; set; }
            public string Message { get; set; }
            public string RequestId { get; set; }
            public List<ResponseData> ResponseData { get; set; }
        }

        private class ResponseData
        {
            public string LeadId { get; set; }
            public DateTime CreatedDateTime { get; set; }
            public string RedirectUrl { get; set; }
            public string CallStatus { get; set; }
            public bool IsFtd { get; set; }
            public DateTime FtdDateTime { get; set; }
            public bool IsTest { get; set; }
            public string OfferId { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
            public string Language { get; set; }
            public string Ip { get; set; }
            public string CountryCode { get; set; }
            public string CountryPhoneDialCode { get; set; }
            public string PhoneNumber { get; set; }
            public string UserAgent { get; set; }
            public string LeadSourceName { get; set; }
            public string LeadSourceWebsiteUrl { get; set; }
            public string LeadSourceDescription { get; set; }
            public string Sub1 { get; set; }
            public string Sub2 { get; set; }
            public string Sub3 { get; set; }
            public string Sub4 { get; set; }
            public string Sub5 { get; set; }
            public string Sub6 { get; set; }
        }
    }
}
