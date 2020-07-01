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
        public ActionResult<IEnumerable<object>> GetAll()
        {
            var leads = this.leadsService
                .GetAll()
                .Select(lead => new
                {
                    lead.Id,
                    lead.FirstName,
                    lead.LastName,
                    lead.Email,
                    lead.PhoneNumber,
                    CountryName = lead.Country.Name,
                    OfferName = lead.ApiRegistrationId == null ? lead.ClickRegistration.Offer.Name : lead.ApiRegistration.Offer.Name,
                    HasBeenSend = lead.BrokerId != null,
                    HasBecomeFtd = lead.FtdBecameOn != null
                })
                .ToList();

            return leads;
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
                await this.httpClient.GetAsync<string>(url);
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

            var depositedLead = await this.leadsService.FtdSuccessAsync(lead, DateTime.UtcNow, "Deposit");

            var trackerConfiguration = this.affiliatesService.GetTrackerSettings(lead.ClickRegistration?.Affiliate.Id);
            var clickRegistration = this.clicksRegistrationsService.GetBy(lead.ClickRegistrationId);

            if (trackerConfiguration != null && string.IsNullOrWhiteSpace(trackerConfiguration.FtdPostbackUrl) == false)
            {
                var url = trackerConfiguration.FtdPostbackUrl.Replace("{glm}", clickRegistration.TrackerClickId);
                await this.httpClient.GetAsync<object>(url);
            }

            var response = new
            {
                depositedLead.Id,
                depositedLead.FtdBecameOn,
                depositedLead.CreatedOn,
                depositedLead.Status,
            };

            return response;
        }
    }
}
