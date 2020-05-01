namespace GoldLeadsMedia.Web.Infrastructure.ViewComponents
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using GoldLeadsMedia.Web.Models.ViewModels.Shared;

    public class PageHeader : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var viewModel = new HeaderViewModel
            {
                ManagerId = "",
                ManagerUserName = ""
            };

            return this.View(viewModel);
        }
    }
}
