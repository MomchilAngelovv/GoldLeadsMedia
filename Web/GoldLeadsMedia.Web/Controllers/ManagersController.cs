
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using GoldLeadsMedia.Database.Models;
using GoldLeadsMedia.Web.Infrastructure.HttpHelper;
using System.Threading.Tasks;
using GoldLeadsMedia.Web.Models.ViewModels;
using System.Collections.Generic;
using GoldLeadsMedia.Web.Models.InputModels;

namespace GoldLeadsMedia.Web.Controllers
{
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
        public async Task<IActionResult> NotConfirmedLeads()
        {
            var notConfirmedLeads = await this.httpClient.GetAsync<List<ManagersNotConfirmedLeadsLead>>("Api/Managers/NotConfirmedLeads");

            var viewModel = new ManagersNotConfirmedLeadsViewModel
            {
                NotConfirmedLeads = notConfirmedLeads
            };

            return this.View(viewModel);
        }
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

        //public async Task<IActionResult> AffiliateDetails(string a_Id)
        //{
        //    var loggedUser = await this.userManager.GetUserAsync(this.User);
        //    var viewModel = new AffiliateDetailsViewModel();
        //    string url = string.Format("{0}api/User/GetManagerUsers?", this.configuration.GetConnectionString("CoreApiUrl"));
        //    var req = new
        //    {
        //        Manager_Id = loggedUser.Id,
        //        User_Id = a_Id
        //    };
        //    var response = await this.httpClient.PostAsync<BaseResultModel>(url, req);
        //    if (response.Code == 1)
        //    {
        //        var reqRes = JsonSerializer.Deserialize<AffiliateDetailsAffiliate>(response.Message);

        //        viewModel.Affiliate = reqRes;
        //        if (viewModel.Affiliate == null)
        //        {
        //            return RedirectToAction("Index", "Manager");
        //        }

        //        string date = DateTime.Now.AddDays(-30).ToString("MM/dd/yyyy") + " - " + DateTime.Now.ToString("MM/dd/yyyy");
        //        viewModel.LeadsAndClickReport = await GetUserReportData(a_Id, date);
        //        viewModel.PaymentsReport = await GetUserPaymentsData(a_Id, date);
        //    }

        //    return View(viewModel);
        //}
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
