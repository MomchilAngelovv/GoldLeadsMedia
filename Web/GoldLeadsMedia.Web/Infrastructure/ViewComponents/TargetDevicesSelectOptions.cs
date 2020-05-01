namespace GoldLeadsMedia.Web.Infrastructure.ViewComponents
{
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using GoldLeadsMedia.Web.Infrastructure.HttpHelper;
    using GoldLeadsMedia.Web.Models.CoreApiResponses;

    public class TargetDevicesSelectOptions : ViewComponent
    {
        private readonly IAsyncHttpClient httpClient;

        public TargetDevicesSelectOptions(
            IAsyncHttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var devices = await this.httpClient.GetAsync<List<DeviceApiResponse>>("api/targetdevices");
            var devicesSelectOptions = devices
                .Select(device => new SelectListItem
                {
                    Value = device.Id.ToString(),
                    Text = device.Name
                })
                .ToList();

            return this.View(devicesSelectOptions);
        }
    }
}
