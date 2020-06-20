namespace GoldLeadsMedia.CoreApi.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using GoldLeadsMedia.CoreApi.Common;
    using GoldLeadsMedia.CoreApi.Models.InputModels;
    using GoldLeadsMedia.CoreApi.Models.ServiceModels;
    using GoldLeadsMedia.CoreApi.Services.Application.Common;
    using GoldLeadsMedia.CoreApi.Services.AsyncHttpClient;

    public class LeadsController : ApiController
    {
        private readonly ILeadsService leadsService;
        private readonly ICountriesService countriesService;
        private readonly IAffiliatesService affiliatesService;
        private readonly IAsyncHttpClient httpClient;
        private readonly IClicksRegistrationsService clicksRegistrationsService;

        public LeadsController(
            ILeadsService leadsService,
            ICountriesService countriesService,
            IAffiliatesService affiliatesService,
            IAsyncHttpClient httpClient,
            IClicksRegistrationsService clicksRegistrationsService)
        {
            this.leadsService = leadsService;
            this.countriesService = countriesService;
            this.affiliatesService = affiliatesService;
            this.httpClient = httpClient;
            this.clicksRegistrationsService = clicksRegistrationsService;
        }

        [HttpPost]
        public async Task<ActionResult<object>> Register(LeadsRegisterInputModel inputModel)
        {
            var country = this.countriesService.GetBy(inputModel.CountryName);

            if (country == null)
            {
                return this.BadRequest(ErrorConstants.InvalidCountryName);
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

            var lead = await this.leadsService.RegisterAsync(serviceModel);

            //We get the clickRegistration and trackerConfiguration in order to make Get request at the lead postback url to register the lead into affiliate tracker
            var clickRegistration = this.clicksRegistrationsService.GetBy(inputModel.ClickRegistrationId);
            var trackerConfiguration = this.affiliatesService.GetTrackerSettings(clickRegistration.AffiliateId);

            if (trackerConfiguration != null && string.IsNullOrWhiteSpace(trackerConfiguration.LeadPostbackUrl) == false)
            {
                var url = trackerConfiguration.LeadPostbackUrl.Replace("{glm}", clickRegistration.AffiliateTrackerClickId);
                await this.httpClient.GetAsync<string>(url);
            }

            return lead;
        }
    }
}
