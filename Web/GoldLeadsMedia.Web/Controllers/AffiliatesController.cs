using GoldLeadsMedia.Web.Infrastructure.HttpHelper;
using GoldLeadsMedia.Web.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoldLeadsMedia.Web.Controllers
{
    public class AffiliatesController : Controller
    {
        private readonly IAsyncHttpClient httpClient;

        public AffiliatesController(IAsyncHttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

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
