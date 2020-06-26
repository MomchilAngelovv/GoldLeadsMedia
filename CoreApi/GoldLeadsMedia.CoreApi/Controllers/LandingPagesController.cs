namespace GoldLeadsMedia.CoreApi.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Mvc;

    using GoldLeadsMedia.CoreApi.Models.CoreApiModels;
    using GoldLeadsMedia.CoreApi.Services.Application.Common;

    public class LandingPagesController : ApiController
    {
        private readonly ILandingPagesService landingPagesService;

        public LandingPagesController(
            ILandingPagesService landingPagesService)
        {
            this.landingPagesService = landingPagesService;
        }


        [HttpGet]
        public ActionResult<IEnumerable<object>> All()
        {
            var landingPages = this.landingPagesService
                .GetAll()
                .Select(landingPage => new 
                { 
                    landingPage.Id,
                    landingPage.Name
                })
                .ToList();

            return landingPages;
        }


        [HttpPost]
        public async Task<ActionResult<object>> Register(LandingPagesRegisterInputModel inputModel)
        {
            var landingPage = await this.landingPagesService.CreateAsync(inputModel.Name, inputModel.Url);

            var response = new
            {
                landingPage.Name
            };

            return response;
        }
    }
}
