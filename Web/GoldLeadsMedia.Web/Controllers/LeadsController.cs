using GoldLeadsMedia.Web.Infrastructure.HttpHelper;
using GoldLeadsMedia.Web.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoldLeadsMedia.Web.Controllers
{
    public class LeadsController : Controller
    {
        private readonly IAsyncHttpClient httpClient;

        public LeadsController(
            IAsyncHttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var leads = await this.httpClient.GetAsync<List<LeadsAllLead>>("Leads");
            var brokers = await this.httpClient.GetAsync<List<LeadsAllBroker>>("Brokers");

            var viewModel = new LeadsAllViewModel
            {
                Leads = leads,
                Brokers = brokers
            };

            return this.View(viewModel);
        }
    }
}
