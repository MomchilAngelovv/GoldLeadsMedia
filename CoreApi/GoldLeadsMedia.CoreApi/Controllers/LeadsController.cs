namespace GoldLeadsMedia.CoreApi.Controllers
{
    using System.Linq;
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Mvc;
    using GoldLeadsMedia.CoreApi.Services.Application.Common;
    using GoldLeadsMedia.CoreApi.Models.InputModels;
    using GoldLeadsMedia.CoreApi.Models.ServiceModels;
    using System.Threading.Tasks;

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

            var serviceModel = new LeadsRegisterInputServiceModel
            {
                FirstName = inputModel.FirstName,
                LastName = inputModel.LastName,
                Password = inputModel.Password,
                Email = inputModel.Email,
                PhoneNumber = inputModel.PhoneNumber,
                CountryId = country.Id,
                ClickId = inputModel.ClickId
            };

            var lead = await this.leadsService.RegisterAsync(serviceModel);

            return this.Created($"/Api/Leads/{lead.Id}", lead);
        }
    }
}
