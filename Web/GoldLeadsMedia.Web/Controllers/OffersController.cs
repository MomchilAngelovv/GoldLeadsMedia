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
            var viewModel = await this.httpClient.GetAsync<OffersDetailsViewModel>($"api/offers/{id}");
            var loggedUser = await this.userManager.GetUserAsync(this.User);

            viewModel.RedirectUrl = $"https://my.goldleadsmedia.com/offer/load?u={loggedUser.Id}&o={viewModel.Id}";

            return this.View(viewModel);
        }
        //public async Task<IActionResult> GetFiltersJSON(string model)
        //{
        //    //var combinedModel = new CombinedFilterModel() { };
        //    //string url = string.Format("{0}api/Offer/GetOffersFilters?", this.configuration.GetConnectionString("CoreApiUrl"));

        //    //var response = await this.httpClient.PostAsync<BaseResultModel>(url, model);
        //    //if (response.Code == 1)
        //    //{
        //    //    var offerRes = JsonSerializer.Deserialize<GetFilterResultModel>(response.Message);
        //    //    if (offerRes.Code == 1)
        //    //    {
        //    //        combinedModel.Filter = offerRes;
        //    //        combinedModel.BindingFilter = model;
        //    //    }
        //    //}


        //    return this.Json(2);
        //}
        //public ActionResult GetAdditivesFilters(GetAdditivesFiltersModel model)
        //{
        //    TempData["promo"] = "";
        //    model.ShowSpecialItems = ShowSpecialItems();
        //    var filters = new FilterUniversalProducts();
        //    using (EShopWSClient client = new EShopWSClient())
        //    {
        //        client.GetUniversalAdditivesFilters(model, out filters);
        //    }
        //    var combinedModel = new CombinedAdditiveModel() { Filter = filters, BindingFilter = model };
        //    return PartialView("_GetAdditivesFilters", combinedModel);
        //}
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
        //public async Task<IActionResult> Details(string id)
        //{
        //    var loggedUser = await this.userManager.GetUserAsync(this.User);
        //    var offerModel = new OfferViewModel();
        //    offerModel.User_Id = loggedUser.Id;
        //    string url = string.Format("{0}api/Offer/GetOffersByFilters?", this.configuration.GetConnectionString("CoreApiUrl"));
        //    var req = new GetOffersByFiltersBindingModel() { IdOrName = id?.ToString(), IsById = 1, User_Id = offerModel.User_Id };
        //    var response = await this.httpClient.PostAsync<BaseResultModel>(url, req);

        //    if (response.Code == 1)
        //    {
        //        var offerRes = JsonSerializer.Deserialize<GetOffersByFiltersResultModel>(response.Message);
        //        if (offerRes.Code == 1)
        //        {
        //            //offerModel.Access = offerRes.Offers.FirstOrDefault().Access;
        //            //offerModel.ActionFlow = offerRes.Offers.FirstOrDefault().ActionFlow;
        //            //offerModel.Country = offerRes.Offers.FirstOrDefault().Country;
        //            //offerModel.DailyCap = offerRes.Offers.FirstOrDefault().DailyCap;
        //            //offerModel.Description = offerRes.Offers.FirstOrDefault().Description;
        //            //offerModel.Device = offerRes.Offers.FirstOrDefault().Device;
        //            //offerModel.Id = offerRes.Offers.FirstOrDefault().Id;
        //            //offerModel.Name = offerRes.Offers.FirstOrDefault().Name;
        //            //offerModel.PaymentType = offerRes.Offers.FirstOrDefault().PaymentType;
        //            //offerModel.Payout = offerRes.Offers.FirstOrDefault().Payout;
        //            //offerModel.Vertical = offerRes.Offers.FirstOrDefault().Vertical;
        //            //offerModel.PreLandingPages = offerRes.PreLandingPages;
        //            //offerModel.Image = "HardCoded need fix asap!";
        //            //offerModel.LandingPages = offerRes.LandingPages;
        //            //offerModel.CP = offerRes.Offers.FirstOrDefault().CP;
        //            //offerModel.Language = offerRes.Offers.FirstOrDefault().Language;
        //            //offerModel.PostbackUrl = offerRes.Offers.FirstOrDefault().PostbackUrl;

        //            return View(offerModel);
        //        }
        //    }

        //    return View(offerModel);
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
        //[AllowAnonymous]
        //public async Task<IActionResult> Load(string o, string l, string p, string u, string glm)
        //{
        //    var clientIp = this.HttpContext.Connection.RemoteIpAddress.ToString();
        //    string redUrl = "http://google.com";
        //    var req = new
        //    {
        //        LandingPage_Id = l,
        //        Offer_Id = o,
        //        PreLandingPage_Id = p,
        //        User_Id = u,
        //        IpAddress = clientIp,
        //        ClickId = glm
        //    };
        //    string url = string.Format("{0}api/Offer/InsertOfferEntry?", this.configuration.GetConnectionString("CoreApiUrl"));

        //    var response = await this.httpClient.PostAsync<BaseResultModel>(url, req);
        //    if (response.Code == 1)
        //    {
        //        //var offerRes = JsonSerializer.Deserialize<InsertOfferEntryApiResponse>(response.Message);
        //        //if (offerRes.Code == 1)
        //        //{
        //        //    redUrl = offerRes.RedirectUrl + offerRes.Id;
        //        //}
        //    }
        //    return Redirect(redUrl);
        //}
        public async Task<IActionResult> DashBoard()
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
