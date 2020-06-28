namespace GoldLeadsMedia.Web.Infrastructure.ViewComponents
{
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;

    using GoldLeadsMedia.Web.Infrastructure.HttpHelper;
    using GoldLeadsMedia.Web.Models.CoreApiResponses;

    public class OffersSelectOptions : ViewComponent
    {
        private readonly IAsyncHttpClient httpClient;

        public OffersSelectOptions(IAsyncHttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var offers = await this.httpClient.GetAsync<List<GetApiOffersOffer>>("Offers");

            var offersSelectOptions = offers
                .Select(offer => new SelectListItem
                {
                    Value = offer.Id.ToString(),
                    Text = $"{offer.Number} - {offer.Name}"
                })
                .ToList();

            return this.View(offersSelectOptions);
        }
    }
}
