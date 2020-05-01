namespace GoldLeadsMedia.Web.Controllers
{
    using GoldLeadsMedia.Database.Models;
    using GoldLeadsMedia.Web.Infrastructure.HttpHelper;
    using GoldLeadsMedia.Web.Models.InputModels;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    public class ClicksController : Controller
    {
        private readonly IAsyncHttpClient httpClient;
        private readonly UserManager<GoldLeadsMediaUser> userManager;

        public ClicksController(
            IAsyncHttpClient httpClient,
            UserManager<GoldLeadsMediaUser> userManager)
        {
            this.httpClient = httpClient;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Register(ClicksRegisterInputModel inputModel)
        {
            var loggedUser = await this.userManager.GetUserAsync(this.User);

            var requestBody = new
            {
                inputModel.OfferId,
                inputModel.LandingPageId,
                AffiliateId = loggedUser.Id
            };

            var clickId = await this.httpClient.PostAsync<string>("Api/Clicks", requestBody);
            return this.Redirect($"/Offers/Dashboard?clickId={clickId}");
        }
    }
}
