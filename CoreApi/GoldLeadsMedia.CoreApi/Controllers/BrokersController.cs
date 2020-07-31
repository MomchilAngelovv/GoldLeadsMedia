namespace GoldLeadsMedia.CoreApi.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Mvc;
    using GoldLeadsMedia.CoreApi.Models.CoreApi.Input;
    using GoldLeadsMedia.CoreApi.Models.Services.Input;
    using GoldLeadsMedia.CoreApi.Services.Common;
    using GoldLeadsMedia.CoreApi.Common;

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
        [HttpGet("{id}")]
        public ActionResult<object> Details(string id)
        {
            var broker = this.brokersService.GetBy(id);
            if (broker == null)
            {
                return this.BadRequest(ErrorConstants.BrokerNotFound);
            }

            var response = new
            {
                broker.Id,
                broker.Name,
                Leads = broker.Leads.Select(lead => new
                {
                    lead.Id,
                    lead.Email,
                    lead.FirstName,
                    lead.LastName,
                    lead.Status,
                    HasBeenSend = lead.BrokerId != null,
                    HasBecomeFtd = lead.FtdBecameOn != null
                }),
                CreatedOn = broker.CreatedOn.ToString(),
                DeletedOn = broker.DeletedOn.ToString(),
                broker.Information
            };

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
                return this.BadRequest(ErrorConstants.BrokerNotFound);
            }

            var brokerType = typeof(IBroker)
                .Assembly
                .GetTypes()
                .SingleOrDefault(type => type.IsClass && type.IsPublic && type.Name.ToLower().StartsWith(broker.Name.ToLower().Replace(" ", string.Empty)) && type.Name.EndsWith("Broker"));

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
                .Where(type => type.IsClass && type.IsPublic && type.Name.EndsWith("Broker"))
                .ToList();

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
                ScannedBrokers = brokerTypes.Count,
                Ftds = ftdCounter
            };

            return response;
        }
    }
}
