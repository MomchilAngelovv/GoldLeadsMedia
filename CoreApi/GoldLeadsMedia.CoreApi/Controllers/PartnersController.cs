using GoldLeadsMedia.CoreApi.Models.CoreApiModels;
using GoldLeadsMedia.CoreApi.Models.InputModels;
using GoldLeadsMedia.CoreApi.Models.ServiceModels;
using GoldLeadsMedia.CoreApi.Services.Application.Common;
using GoldLeadsMedia.CoreApi.Services.Partners.Common;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoldLeadsMedia.CoreApi.Controllers
{
    public class PartnersController : ApiController
    {
        private readonly IBrokersService partnersService;
        private readonly IServiceProvider serviceProvider;

        public PartnersController(
            IBrokersService brokersService,
            IServiceProvider serviceProvider)
        {
            this.partnersService = brokersService;
            this.serviceProvider = serviceProvider;
        }

        public ActionResult<IEnumerable<object>> GetAll()
        {
            var partners = this.partnersService
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
            var serviceModel = new PartnersRegisterInputServiceModel
            {
                Name = inputModel.Name
            };

            var partner = await this.partnersService.RegisterAsync(serviceModel);

            var response = new
            {
                partner.Name
            };

            return response;
        }

        [HttpPost("{brokerId}/SendLeads")]
        public async Task<ActionResult<int>> SendLeads(string brokerId, PartnersSendLeadsInputModel inputModel)
        {
            var broker = this.partnersService.GetBy(brokerId);

            if (broker == null)
            {
                //TODO LOGIC IF no partner is find -> register developer error maybe
            }

            var brokerType = typeof(IBroker)
                .Assembly
                .GetTypes()
                .SingleOrDefault(type => type.IsClass && type.IsPublic && type.Name.ToLower().StartsWith(broker.Name.ToLower()) && type.Name.EndsWith("Broker"));

            var brokerInstance = this.serviceProvider.GetService(brokerType) as IBroker;

            var errorCount = await brokerInstance.SendLeadsAsync(inputModel.LeadIds, broker.Id, inputModel.BrokerOfferId);
            return errorCount;
        }

        [HttpPost("FtdScan")]
        public async Task<ActionResult<object>> FtdScan()
        {
            var partnerTypes = typeof(IBroker)
                .Assembly
                .GetTypes()
                .Where(type => type.IsClass && type.IsPublic && type.Name.EndsWith("Broker"));

            var from = DateTime.UtcNow.AddDays(-60);
            var to = DateTime.UtcNow.AddDays(1);

            var newFtdsCounter = 0;
            foreach (var partnerType in partnerTypes)
            {
                var partnerInstance = this.serviceProvider.GetService(partnerType) as IBroker;
                var newFtds = await partnerInstance.FtdScanAsync(from, to);

                newFtdsCounter += newFtds;
            }

            var response = new
            {
                AllPartners = partnerTypes.Count(),
                NewFtds = newFtdsCounter
            };

            return response;
        }
    }
}
