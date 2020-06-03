namespace GoldLeadsMedia.Web.Infrastructure.ViewComponents
{
    using System.Linq;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using GoldLeadsMedia.Web.Models.CoreApiResponses;
    using GoldLeadsMedia.Web.Infrastructure.HttpHelper;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;

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
            var verticals = await this.httpClient.GetAsync<List<GetApiVerticalsVertical>>("Api/Verticals");

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
