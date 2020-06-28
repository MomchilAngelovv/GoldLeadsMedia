namespace GoldLeadsMedia.Web.Infrastructure.ViewComponents
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Identity;

    using GoldLeadsMedia.Database.Models;
    using GoldLeadsMedia.Web.Models.Shared;
    using GoldLeadsMedia.Web.Infrastructure.HttpHelper;
    using GoldLeadsMedia.Web.Models.CoreApiResponses;

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
            var affiliatePayments = await this.httpClient.GetAsync<GetAffiliatesIdPaymentsStatusResponse>($"Affiliates/{loggedUsed.Id}/PaymentsStatus");

            var viewModel = new NavigationBarViewModel
            {
                TotalEarned = affiliatePayments.TotalEarned,
                TotalPaid = affiliatePayments.TotalPaid,
                IsManager = await this.userManager.IsInRoleAsync(loggedUsed, "Manager"),
                IsAdministrator = await this.userManager.IsInRoleAsync(loggedUsed, "Administrator")
            };

            return this.View(viewModel);
        }
    }
}
