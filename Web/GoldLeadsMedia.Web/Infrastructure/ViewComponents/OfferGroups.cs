namespace GoldLeadsMedia.Web.Infrastructure.ViewComponents
{
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Mvc;

    using GoldLeadsMedia.Web.Infrastructure.HttpHelper;
    using GoldLeadsMedia.Web.Models.ViewModels;

    public class OfferGroups : ViewComponent
    {
        private readonly IAsyncHttpClient httpClient;

        public OfferGroups(
            IAsyncHttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var viewModel = new OffersOfferGroupsViewModel
            {
                OfferGroups = await this.httpClient.GetAsync<List<OfferGroupsOffer>>("api/offergroups")
            };

            return this.View(viewModel);
        }
    }
}
