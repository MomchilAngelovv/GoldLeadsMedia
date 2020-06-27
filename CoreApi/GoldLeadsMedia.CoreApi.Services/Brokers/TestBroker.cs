using GoldLeadsMedia.CoreApi.Services.Application.Common;
using GoldLeadsMedia.CoreApi.Services.Partners.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GoldLeadsMedia.CoreApi.Services.Brokers
{
    public class TestBroker : IBroker
    {
        private readonly ILeadsService leadsService;

        private readonly string brokerId = "6ad9f23a-83ed-4532-ad8d-52f27485f2e5";

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
