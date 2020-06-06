namespace GoldLeadsMedia.CoreApi.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using GoldLeadsMedia.CoreApi.Models.InputModels;
    using GoldLeadsMedia.CoreApi.Models.ServiceModels;
    using GoldLeadsMedia.CoreApi.Services.Application.Common;

    public class ClicksController : ApiController
    {
        private readonly IClicksService clicksService;
        private readonly ILandingPagesService landingPagesService;

        public ClicksController(
            IClicksService clicksService, 
            ILandingPagesService landingPagesService)
        {
            this.clicksService = clicksService;
            this.landingPagesService = landingPagesService;
        }

        [HttpPost]
        public async Task<ActionResult<object>> Register(ClicksRegisterInputModel inputModel)
        {
            var ipAddress = this.HttpContext.Connection.RemoteIpAddress.ToString();

            var serviceModel = new ClicksRegisterInputServiceModel
            {
                OfferId = inputModel.OfferId,
                LandingPageId = inputModel.LandingPageId,
                AffiliateId = inputModel.AffiliateId,
                IpAddress = ipAddress
            };

            var click = await this.clicksService.RegisterAsync(serviceModel);
            var landingPage = this.landingPagesService.GetBy(click.LandingPageId);

            var response = new
            {
                click.Id,
                LandingPageUrl = landingPage.Url
            };

            return response;
        }
    }
}
