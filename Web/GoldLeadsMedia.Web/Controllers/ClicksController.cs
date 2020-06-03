namespace GoldLeadsMedia.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Identity;

    using GoldLeadsMedia.Database.Models;
    using GoldLeadsMedia.Web.Models.InputModels;
    using GoldLeadsMedia.Web.Infrastructure.HttpHelper;
    using GoldLeadsMedia.Web.Models.CoreApiResponses;

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

            var response = await this.httpClient.PostAsync<PostApiClicksReponse>("Api/Clicks", requestBody);
            return this.Redirect($"{response.LandingPageUrl}?clickId={response.Id}");
        }
    }
}
