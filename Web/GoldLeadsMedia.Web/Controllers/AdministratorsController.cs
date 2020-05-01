namespace GoldLeadsMedia.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Identity;

    using GoldLeadsMedia.Database.Models;
    using GoldLeadsMedia.Web.Infrastructure.HttpHelper;
    using GoldLeadsMedia.Web.Models.InputModels;

    public class AdministratorsController : Controller
    {
        private readonly IAsyncHttpClient httpClient;
        private readonly UserManager<GoldLeadsMediaUser> userManager;

        public AdministratorsController(
            IAsyncHttpClient httpClient,
            UserManager<GoldLeadsMediaUser> userManager)
        {
            this.httpClient = httpClient;
            this.userManager = userManager;
        }

        public IActionResult Information()
        {
            return this.View();
        }
        public IActionResult CreateOffer()
        {
            return this.View();
        }
        public IActionResult RegisterPartner()
        {
            return this.View();
        }
        [HttpPost]
        public async Task<IActionResult> RegisterPartner(AdministratorsRegisterPartnerInputModel inputModel)
        {
            var requestBody = new
            {
                inputModel.Name
            };

            await this.httpClient.PostAsync<string>("Api/Partners", requestBody);
            return this.Redirect("/Offers/Dashboard");
        }


        [HttpPost]
        public async Task<IActionResult> CreateOffer(AdministratorsCreateOfferInputModel inputModel)
        {
            if (this.ModelState.IsValid == false)
            {
                return this.View(inputModel);
            }

            var loggedUser = await this.userManager.GetUserAsync(this.User);

            var requestBody = new
            {
                inputModel.Number,
                inputModel.Name,
                inputModel.Description,
                inputModel.CountryId,
                inputModel.VerticalId,
                inputModel.PaymentTypeId,
                inputModel.TargetDeviceId,
                inputModel.AccessId,
                inputModel.ActionFlow,
                inputModel.PayOut,
                inputModel.DailyCap,
                inputModel.LanguageId,
                inputModel.PayPerClick,

                CreatedBy = loggedUser.Id
            };

            var response = await this.httpClient.PostAsync<Offer>("Api/Offers", requestBody);
            return this.Redirect($"~/Offers/Details/{response.Id}");
        }
        public IActionResult AssignLandingPagesToOffer()
        {
            return this.View();
        }
        [HttpPost]
        public async Task<IActionResult> AssignLandingPagesToOffer(AdministratorsAssignLandingPagesToOffersInputModel inputModel)
        {
            var reqeustBody = new
            {
                inputModel.OfferId,
                inputModel.LandingPageIds
            };

            await this.httpClient.PostAsync<int>("api/Offers/AssignLandingPages", reqeustBody);

            return this.Redirect($"/Offers/Details/{inputModel.OfferId}");
        }
    }
}
