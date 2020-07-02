using GoldLeadsMedia.Web.Infrastructure.HttpHelper;
using GoldLeadsMedia.Web.Models.CoreApiResponses;
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
        public async Task<IActionResult> All(int page = 1)
        {
            var queryParameters = new
            {
                Page = page
            };

            var leads = await this.httpClient.GetAsync<List<LeadsAllLead>>("Leads", queryParameters);
            var brokers = await this.httpClient.GetAsync<List<LeadsAllBroker>>("Brokers");

            var viewModel = new LeadsAllViewModel
            {
                Leads = leads,
                Brokers = brokers,
                TotalPages = 25, //TODO HARDCODED FOR NOW need to calulate how many pages will there be 
                CurrentPage = page
            };

            return this.View(viewModel);
        }
        [HttpGet]
        public async Task<IActionResult> Details(string id)
        {
            var lead = await this.httpClient.GetAsync<LeadsDetailsLead>($"Leads/{id}");

            var viewModel = new LeadsDetailsViewModel
            {
                Lead = lead
            };

            return this.View(viewModel);
        }
        [HttpGet]
        public async Task<IActionResult> SetTest(string id)
        {
            var response = await this.httpClient.PostAsync<PostLeadsIdSetTest>($"Leads/{id}/SetTest", null);
            return this.RedirectToAction(nameof(Details),new { id });
        }
    }
}
