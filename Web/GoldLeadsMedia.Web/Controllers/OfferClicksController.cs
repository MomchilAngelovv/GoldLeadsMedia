using GoldLeadsMedia.Web.Models.InputModels.OfferClicks;
namespace GoldLeadsMedia.Web.Controllers
{
    using GoldLeadsMedia.Database.Models;
    using GoldLeadsMedia.Web.Infrastructure.HttpHelper;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    public class OfferClicksController : Controller
    {
        private readonly IAsyncHttpClient httpClient;
        private readonly UserManager<GoldLeadsMediaUser> userManager;

        public OfferClicksController(
            IAsyncHttpClient httpClient,
            UserManager<GoldLeadsMediaUser> userManager)
        {
            this.httpClient = httpClient;
            this.userManager = userManager;
        }

        public async Task<IActionResult> RegisterOfferClick(RegisterOfferClickInputModel inputModel)
        {
            var loggedUser = await this.userManager.GetUserAsync(this.User);

            var reqeustBody = new
            {
                inputModel.OfferId,
                inputModel.LandingPageId,
                UserId = loggedUser.Id
            };

            var response = await this.httpClient.PostAsync<int>("api/offerclicks", reqeustBody);
            return this.Json(response);
        }
    }
}
