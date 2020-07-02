namespace GoldLeadsMedia.Web.Controllers
{
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Identity;

    using GoldLeadsMedia.Web.Common;
    using GoldLeadsMedia.Database.Models;
    using GoldLeadsMedia.Web.Models.ViewModels;
    using GoldLeadsMedia.Web.Models.InputModels;
    using GoldLeadsMedia.Web.Models.CoreApiResponses;
    using GoldLeadsMedia.Web.Infrastructure.HttpHelper;

    public class AdministratorsController : Controller
    {
        private readonly IAsyncHttpClient httpClient;
        private readonly UserManager<GoldLeadsMediaUser> userManager;

        public AdministratorsController(
            UserManager<GoldLeadsMediaUser> userManager, 
            IAsyncHttpClient httpClient)
        {
            this.httpClient = httpClient;
            this.userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Information()
        {
            var developerErrors = await this.httpClient.GetAsync<List<AdministratorsInformationDeveloperError>>("Errors/Developer");
            var sendLeads = await this.httpClient.GetAsync<List<AdministratorsInformationSendLeadError>>("Errors/SendLead");
            var ftdScanErrors = await this.httpClient.GetAsync<List<AdministratorsInformationFtdScanError>>("Errors/FtdScan");

            var viewModel = new AdministratorsInformationViewModel
            {
                DeveloperErrors = developerErrors,
                SendLeadErrors = sendLeads,
                FtdScanErrors = ftdScanErrors,
                DeveloperErrorsCount = developerErrors.Count,
                SendLeadsErrorsCount = sendLeads.Count,
                FtdScanErrorsCount = ftdScanErrors.Count,
            };

            return this.View(viewModel);
        }
        [HttpGet]
        public IActionResult CreateOffer()
        {
            return this.View();
        }
        [HttpGet]
        public IActionResult RegisterBroker()
        {
            return this.View();
        }
        [HttpGet]
        public IActionResult AssignLandingPagesToOffer()
        {
            return this.View();
        }
        [HttpGet]
        public IActionResult RegisterAffiliate()
        {
            return this.View();
        }
        [HttpGet]
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
                inputModel.CountryTierId,
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

            var response = await this.httpClient.PostAsync<Offer>("Offers", requestBody);

            return this.Redirect($"~/Offers/Details/{response.Id}");
        }
        [HttpPost]
        public async Task<IActionResult> RegisterBroker(AdministratorsRegisterBrokerInputModel inputModel)
        {
            if (this.ModelState.IsValid == false)
            {
                return this.View(inputModel);
            }

            var requestBody = new
            {
                inputModel.Name
            };

            await this.httpClient.PostAsync<PostApiPartnersResponse>("Brokers", requestBody);
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

            await this.httpClient.PostAsync<int>("Offers/AssignLandingPages", requestBody);

            return this.Redirect($"/Offers/Details/{inputModel.OfferId}");
        }
        [HttpPost]
        public async Task<IActionResult> RegisterAffiliate(AdministratorsRegisterAffiliateInputModel inputModel)
        {
            var loggedUser = await this.userManager.GetUserAsync(this.User);

            var affiliate = await this.userManager.FindByEmailAsync(inputModel.Email);
            if (affiliate != null)
            {
                return this.BadRequest(ErrorConstants.AffiliateAlreadyExists);
            }

            var newAffiliate = new GoldLeadsMediaUser
            {
                UserName = inputModel.UserName,
                Email = inputModel.Email,
                ManagerId = loggedUser.Id
            };
           
            await this.userManager.CreateAsync(newAffiliate, inputModel.Password);
            await this.userManager.AddToRoleAsync(newAffiliate, "Affiliate");

            return this.Redirect("/Affiliates/All");
        }
        [HttpPost]
        public async Task<IActionResult> RegisterLandingPage(AdministratorsRegisterLandingPageInputModel inputModel)
        {
            if (this.ModelState.IsValid == false)
            {
                return this.View(inputModel);
            }

            var requestBody = new
            {
                inputModel.Name,
                inputModel.Url
            };

            var landingPage = await this.httpClient.PostAsync<PostApiLandingPages>("LandingPages", requestBody);

            return this.Redirect($"/Offers/Dashboard");
        }
    }
}
