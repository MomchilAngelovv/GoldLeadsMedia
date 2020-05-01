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
            var paymentTypes = await this.httpClient.GetAsync<List<PaymentTypeApiResponse>>("Api/PayTypes");

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
