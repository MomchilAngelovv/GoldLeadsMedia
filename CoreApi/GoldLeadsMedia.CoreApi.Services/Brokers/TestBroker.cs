namespace GoldLeadsMedia.CoreApi.Services.Brokers
{
    using System;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using GoldLeadsMedia.CoreApi.Services.Common;

    public class TestBroker : IBroker
    {
        private readonly ILeadsService leadsService;

        private readonly string brokerId = "d928f7b9-15ce-4f3a-aa7a-1dd7ba90ca17";

        public TestBroker(
            ILeadsService leadsService)
        {
            this.leadsService = leadsService;
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
