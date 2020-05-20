namespace GoldLeadsMedia.PartnersApi.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using GoldLeadsMedia.PartnersApi.Services;
    using GoldLeadsMedia.PartnersApi.Models.InputModels;
    using GoldLeadsMedia.PartnersApi.Models.ServiceModels;

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

        public ActionResult<string> Get()
        {
            var greetingMessage = "Welcome to partners Api!";
            return greetingMessage;
        }

        [HttpPost]
        public async Task<ActionResult<object>> Register(LeadsRegisterInputModel inputModel)
        {
            var country = this.countriesService.GetBy(inputModel.CountryName);

            var serviceModel = new LeadsRegisterInputServiceModel
            {
                AffiliateId = inputModel.SenderId,
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
    }
}
