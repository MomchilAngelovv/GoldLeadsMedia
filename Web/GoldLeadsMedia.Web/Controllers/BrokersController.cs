using GoldLeadsMedia.Web.Infrastructure.HttpHelper;
using GoldLeadsMedia.Web.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoldLeadsMedia.Web.Controllers
{
    public class BrokersController : Controller
    {
        private readonly IAsyncHttpClient httpClient;

        public BrokersController(
            IAsyncHttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public async Task<IActionResult> All()
        {
            var brokers = await this.httpClient.GetAsync<List<BrokersAllBroker>>("Api/Reports/Brokers");

            var viewModel = new BrokersAllViewModel
            {
                Brokers = brokers
            };

            return this.View(viewModel);
        }
    }
}
