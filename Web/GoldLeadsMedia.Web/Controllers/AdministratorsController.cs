namespace GoldLeadsMedia.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;

    using GoldLeadsMedia.Database.Models;
    using GoldLeadsMedia.Web.Models.InputModels;
    using GoldLeadsMedia.Web.Models.CoreApiResponses;
    using GoldLeadsMedia.Web.Infrastructure.HttpHelper;
    using Microsoft.EntityFrameworkCore.Metadata.Internal;
    using System.Collections.Generic;

    public class AdministratorsController : Controller
    {
        private readonly IAsyncHttpClient httpClient;
        private readonly UserManager<GoldLeadsMediaUser> userManager;
        private readonly IWebHostEnvironment hostEnvironment;

        public AdministratorsController(
            IAsyncHttpClient httpClient,
            UserManager<GoldLeadsMediaUser> userManager, 
            IWebHostEnvironment hostEnvironment)
        {
            this.httpClient = httpClient;
            this.userManager = userManager;
            this.hostEnvironment = hostEnvironment;
        }

        public IActionResult Information()
        {
            return this.View();
        }
        public IActionResult CreateOffer()
        {
            return this.View();
        }
        public IActionResult RegisterBroker()
        {
            return this.View();
        }
        public IActionResult AssignLandingPagesToOffer()
        {
            return this.View();
        }
        public IActionResult RegisterAffiliate()
        {
            return this.View();
        }
        public IActionResult RegisterLandingPage()
        {
            return this.View();
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
                inputModel.TierCountryId,
                inputModel.VerticalId,
                inputModel.PayTypeId,
                inputModel.TargetDeviceId,
                inputModel.AccessId,
                inputModel.ActionFlow,
                inputModel.PayPerAction,
                inputModel.PayPerLead,
                inputModel.PayPerClick,
                inputModel.LanguageId,
                CreatedByManagerId = loggedUser.Id
            };

            var response = await this.httpClient.PostAsync<Offer>("Api/Offers", requestBody);

            return this.Redirect($"~/Offers/Details/{response.Id}");
        }
        [HttpPost]
        public async Task<IActionResult> RegisterBroker(AdministratorsRegisterBrokerInputModel inputModel)
        {
            var requestBody = new
            {
                inputModel.Name
            };

            await this.httpClient.PostAsync<PostApiPartnersResponse>("Api/Brokers", requestBody);
            return this.Redirect("/Offers/Dashboard");
        }
        [HttpPost]
        public async Task<IActionResult> AssignLandingPagesToOffer(AdministratorsAssignLandingPagesToOffersInputModel inputModel)
        {
            if (this.ModelState.IsValid == false)
            {
                this.TempData["NullOffer"] = "Please select offer!";
                return this.Redirect("/Administrators/AssignLandingPagesToOffer");
            }

            if (inputModel.LandingPageIds == null)
            {
                inputModel.LandingPageIds = new List<string>();
            }

            var requestBody = new
            {
                inputModel.OfferId,
                inputModel.LandingPageIds
            };

            await this.httpClient.PostAsync<int>("Api/Offers/AssignLandingPages", requestBody);

            return this.Redirect($"/Offers/Details/{inputModel.OfferId}");
        }
        [HttpPost]
        public async Task<IActionResult> RegisterAffiliate(AdministratorsRegisterAffiliateInputModel inputModel)
        {
            var loggedUser = await this.userManager.GetUserAsync(this.User);

            var affiliate = new GoldLeadsMediaUser
            {
                UserName = inputModel.UserName,
                Email = inputModel.Email,
                ManagerId = loggedUser.Id
            };
           
            await this.userManager.CreateAsync(affiliate, inputModel.Password);
            await this.userManager.AddToRoleAsync(affiliate, "Affiliate");
            return this.Redirect("/Managers/Affiliates");
        }
        [HttpPost]
        public async Task<IActionResult> RegisterLandingPage(AdministratorsRegisterLandingPageInputModel inputModel)
        {
            var requestBody = new
            {
                inputModel.Name,
                inputModel.Url
            };

            var landingPage = await this.httpClient.PostAsync<PostApiLandingPages>("Api/LandingPages", requestBody);

            return this.Redirect($"/Offers/Dashboard");
        }
    }
}
