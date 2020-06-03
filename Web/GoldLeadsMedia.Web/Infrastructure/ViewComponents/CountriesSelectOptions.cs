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
    public class CountriesSelectOptions : ViewComponent
    {
        private readonly IAsyncHttpClient httpClient;

        public CountriesSelectOptions(
            IAsyncHttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var countries = await this.httpClient.GetAsync<List<GetApiCountriesCountry>>("Api/Countries");

            var countriesSelectOptions = countries
                .Select(country => new SelectListItem
                {
                    Value = country.Id.ToString(),
                    Text = country.Name
                })
                .ToList();

            return this.View(countriesSelectOptions);
        }
    }
}
