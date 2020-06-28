namespace GoldLeadsMedia.CoreApi.Services.Brokers
{
    using System;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using GoldLeadsMedia.CoreApi.Services.Common;
    using GoldLeadsMedia.CoreApi.Models.Services.Input;
    using GoldLeadsMedia.CoreApi.Services.AsyncHttpClient;

    public class ProfitPixelsBroker : IBroker
    {
        private readonly ILeadsService leadsService;
        private readonly IErrorsService errorsService;
        private readonly IAsyncHttpClient httpClient;

        private readonly string brokerId = "e4eb3f0f-c97e-468e-9567-91cfb237b0e2";

        public ProfitPixelsBroker(
            ILeadsService leadsService,
            IErrorsService errorsService,
            IAsyncHttpClient httpClient)
        {
            this.leadsService = leadsService;
            this.errorsService = errorsService;
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

            var ftdScanResponse = await httpClient.GetAsync<ProfitPixelsFtdScanResponse>($"https://api.profitpixels.com/client/v5/leads", queryParameters, headers);

            var ftdCounter = 0;
            if (ftdScanResponse.Success)
            {
                foreach (var ftdData in ftdScanResponse.ResponseData)
                {
                    var lead = leadsService.GetBy(ftdData.LeadId, true);

                    var ftd = await leadsService.FtdSuccessAsync(lead, ftdData.FtdDateTime, ftdData.CallStatus);
                    ftdCounter++;
                }
            }
            else
            {
                var serviceModel = new ErrorsRegisterFtdScanErrorInputServiceModel
                {
                    Message = ftdScanResponse.Message,
                    Information = $"Request Id: {ftdScanResponse.RequestId}",
                    BrokerId = this.brokerId
                };

                await errorsService.RegisterFtdScanErrorAsync(serviceModel);
            }

            return ftdCounter;
        }
        public async Task<int> SendLeadsAsync(IEnumerable<string> leadIds)
        {
            var failedLeadsCount = 0;
            var url = "https://api.profitpixels.com/client/v5/leads";

            foreach (var leadId in leadIds)
            {
                var lead = leadsService.GetBy(leadId);

                var requestBody = new
                {
                    lead.FirstName,
                    lead.LastName,
                    lead.Email,
                    Language = lead.Country.IsoCode, //TODO This may need some changes due to different partner requierments For now I put country code as language BUT need to think something,
                    OfferId = "Fill this with their offer ID",
                    lead.PhoneNumber,
                    lead.Password
                };

                var headers = new Dictionary<string, string>
                {
                    ["X-Auth-ClientId"] = "cc94150f-ca09-4950-a23d-bcea325b91f5",
                    ["X-Auth-Key"] = "ac7e4690eebc45989d690ffce24cfd5e4923f2dd4ba540e98f637fc06743d5b3"
                };

                var sendLeadResponse = await httpClient.PostAsync<ProfitPixelsSendLeadResponse>(url, requestBody, headers);

                if (sendLeadResponse.Success)
                {
                    await leadsService.SendLeadSuccessAsync(lead, this.brokerId, sendLeadResponse.ResponseData.LeadId);
                }
                else
                {
                    var serviceModel = new ErrorsRegisterSendLeadErrorInputServiceModel
                    {
                        LeadId = lead.Id,
                        BrokerId = this.brokerId,
                        ErrorMessage = sendLeadResponse.Message,
                    };

                    await errorsService.RegisterSendLeadErrorAsync(serviceModel);
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
