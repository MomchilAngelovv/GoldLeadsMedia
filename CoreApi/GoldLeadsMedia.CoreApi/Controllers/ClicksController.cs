namespace GoldLeadsMedia.CoreApi.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using GoldLeadsMedia.Database.Models;
    using GoldLeadsMedia.CoreApi.Models.InputModels;
    using GoldLeadsMedia.CoreApi.Services.Application.Common;
    using GoldLeadsMedia.CoreApi.Models.ServiceModels;
    using GoldLeadsMedia.Database;
    using Microsoft.AspNetCore.Builder;

    public class ClicksController : ApiController
    {
        private readonly IClicksService clicksService;
        private readonly GoldLeadsMediaDbContext db;

        public ClicksController(
            IClicksService clicksService, GoldLeadsMediaDbContext db)
        {
            this.clicksService = clicksService;
            this.db = db;
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

            var response = new
            {
                click.Id
            };

            return response;
        }

        [HttpPost("test")]
        public async Task<ActionResult<string>> Test()
        {
            var testError = new DeveloperError
            {
                Message = "From worker"
            };

            await this.db.DeveloperErrors.AddAsync(testError);
            await this.db.SaveChangesAsync();

            return testError.Id;
        }
    }
}
