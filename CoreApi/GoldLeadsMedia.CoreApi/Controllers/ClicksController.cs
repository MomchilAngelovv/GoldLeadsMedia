namespace GoldLeadsMedia.CoreApi.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using GoldLeadsMedia.Database.Models;
    using GoldLeadsMedia.CoreApi.Models.InputModels;
    using GoldLeadsMedia.CoreApi.Services.Application.Common;
    using GoldLeadsMedia.CoreApi.Models.ServiceModels;

    public class ClicksController : ApiController
    {
        private readonly IClicksService clicksService;

        public ClicksController(
            IClicksService clicksService)
        {
            this.clicksService = clicksService;
        }

        [HttpPost]
        public async Task<ActionResult<object>> Register(ClicksRegisterInputModel inputModel)
        {
            var ipAddress = this.HttpContext.Connection.RemoteIpAddress.ToString();

            var serviceModel = new ClicksRegisterServiceModel
            {
                OfferId = inputModel.OfferId,
                LandingPageId = inputModel.LandingPageId,
                AffiliateId = inputModel.AffiliateId,
                IpAddress = ipAddress
            };

            var click = await this.clicksService.RegisterAsync(serviceModel);

            return click;
        }
    }
}
