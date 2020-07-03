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
            var loggedUser = await this.userManager.GetUserAsync(this.UserClaimsPrincipal);

            var viewModel = new PageHeaderViewModel
            {
                ManagerId = "81238bec-3d49-47c2-8423-84f1dff6d738",
                ManagerUserName = "Lora"
            };
            
            //TODO: Fix this with images
            //if (loggedUser.ManagerId != null)
            //{
            //    viewModel.ManagerId = loggedUser.ManagerId;
            //}

            return this.View(viewModel);
        }
    }
}
