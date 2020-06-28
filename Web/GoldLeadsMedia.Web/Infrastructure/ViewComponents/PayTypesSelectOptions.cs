namespace GoldLeadsMedia.Web.Infrastructure.ViewComponents
{
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;

    using GoldLeadsMedia.Web.Models.CoreApiResponses;
    using GoldLeadsMedia.Web.Infrastructure.HttpHelper;

    public class PayTypesSelectOptions : ViewComponent
    {
        private readonly IAsyncHttpClient httpClient;

        public PayTypesSelectOptions(
            IAsyncHttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var paymentTypes = await this.httpClient.GetAsync<List<GetApiPayTypesPayType>>("PayTypes");

            var paymentTypesSelectOptions = paymentTypes
                .Select(paymentType => new SelectListItem
                {
                    Value = paymentType.Id.ToString(),
                    Text = paymentType.Name
                })
                .ToList();

            return this.View(paymentTypesSelectOptions);
        }
    }
}
