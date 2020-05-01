namespace GoldLeadsMedia.Web.Controllers
{
    using System.Text.Json;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.Extensions.Configuration;

    using GoldLeadsMedia.Web.Models;
    using GoldLeadsMedia.Database.Models;
    using GoldLeadsMedia.Web.Infrastructure.HttpHelper;
    using GoldLeadsMedia.Web.Models.ViewModels;

    public class OffersController : Controller
    {
        private readonly IConfiguration configuration;
        private readonly IAsyncHttpClient httpClient;
        private readonly UserManager<GoldLeadsMediaUser> userManager;

        public OffersController(
            IConfiguration configuration,
            IAsyncHttpClient httpClient,
            UserManager<GoldLeadsMediaUser> userManager)
        {
            this.configuration = configuration;
            this.httpClient = httpClient;
            this.userManager = userManager;
        }

        [Authorize]
        public async Task<IActionResult> All()
        {
            var offers = await this.httpClient.GetAsync<IEnumerable<OffersAllOffer>>("api/offers");

            var viewModel = new OffersAllViewModel
            {
                Offers = offers
            };

            return this.View(viewModel);
        }

        public async Task<IActionResult> Details(string id)
        {
            var viewModel = await this.httpClient.GetAsync<OffersDetailsViewModel>($"Api/Offers/{id}");

            viewModel.RedirectUrl = $"https://localhost:44349/Clicks/Register?offerId={viewModel.Id}";

            return this.View(viewModel);
        }
        
        //public async Task<IActionResult> OffersByFilter(OffersFilterInputModel input)
        //{
        //    string url = string.Format("{0}api/Offer/GetOffersByFilters?", this.configuration.GetConnectionString("CoreApiUrl"));

        //    var response = await this.httpClient.PostAsync<BaseResultModel>(url, input);
        //    if (response.Code == 1)
        //    {
        //        var offerRes = JsonSerializer.Deserialize<GetOffersByFiltersResultModel>(response.Message);

        //        return PartialView("_GetOffersByFilters", offerRes);

        //    }

        //    return PartialView("_OffersByFilterPartial", new GetOffersByFiltersResultModel());
        //}
        
        //public async Task<IActionResult> SaveUserOfferTrackingSettings(string offer_Id, string postbackUrl)
        //{
        //    var loggedUser = await this.userManager.GetUserAsync(this.User);
        //    BaseResultModel returnRes = new BaseResultModel { Code = -1 };
        //    string url = string.Format("{0}api/Offer/InsertUpdateUserOfferTrackingSettings?", this.configuration.GetConnectionString("CoreApiUrl"));

        //    var req = new
        //    {
        //        Offer_Id = offer_Id,
        //        PostbackURL = postbackUrl,
        //        User_Id = loggedUser.Id
        //    };

        //    var response = await this.httpClient.PostAsync<BaseResultModel>(url, req);

        //    if (response.Code == 1)
        //    {
        //        var offerRes = JsonSerializer.Deserialize<BaseResultModel>(response.Message);

        //        returnRes = offerRes;
        //    }
        //    else
        //    {
        //        returnRes.Code = -1;
        //        returnRes.Message = "Error while saving data. Please, contact Gold Leads Media.";

        //    }
        //    return Json(returnRes);

        //}
     
        public IActionResult DashBoard()
        {
            return View();
        }
        //public async Task<IActionResult> OfferGroups()
        //{
        //    string url = string.Format("{0}api/Offer/GetOfferGroups", this.configuration.GetConnectionString("CoreApiUrl"));
        //    var response = await this.httpClient.PostAsync<BaseResultModel>(url, null);
        //    if (response.Code == 1)
        //    {
        //        //var offerRes = JsonSerializer.Deserialize<OfferGroupsViewModel>(response.Message);
        //       // return this.PartialView("_OfferGroups", offerRes);
        //    }

        //    return this.PartialView("_OfferGroups");
        //}
        //public async Task<IActionResult> OffersByGroup(int group_Id)
        //{
        //    var viewModel = new OffersByGroupViewModel();
        //    string url = string.Format("{0}api/Offer/GetOffersByGroup?", this.configuration.GetConnectionString("CoreApiUrl"));
        //    var req = new 
        //    {
        //        Group_Id = group_Id
        //    };
        //    var response = await this.httpClient.PostAsync<BaseResultModel>(url, req);
        //    if (response.Code == 1)
        //    {
        //        //var offerRes = JsonSerializer.Deserialize<GetOffersByFiltersResultModel>(response.Message);
        //        //if (offerRes.Code == 1)
        //        //{
        //        //    foreach (var offer in offerRes.Offers)
        //        //    {
        //        //        var sOffer = new OfferViewModel
        //        //        {
        //        //            Access = offer.Access,
        //        //            ActionFlow = offer.ActionFlow,
        //        //            Country = offer.Country,
        //        //            DailyCap = offer.DailyCap,
        //        //            Description = offer.Description,
        //        //            Device = offer.Device,
        //        //            EarningPerClick = offer.EarningPerClick,
        //        //            Id = offer.Id,
        //        //            Name = offer.Name,
        //        //            PaymentType = offer.PaymentType,
        //        //            Payout = offer.Payout,
        //        //            Vertical = offer.Vertical,
        //        //            Image = "HardCoded need fix asap!"
        //        //        };
        //        //        viewModel.Offers.Add(sOffer);
        //        //    }
        //        //}
        //    }
        //    return PartialView("_OffersByGroup", viewModel);
        //}
    }
}
