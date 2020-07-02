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
        private readonly IAffiliatesService affiliatesService;
        private readonly IClicksRegistrationsService clicksRegistrationsService;

        private readonly string brokerId = "8e18406a-a348-4216-9028-7670c0d43ca7";

        public ProfitPixelsBroker(
            ILeadsService leadsService,
            IErrorsService errorsService,
            IAsyncHttpClient httpClient,
            IAffiliatesService affiliatesService,
            IClicksRegistrationsService clicksRegistrationsService)
        {
            this.leadsService = leadsService;
            this.errorsService = errorsService;
            this.httpClient = httpClient;
            this.affiliatesService = affiliatesService;
            this.clicksRegistrationsService = clicksRegistrationsService;
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

                    var ftd = await leadsService.FtdSuccessAsync(lead, ftdData.FtdDateTime.GetValueOrDefault(), ftdData.CallStatus);

                    var trackerConfiguration = this.affiliatesService.GetTrackerSettings(lead.ClickRegistration?.Affiliate?.Id);
                    var clickRegistration = this.clicksRegistrationsService.GetBy(lead.ClickRegistrationId);

                    if (trackerConfiguration != null && string.IsNullOrWhiteSpace(trackerConfiguration.FtdPostbackUrl) == false)
                    {
                        var url = trackerConfiguration.FtdPostbackUrl.Replace("{glm}", clickRegistration.TrackerClickId);
                        await this.httpClient.GetAsync<object>(url);
                    }

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
                    Language = "EN", //TODO HARD CODED BUT FOR NOW ITS OK since there is questioons which language to put on offer or on lead country or on something else
                    OfferId = "648d077d-dded-4ee1-b515-2d4a226262e1",
                    lead.PhoneNumber,
                    lead.Password,
                    CountryCode = lead.Country.IsoCode
                };

                var headers = new Dictionary<string, string>
                {
                    ["X-Auth-ClientId"] = "cc94150f-ca09-4950-a23d-bcea325b91f5",
                    ["X-Auth-Key"] = "ac7e4690eebc45989d690ffce24cfd5e4923f2dd4ba540e98f637fc06743d5b3"
                };

                var sendLeadResponse = await httpClient.PostAsync<ProfitPixelsSendLeadResponse>(url, requestBody, headers, "application/x-www-form-urlencoded");

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

        public class ProfitPixelsSendLeadResponse
        {
            public bool Success { get; set; }
            public string Message { get; set; }
            public string RequestId { get; set; }
            public ResponseData ResponseData { get; set; }
        }

        public class ProfitPixelsFtdScanResponse
        {
            public bool Success { get; set; }
            public string Message { get; set; }
            public string RequestId { get; set; }
            public List<ResponseData> ResponseData { get; set; }
        }

        public class ResponseData
        {
            public string LeadId { get; set; }
            public DateTime? CreatedDateTime { get; set; }
            public string RedirectUrl { get; set; }
            public string CallStatus { get; set; }
            public bool IsFtd { get; set; }
            public DateTime? FtdDateTime { get; set; }
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
