namespace GoldLeadsMedia.CoreApi.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using GoldLeadsMedia.CoreApi.Models.InputModels;
    using GoldLeadsMedia.CoreApi.Models.ServiceModels;
    using GoldLeadsMedia.CoreApi.Services.Application.Common;
    using GoldLeadsMedia.CoreApi.Services.AsyncHttpClient;

    public class ClicksController : ApiController
    {
        private readonly IClicksService clicksService;
        private readonly ILandingPagesService landingPagesService;
        private readonly IAffiliatesService affiliatesService;
        private readonly IAsyncHttpClient httpClient;

        public ClicksController(
            IClicksService clicksService, 
            ILandingPagesService landingPagesService,
            IAffiliatesService affiliatesService,
            IAsyncHttpClient httpClient)
        {
            this.clicksService = clicksService;
            this.landingPagesService = landingPagesService;
            this.affiliatesService = affiliatesService;
            this.httpClient = httpClient;
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
                AffiliateTrackerClickId = inputModel.AffiliateTrackerClickId,
                IpAddress = ipAddress
            };

            var clickRegistration = await this.clicksService.RegisterAsync(serviceModel);
           
            var landingPage = this.landingPagesService.GetBy(clickRegistration.LandingPageId);

            var response = new
            {
                clickRegistration.Id,
                LandingPageUrl = landingPage.Url
            };

            return response;
        }
    }
}
