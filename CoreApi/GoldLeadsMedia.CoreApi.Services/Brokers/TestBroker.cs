namespace GoldLeadsMedia.CoreApi.Services.Brokers
{
    using System;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using GoldLeadsMedia.CoreApi.Services.Common;
    using GoldLeadsMedia.CoreApi.Services.AsyncHttpClient;

    public class TestBroker : IBroker
    {
        private readonly ILeadsService leadsService;
        private readonly IAffiliatesService affiliatesService;
        private readonly IClicksRegistrationsService clicksRegistrationsService;
        private readonly IAsyncHttpClient httpClient;

        private readonly string brokerId = "e15e3346-dfa0-477d-ae5c-c68987a6354c";
        
        public TestBroker(
            ILeadsService leadsService,
            IAffiliatesService affiliatesService,
            IClicksRegistrationsService clicksRegistrationsService,
            IAsyncHttpClient httpClient)
        {
            this.leadsService = leadsService;
            this.affiliatesService = affiliatesService;
            this.clicksRegistrationsService = clicksRegistrationsService;
            this.httpClient = httpClient;
        }

        public async Task<int> FtdScanAsync(DateTime from, DateTime to)
        {
            return 0;
        }
        public async Task<int> SendLeadsAsync(IEnumerable<string> leadIds)
        {
            var failedLeadsCount = 0;

            foreach (var leadId in leadIds)
            {
                var lead = leadsService.GetBy(leadId);
                await leadsService.SendLeadSuccessAsync(lead, this.brokerId, "IdInTestBroker");
            }

            return failedLeadsCount;
        }
    }
}
