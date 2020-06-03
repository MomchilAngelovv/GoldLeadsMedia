namespace GoldLeadsMedia.Web.Controllers
{
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Mvc;

    using GoldLeadsMedia.Web.Models.ViewModels;
    using GoldLeadsMedia.Web.Infrastructure.HttpHelper;

    public class AffiliatesController : Controller
    {
        private readonly IAsyncHttpClient httpClient;

        public AffiliatesController(
            IAsyncHttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        [HttpGet]
        public async Task<IActionResult> Details(string id)
        {
            var affiliate = await this.httpClient.GetAsync<AffiliatesDetailsAffiliate>($"Api/Affiliates/{id}");

            var viewModel = new AffiliatesDetailsViewModel
            {
                Affiliate = affiliate,
                LeadsAndClickReport = new ManagersAffiliateDetailsLeadsAndClickReport { Offers = new List<ManagersAffiliateDetailsOffer>() },
                PaymentsReport = new ManagersAffiliateDetailsPaymentsReport { Payments = new List<ManagersAffiliateDetailsPayment>() }
            };

            return this.View(viewModel);
        }
    }
}
