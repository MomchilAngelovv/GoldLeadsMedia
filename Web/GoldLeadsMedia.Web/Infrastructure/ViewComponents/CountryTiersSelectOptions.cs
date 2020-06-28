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
    public class CountryTiersSelectOptions : ViewComponent
    {
        private readonly IAsyncHttpClient httpClient;

        public CountryTiersSelectOptions(
            IAsyncHttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var tierCountries = await this.httpClient.GetAsync<List<GetApiTierCountriesCountry>>("CountryTiers");
            var tierCountriesSelectOptions = tierCountries
                .Select(tierCountry => new SelectListItem
                {
                    Value = tierCountry.Id.ToString(),
                    Text = tierCountry.Name
                })
                .ToList();

            return this.View(tierCountriesSelectOptions);
        }
    }
}
