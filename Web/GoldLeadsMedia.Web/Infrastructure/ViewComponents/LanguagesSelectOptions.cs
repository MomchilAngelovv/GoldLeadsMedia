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
    public class LanguagesSelectOptions : ViewComponent
    {
        private readonly IAsyncHttpClient httpClient;

        public LanguagesSelectOptions(
            IAsyncHttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var languages = await this.httpClient.GetAsync<List<LanguageApiResponse>>("api/languages");
            var languagesSelectOptions = languages
                .Select(language => new SelectListItem
                {
                    Value = language.Id.ToString(),
                    Text = language.Name
                })
                .ToList();

            return this.View(languagesSelectOptions);
        }
    }
}
