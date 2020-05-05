namespace GoldLeadsMedia.Web.Infrastructure.ViewComponents
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using GoldLeadsMedia.Web.Models.Shared;
    using GoldLeadsMedia.Web.Infrastructure.HttpHelper;
    using Microsoft.AspNetCore.Identity;
    using GoldLeadsMedia.Database.Models;
    using GoldLeadsMedia.Web.Models.CoreApiResponses.ConventionTest;

    public class NavigationBar : ViewComponent
    {
        private readonly IAsyncHttpClient httpClient;
        private readonly UserManager<GoldLeadsMediaUser> userManager;

        public NavigationBar(
            IAsyncHttpClient httpClient,
            UserManager<GoldLeadsMediaUser> userManager)
        {
            this.httpClient = httpClient;
            this.userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var loggedUsed = await this.userManager.GetUserAsync(this.HttpContext.User);
            var affiliatePayments = await this.httpClient.GetAsync<GetAffiliatesIdPaymentsResponse>($"Api/Affiliates/{loggedUsed.Id}/Payments");

            var viewModel = new NavigationBarViewModel
            {
                Available = affiliatePayments.Available,
                Paid = affiliatePayments.Paid,
                IsLoggedUserManagerOrAdministrator = true // Hard coded need to check if user is manager or admin role
            };

            return this.View(viewModel);
        }
    }
}
