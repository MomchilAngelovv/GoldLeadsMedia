namespace GoldLeadsMedia.Web.Infrastructure.ViewComponents
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using GoldLeadsMedia.Web.Models.ViewModels.Shared;
    using Microsoft.AspNetCore.Identity;
    using GoldLeadsMedia.Database.Models;

    public class PageHeader : ViewComponent
    {
        private readonly UserManager<GoldLeadsMediaUser> userManager;

        public PageHeader(UserManager<GoldLeadsMediaUser> userManager)
        {
            this.userManager = userManager;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var viewModel = new HeaderViewModel
            {
                ManagerId = "1697bfe2-8327-4804-ac1c-b884fef9c279",
                ManagerUserName = "Lora"
            };

            return this.View(viewModel);
        }
    }
}
