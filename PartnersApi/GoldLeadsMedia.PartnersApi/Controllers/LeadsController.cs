namespace GoldLeadsMedia.PartnersApi.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using GoldLeadsMedia.PartnersApi.Services;
    using GoldLeadsMedia.PartnersApi.Models.InputModels;
    using GoldLeadsMedia.PartnersApi.Models.ServiceModels;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public class LeadsController : ApiController
    {
        private readonly ILeadsService leadsService;
        private readonly ICountriesService countriesService;

        public LeadsController(
            ILeadsService leadsService,
            ICountriesService countriesService)
        {
            this.leadsService = leadsService;
            this.countriesService = countriesService;
        }

        [HttpGet("Welcome")]
        public ActionResult<string> Get()
        {
            var greetingMessage = "Welcome to partners Api!";
            return greetingMessage;
        }

        [HttpPost]
        public async Task<ActionResult<object>> Register(LeadsRegisterInputModel inputModel)
        {
            var country = this.countriesService.GetBy(inputModel.CountryName);

            if (country == null)
            {
                return this.BadRequest("Invalid country name! Make sure to provide correct country name!");
            }

            var serviceModel = new LeadsRegisterInputServiceModel
            {
                AffiliateId = inputModel.UserId,
                OfferId = inputModel.OfferId,

                FirstName = inputModel.FirstName,
                LastName = inputModel.LastName,
                Email = inputModel.Email,
                PhoneNumber = inputModel.PhoneNumber,
                CountryId = country.Id
            };

            var lead = await this.leadsService.RegisterAsync(serviceModel);

            var response = new
            {
                lead.Id,
            };

            return response;
        }

        [HttpGet("{userId}")]
        public ActionResult<object> LeadsByAffiliateId(string userId)
        {
            var leads = this.leadsService.GetAllBy(userId);

            var response = new
            {
                Leads = leads
            };

            return response;
        }
    }
}
