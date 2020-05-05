namespace GoldLeadsMedia.Web.Infrastructure.ViewComponents
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using GoldLeadsMedia.Web.Models.Shared;
    using GoldLeadsMedia.Web.Infrastructure.HttpHelper;

    public class NavigationBar : ViewComponent
    {
        private readonly IAsyncHttpClient httpClient;

        public NavigationBar(IAsyncHttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            
            var viewModel = new NavigationBarViewModel
            {
                Earned = 1,
                Paid = 2,
                IsLoggedUserManagerOrAdministrator = true
            };

            return this.View(viewModel);
        }
    }
}
