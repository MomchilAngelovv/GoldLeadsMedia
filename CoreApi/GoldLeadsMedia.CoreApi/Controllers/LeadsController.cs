namespace GoldLeadsMedia.CoreApi.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using GoldLeadsMedia.CoreApi.Common;
    using GoldLeadsMedia.CoreApi.Models.InputModels;
    using GoldLeadsMedia.CoreApi.Models.ServiceModels;
    using GoldLeadsMedia.CoreApi.Services.Application.Common;

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

                ClickRegistrationId = inputModel.ClickId,
            };

            var lead = await this.leadsService.RegisterAsync(serviceModel);

            //MAKE GET REQUEST TO FINISH s2s
            return lead;
        }
    }
}
