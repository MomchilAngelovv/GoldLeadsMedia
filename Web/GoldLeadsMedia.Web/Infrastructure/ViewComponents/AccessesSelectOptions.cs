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
    public class AccessesSelectOptions : ViewComponent
    {
        private readonly IAsyncHttpClient httpClient;

        public AccessesSelectOptions(
            IAsyncHttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var accesses = await this.httpClient.GetAsync<List<GetApiAccessesAccess>>("Api/Accesses");

            var accessesSelectOptions = accesses
                .Select(access => new SelectListItem
                {
                    Value = access.Id.ToString(),
                    Text = access.Name
                })
                .ToList();

            return this.View(accessesSelectOptions);
        }
    }
}
