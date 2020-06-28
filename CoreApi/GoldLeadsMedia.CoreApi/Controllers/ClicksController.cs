namespace GoldLeadsMedia.CoreApi.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using GoldLeadsMedia.CoreApi.Services.AsyncHttpClient;
    using GoldLeadsMedia.CoreApi.Models.CoreApi.Input;
    using GoldLeadsMedia.CoreApi.Models.Services.Input;
    using GoldLeadsMedia.CoreApi.Services.Common;

    public class ClicksController : ApiController
    {
        private readonly IClicksRegistrationsService clicksService;
        private readonly ILandingPagesService landingPagesService;
        private readonly IAffiliatesService affiliatesService;
        private readonly IAsyncHttpClient httpClient;

        public ClicksController(
            IClicksRegistrationsService clicksService, 
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
