namespace GoldLeadsMedia.CoreApi.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using GoldLeadsMedia.CoreApi.Common;
    using GoldLeadsMedia.CoreApi.Services.AsyncHttpClient;
    using System.Collections.Generic;
    using System.Linq;
    using Castle.DynamicProxy.Generators.Emitters;
    using System;
    using GoldLeadsMedia.CoreApi.Models.CoreApi.Input;
    using GoldLeadsMedia.CoreApi.Services.Common;
    using GoldLeadsMedia.CoreApi.Models.Services.Input;

    public class LeadsController : ApiController
    {
        private readonly ILeadsService leadsService;
        private readonly ICountriesService countriesService;
        private readonly IAffiliatesService affiliatesService;
        private readonly IAsyncHttpClient httpClient;
        private readonly IClicksRegistrationsService clicksRegistrationsService;
        private readonly IBrokersService brokersService;

        public LeadsController(
            ILeadsService leadsService,
            ICountriesService countriesService,
            IAffiliatesService affiliatesService,
            IAsyncHttpClient httpClient,
            IClicksRegistrationsService clicksRegistrationsService,
            IBrokersService brokersService)
        {
            this.leadsService = leadsService;
            this.countriesService = countriesService;
            this.affiliatesService = affiliatesService;
            this.httpClient = httpClient;
            this.clicksRegistrationsService = clicksRegistrationsService;
            this.brokersService = brokersService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<object>> GetAll([FromQuery] int page)
        {
            var leads = this.leadsService
                .GetAll()
                .Select(lead => new
                {
                    lead.Id,
                    Affiliate = lead.ApiRegistrationId == null ? lead.ClickRegistration.Affiliate.UserName : lead.ApiRegistration.Affiliate.UserName,
                    lead.FirstName,
                    lead.LastName,
                    lead.Email,
                    lead.PhoneNumber,
                    CountryName = lead.Country.Name,
                    OfferName = lead.ApiRegistrationId == null ? lead.ClickRegistration.Offer.Name : lead.ApiRegistration.Offer.Name,
                    HasBeenSend = lead.BrokerId != null,
                    HasBecomeFtd = lead.FtdBecameOn != null
                })
                .OrderByDescending(lead => lead.HasBecomeFtd)
                .ThenByDescending(lead => lead.HasBeenSend)
                .Skip((page - 1) * 25)
                .Take(25)
                .ToList();

            return leads;
        }
        [HttpGet("{id}")]
        public ActionResult<object> Details(string id)
        {
            var lead = this.leadsService.GetBy(id);

            if (lead == null)
            {
                return this.BadRequest(ErrorConstants.LeadNotFound);
            }

            var response = new
            {
                lead.Id,
                lead.FirstName,
                lead.LastName,
                lead.Email,
                lead.PhoneNumber,
                IsTest = lead.IsTest == true ? "Test" : "Real",
                lead.IdInBroker,
                lead.Status,
                FtdBecameOn = lead.FtdBecameOn.ToString(),
                lead.DepositAmmount,
                CreatedOn = lead.CreatedOn.ToString(),
                lead.ClickRegistrationId,
                lead.ApiRegistrationId,
                Country = lead.Country.Name,
                Broker = lead.Broker?.Name,
                UpdatedOn = lead.UpdatedOn.GetValueOrDefault().ToString()
            };

            return response;
        }


        [HttpPost]
        public async Task<ActionResult<object>> Register(LeadsRegisterInputModel inputModel)
        {
            var clickRegistration = this.clicksRegistrationsService.GetBy(inputModel.ClickRegistrationId);
            if (clickRegistration == null)
            {
                return this.BadRequest(ErrorConstants.ClickRegistrationNotFound);
            }

            var country = this.countriesService.GetBy(inputModel.CountryName);
            if (country == null)
            {
                return this.BadRequest(ErrorConstants.CountryNotFound);
            }

            var lead = this.leadsService.GetByEmail(inputModel.Email);
            if (lead != null)
            {
                return BadRequest(ErrorConstants.LeadAlreadyExists);
            }

            var serviceModel = new LeadsRegisterInputServiceModel
            {
                FirstName = inputModel.FirstName,
                LastName = inputModel.LastName,
                Password = inputModel.Password,
                Email = inputModel.Email,
                PhoneNumber = inputModel.PhoneNumber,
                CountryId = country.Id,

                ClickRegistrationId = inputModel.ClickRegistrationId,
            };

            //Register lead in our database
            var newLead = await this.leadsService.RegisterAsync(serviceModel);

            //Register lead in affiliate tracker
            var trackerConfiguration = this.affiliatesService.GetTrackerSettings(clickRegistration.AffiliateId);

            if (trackerConfiguration != null && string.IsNullOrWhiteSpace(trackerConfiguration.LeadPostbackUrl) == false)
            {
                var url = trackerConfiguration.LeadPostbackUrl.Replace("{glm}", clickRegistration.TrackerClickId);
                await this.httpClient.GetAsync(url);
            }

            return newLead;
        }
        [HttpPost("{leadId}/Deposit")]
        public async Task<ActionResult<object>> Deposit(string leadId)
        {
            var lead = this.leadsService.GetBy(leadId);

            if (lead == null)
            {
                return this.BadRequest(ErrorConstants.LeadNotFound);
            }
            if (lead.FtdBecameOn != null)
            {
                return this.BadRequest(ErrorConstants.LeadAlreadyDeposited);
            }
            if (lead.BrokerId == null)
            {
                return this.BadRequest(ErrorConstants.CannotDepositLeadBeforeItIsSend);
            }
            if (lead.Broker.Name != "TestBroker")
            {
                return this.BadRequest(ErrorConstants.CannotDepositLeadInRealBroker);
            }

            var depositedLead = await this.leadsService.FtdSuccessAsync(lead, DateTime.UtcNow, "Deposit");

            var trackerConfiguration = this.affiliatesService.GetTrackerSettings(lead.ClickRegistration?.Affiliate.Id);
            var clickRegistration = this.clicksRegistrationsService.GetBy(lead.ClickRegistrationId);

            if (trackerConfiguration != null && string.IsNullOrWhiteSpace(trackerConfiguration.FtdPostbackUrl) == false)
            {
                var url = trackerConfiguration.FtdPostbackUrl.Replace("{glm}", clickRegistration.TrackerClickId);
                await this.httpClient.GetAsync(url);
            }

            var response = new
            {
                depositedLead.Id,
                FtdBecameOn = depositedLead.FtdBecameOn.ToString(),
                CreatedOn = depositedLead.CreatedOn.ToString(),
                depositedLead.Status,
            };

            return response;
        }
        [HttpPost("{leadId}/SetTest")]
        public async Task<ActionResult<object>> SetTest(string leadId)
        {
            var lead = this.leadsService.GetBy(leadId);
            if (lead == null)
            {
                return this.BadRequest(ErrorConstants.LeadNotFound);
            }

            var leadToSetTest = await this.leadsService.SetTestAsync(leadId);
            var response = new
            {
                leadToSetTest.Id
            };

            return response;
        }
    }
}
