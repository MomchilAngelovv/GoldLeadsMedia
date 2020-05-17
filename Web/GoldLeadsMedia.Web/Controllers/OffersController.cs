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
    using System.Linq;

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

        public async Task<IActionResult> All(OffersAllFilterViewModel filterViewModel)
        {
            var queryParameters = new
            {
                filterViewModel.NumberOrName,
                filterViewModel.CountryId,
                filterViewModel.VerticalId,
                filterViewModel.PayTypeId,
                filterViewModel.DeviceId,
                filterViewModel.AccessId
            };

            var offers = await this.httpClient.GetAsync<IEnumerable<OffersAllOffer>>("Api/Offers", queryParameters);

            var viewModel = new OffersAllFilterViewModel
            {
                NumberOrName = filterViewModel.NumberOrName,
                CountryId = filterViewModel.CountryId,
                VerticalId = filterViewModel.VerticalId,
                PayTypeId = filterViewModel.PayTypeId,
                DeviceId = filterViewModel.DeviceId,
                AccessId = filterViewModel.AccessId,

                Offers = offers
            };

            return this.View(viewModel);
        }
        public async Task<IActionResult> Details(string id)
        {
            var loggedUser = await this.userManager.GetUserAsync(this.User);
            var viewModel = await this.httpClient.GetAsync<OffersDetailsViewModel>($"Api/Offers/{id}");

            var webUrl = this.configuration["WebUrl"];
            viewModel.RedirectUrl = $"{webUrl}/Clicks/Register?offerId={viewModel.Id}";

            viewModel.IsVip = loggedUser.IsVip;
            
            return this.View(viewModel);
        }
        public async Task<IActionResult> DashBoard()
        {
            var offerGroups = await this.httpClient.GetAsync<List<OffersDashboardOfferGroup>>("Api/OfferGroups");
            offerGroups.First().IsFirst = true;

            var queryParameters = new
            {
                GroupId = 1
            };

            var offers = await this.httpClient.GetAsync<List<OffersDashboardOffer>>("Api/Offers", queryParameters);

            foreach (var offer in offers)
            {
                offer.ImageUrl = $"/images/offers/{offer.Id}.jpg";
            }

            var viewModel = new OffersDashboardViewModel
            {
                OfferGroups = offerGroups,
                Offers = offers
            };

            return this.View(viewModel);
        }

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
    }
}
