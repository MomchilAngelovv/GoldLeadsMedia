namespace GoldLeadsMedia.Web.Controllers
{
    using System.Text.Json;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.Extensions.Configuration;

    using GoldLeadsMedia.Web.Models;
    using GoldLeadsMedia.Database.Models;
    using GoldLeadsMedia.Web.Models.ViewModels;
    using GoldLeadsMedia.Web.Infrastructure.HttpHelper;

    public class LeadsController : Controller
    {
        private readonly IConfiguration configuration;
        private readonly IAsyncHttpClient httpClient;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly UserManager<GoldLeadsMediaUser> userManager;

        public LeadsController(
            IConfiguration configuration,
            IAsyncHttpClient httpClient,
            IWebHostEnvironment webHostEnvironment,
            UserManager<GoldLeadsMediaUser> userManager)
        {
            this.configuration = configuration;
            this.httpClient = httpClient;
            this.webHostEnvironment = webHostEnvironment;
            this.userManager = userManager;
        }

        //[HttpPost]
        //[Authorize]
        //public async Task<IActionResult> SendLeadsToPartners(IEnumerable<string> ids, string partner_Id)
        //{
        //    var loggedUser = await this.userManager.GetUserAsync(this.User);
        //    string url = string.Format("{0}api/Offer/SendLeadsToPartner", this.configuration.GetConnectionString("CoreApiUrl"));
        //    var req = new 
        //    {
        //        User_Id = loggedUser.Id,
        //        Leads_Ids = string.Join(";", ids),
        //        Partner_Id = partner_Id
        //    };
        //    var success = false;
        //    var response = await this.httpClient.PostAsync<BaseResultModel>(url, req);
        //    if (response.Code == 1)
        //    {
        //        response = JsonSerializer.Deserialize<BaseResultModel>(response.Message);
        //        if (response.Code == 1)
        //        {
        //            success = true;
        //        }
        //    }
        //    if (success)
        //    {
        //        TempData["ConfirmLeads"] = "You successfully send the leads to the partner.";
        //        TempData["ConfirmLeadsType"] = "1";
        //        return Json(1);

        //    }
        //    else
        //    {
        //        TempData["ConfirmLeads"] = "Error in sending the leads. " + response.Message;
        //        TempData["ConfirmLeadsType"] = "0";
        //        return Json(0);
        //    }
        //}
    }
}
