using GoldLeadsMedia.Web.Infrastructure.HttpHelper;
using GoldLeadsMedia.Web.Models.CoreApiResponses;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoldLeadsMedia.Web.Infrastructure.ViewComponents
{
    public class LandingPagesCheckboxes : ViewComponent
    {
        private readonly IAsyncHttpClient httpClient;

        public LandingPagesCheckboxes(
            IAsyncHttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var landingPages = await this.httpClient.GetAsync<List<GetApiLandingPagesLandingPage>>("LandingPages");
            return this.View(landingPages);
        }
    }
}
