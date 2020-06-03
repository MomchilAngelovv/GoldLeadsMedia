namespace GoldLeadsMedia.Web.Controllers
{
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Configuration;

    using GoldLeadsMedia.Database.Models;
    using GoldLeadsMedia.Web.Models.ViewModels;
    using GoldLeadsMedia.Web.Models.InputModels;
    using GoldLeadsMedia.Web.Infrastructure.HttpHelper;

    public class ManagersController : Controller
    {
        private readonly IConfiguration configuration;
        private readonly IAsyncHttpClient httpClient;
        private readonly UserManager<GoldLeadsMediaUser> userManager;

        public ManagersController(
            IConfiguration configuration,
            IAsyncHttpClient httpClient,
            UserManager<GoldLeadsMediaUser> userManager)
        {
            this.configuration = configuration;
            this.httpClient = httpClient;
            this.userManager = userManager;
        }

        [HttpGet]
        public IActionResult Information()
        {
            return this.View();
        }
        [HttpGet]
        public async Task<IActionResult> Affiliates()
        {
            var loggedUser = await this.userManager.GetUserAsync(this.User);
            var affiliates = await this.httpClient.GetAsync<List<ManagersAffiliatesAffiliate>>($"Api/Managers/{loggedUser.Id}/Affiliates");

            var viewModel = new ManagersAffiliatesViewModel
            {
                Affiliates = affiliates
            };

            return this.View(viewModel);
        }
        [HttpGet]
        public async Task<IActionResult> NotConfirmedLeads()
        {
            var notConfirmedLeads = await this.httpClient.GetAsync<List<ManagersNotConfirmedLeadsLead>>("Api/Managers/NotConfirmedLeads");

            var viewModel = new ManagersNotConfirmedLeadsViewModel
            {
                NotConfirmedLeads = notConfirmedLeads
            };

            return this.View(viewModel);
        }
        [HttpGet]
        public async Task<IActionResult> ConfirmedLeads()
        {
            var confirmedLeads = await this.httpClient.GetAsync<List<ManagersConfirmedLeadsLead>>("Api/Managers/ConfirmedLeads");
            var partners = await this.httpClient.GetAsync<List<ManagersConfirmedLeadsPartner>>("Api/Partners");

            var viewModel = new ManagersConfirmedLeadsViewModel
            {
                ConfirmedLeads = confirmedLeads,
                Partners = partners
            };

            return this.View(viewModel);
        }


        [HttpPost]
        public async Task<IActionResult> ConfirmLeads([FromBody]ManagersConfirmLeadsInputModel inputModel)
        {
            var loggedUser = await this.userManager.GetUserAsync(this.User);

            var requestBody = new
            {
                ManagerId = loggedUser.Id,
                inputModel.LeadIds
            };

            var response = await this.httpClient.PostAsync<int>("Api/Managers/ConfirmLeads", requestBody);

            return this.Ok(response);
        }

        //[HttpPost]
        //public async Task<ActionResult> MakePayment(MakePaymentInputModel input)
        //{
        //    var loggedUser = await this.userManager.GetUserAsync(this.User);
        //    if (loggedUser.Type == 1)
        //    {
        //        string url = string.Format("{0}api/User/InsertPayment?", this.configuration.GetConnectionString("CoreApiUrl"));

        //        input.PayedByUser_Id = loggedUser.Id;
        //        var response = await this.httpClient.PostAsync<BaseResultModel>(url, input);
        //        if (response.Code == 1)
        //        {
        //            var reqRes = JsonSerializer.Deserialize<BaseResultModel>(response.Message);
        //            return Json(reqRes);
        //        }
        //        else
        //        {
        //            return Json(response);
        //        }

        //    }
        //    else
        //    {
        //        return RedirectToAction("Index", "Offer");
        //    }
        //}

        //public async Task<AffiliateDetailsLeadsAndClickReport> GetUserReportData(string user_Id, string date)
        //{
        //    string url = string.Format("{0}api/Report/GetUserReportByDate?", this.configuration.GetConnectionString("CoreApiUrl"));
        //    string fromDate = date.Split('-')[0];
        //    string toDate = date.Split('-')[1];
        //    fromDate = DateTime.Parse(fromDate).ToString("yyyyMMdd");
        //    toDate = DateTime.Parse(toDate).ToString("yyyyMMdd");

        //    var response = await this.httpClient.PostAsync<BaseResultModel>(url, new { User_Id = user_Id, ToDate = toDate, FromDate = fromDate });
        //    if (response.Code == 1)
        //    {
        //        var offerRes = JsonSerializer.Deserialize<AffiliateDetailsLeadsAndClickReport>(response.Message);
        //        return offerRes;
        //    }

        //    return null;
        //}

        //public async Task<AffiliateDetailsPaymentsReport> GetUserPaymentsData(string user_Id, string date)
        //{
        //    string url = string.Format("{0}api/Report/GetUserPaymentsData?", this.configuration.GetConnectionString("CoreApiUrl"));

        //    string fromDate = date.Split('-')[0];
        //    string toDate = date.Split('-')[1];
        //    fromDate = DateTime.Parse(fromDate).ToString("yyyyMMdd");
        //    toDate = DateTime.Parse(toDate).ToString("yyyyMMdd");

        //    var response = await this.httpClient.PostAsync<BaseResultModel>(url, new { User_Id = user_Id, ToDate = toDate, FromDate = fromDate });
        //    if (response.Code == 1)
        //    {
        //        var offerRes = JsonSerializer.Deserialize<AffiliateDetailsPaymentsReport>(response.Message);

        //        return offerRes;
        //    }

        //    return null;
        //}
    }
}
