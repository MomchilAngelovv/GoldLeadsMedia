namespace GoldLeadsMedia.CoreApi.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Mvc;
    using GoldLeadsMedia.CoreApi.Services.Partners.Common;
    using GoldLeadsMedia.CoreApi.Services.Application.Common;
    using GoldLeadsMedia.CoreApi.Models.CoreApi.Input;
    using GoldLeadsMedia.CoreApi.Models.Services.Input;

    public class BrokersController : ApiController
    {
        private readonly IBrokersService brokersService;
        private readonly IServiceProvider serviceProvider;

        public BrokersController(
            IBrokersService brokersService,
            IServiceProvider serviceProvider)
        {
            this.brokersService = brokersService;
            this.serviceProvider = serviceProvider;
        }

        [HttpGet]
        public ActionResult<IEnumerable<object>> GetAll()
        {
            var partners = this.brokersService
                .GetAll();

            var response = partners
                .Select(partner => new
                {
                    partner.Id,
                    partner.Name
                })
                .ToList();

            return response;
        }


        [HttpPost]
        public async Task<ActionResult<object>> Register(PartnersRegisterInputModel inputModel)
        {
            var serviceModel = new BrokersRegisterInputServiceModel
            {
                Name = inputModel.Name
            };

            var partner = await this.brokersService.RegisterAsync(serviceModel);

            var response = new
            {
                partner.Name
            };

            return response;
        }
        [HttpPost("{brokerId}/SendLeads")]
        public async Task<ActionResult<object>> SendLeads(string brokerId, PartnersSendLeadsInputModel inputModel)
        {
            var broker = this.brokersService.GetBy(brokerId);

            if (broker == null)
            {
                //TODO LOGIC IF no partner is find -> register developer error maybe
            }

            var brokerType = typeof(IBroker)
                .Assembly
                .GetTypes()
                .SingleOrDefault(type => type.IsClass && type.IsPublic && type.Name.ToLower().StartsWith(broker.Name.ToLower().Replace(" ","")) && type.Name.EndsWith("Broker"));

            var brokerInstance = this.serviceProvider.GetService(brokerType) as IBroker;

            var errorCount = await brokerInstance.SendLeadsAsync(inputModel.LeadIds);

            var response = new
            {
                Errors = errorCount
            };

            return response;
        }
        [HttpPost("FtdScan")]
        public async Task<ActionResult<object>> FtdScan()
        {
            var brokerTypes = typeof(IBroker)
                .Assembly
                .GetTypes()
                .Where(type => type.IsClass && type.IsPublic && type.Name.EndsWith("Broker"));

            //TODO: Think about moving settings in database table or anything else (now it is hardcored to scan for last 2 months)
            var from = DateTime.UtcNow.AddDays(-60);
            var to = DateTime.UtcNow.AddDays(1);

            var ftdCounter = 0;
            foreach (var brokerType in brokerTypes)
            {
                var partnerInstance = this.serviceProvider.GetService(brokerType) as IBroker;
                var newFtds = await partnerInstance.FtdScanAsync(from, to);

                ftdCounter += newFtds;
            }

            var response = new
            {
                ScannedBrokers = brokerTypes.Count(),
                Ftds = ftdCounter
            };

            return response;
        }
    }
}
