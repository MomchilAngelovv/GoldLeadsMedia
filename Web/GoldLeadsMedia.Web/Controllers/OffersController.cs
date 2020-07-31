namespace GoldLeadsMedia.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Configuration;

    using GoldLeadsMedia.Database.Models;
    using GoldLeadsMedia.Web.Models.ViewModels;
    using GoldLeadsMedia.Web.Infrastructure.HttpHelper;
    using GoldLeadsMedia.Web.Models.CoreApiResponses;

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

        [HttpGet]
        public async Task<IActionResult> All(OffersAllFilterViewModel filterViewModel)
        {
            var queryParameters = new
            {
                filterViewModel.Name,
                filterViewModel.CountryTierId,
                filterViewModel.VerticalId,
                filterViewModel.PayTypeId,
                filterViewModel.DeviceId,
                filterViewModel.AccessId
            };

            var offers = await this.httpClient.GetAsync<IEnumerable<OffersAllOffer>>("Offers", queryParameters);

            var viewModel = new OffersAllFilterViewModel
            {
                Name = filterViewModel.Name,
                CountryTierId = filterViewModel.CountryTierId,
                VerticalId = filterViewModel.VerticalId,
                PayTypeId = filterViewModel.PayTypeId,
                DeviceId = filterViewModel.DeviceId,
                AccessId = filterViewModel.AccessId,

                Offers = offers
            };

            return this.View(viewModel);
        }
        [HttpGet]
        public async Task<IActionResult> Details(string id)
        {
            var loggedUser = await this.userManager.GetUserAsync(this.User);
            var viewModel = await this.httpClient.GetAsync<OffersDetailsViewModel>($"Offers/{id}");
            var trackerConfiguration = await this.httpClient.GetAsync<GetApiAffiliatesIdTrackerConfiguration>($"Affiliates/{loggedUser.Id}/TrackerConfiguration");

            var webUrl = this.configuration["WebUrl"];
            viewModel.RedirectUrl = $"{webUrl}/Clicks/Register?offerId={viewModel.Id}&affiliateId={loggedUser.Id}";
            viewModel.LeadPostbackUrl = trackerConfiguration?.LeadPostbackUrl;
            viewModel.FtdPostbackUrl = trackerConfiguration?.FtdPostbackUrl;

            //TODO: Fix vip offers to show only for vip affiliats
            //viewModel.IsVip = loggedUser.IsVip;
            
            return this.View(viewModel);
        }
        [HttpGet]
        public async Task<IActionResult> DashBoard()
        {
            var offerGroups = await this.httpClient.GetAsync<List<OffersDashboardOfferGroup>>("OfferGroups");
            offerGroups.First().IsFirst = true;

            var queryParameters = new
            {
                GroupId = 1
            };

            var offers = await this.httpClient.GetAsync<List<OffersDashboardOffer>>("Offers", queryParameters);

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
    }
}
