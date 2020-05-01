using GoldLeadsMedia.Web.Infrastructure.HttpHelper;
using GoldLeadsMedia.Web.Models.CoreApiResponses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoldLeadsMedia.Web.Infrastructure.ViewComponents
{
    public class VerticalsSelectOptions : ViewComponent
    {
        private readonly IAsyncHttpClient httpClient;

        public VerticalsSelectOptions(
            IAsyncHttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var verticals = await this.httpClient.GetAsync<List<VerticalApiResponse>>("api/verticals");
            var verticalsSelectOptions = verticals
                .Select(vertical => new SelectListItem
                {
                    Value = vertical.Id.ToString(),
                    Text = vertical.Name
                })
                .ToList();

            return this.View(verticalsSelectOptions);
        }
    }
}
