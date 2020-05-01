namespace GoldLeadsMedia.Web.Controllers
{
    using System;
    using System.Text.Json;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.Extensions.Configuration;

    using GoldLeadsMedia.Web.Models;
    using GoldLeadsMedia.Database.Models;
    using GoldLeadsMedia.Web.Models.Managers;
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

        [Authorize]
        //public async Task<IActionResult> Affiliates()
        //{
        //    var loggedUser = await this.userManager.GetUserAsync(this.User);
        //    if (loggedUser.Type == 1)
        //    {
        //        var viewModel = new GetUsersViewModel();
        //        string url = string.Format("{0}api/User/GetManagerUsers?", this.configuration.GetConnectionString("CoreApiUrl"));
        //        var req = new
        //        {
        //            Manager_Id = loggedUser.Id
        //        };
        //        var response = await this.httpClient.PostAsync<BaseResultModel>(url, req);
        //        if (response.Code == 1)
        //        {
        //            var reqRes = JsonSerializer.Deserialize<GetManagerUsersResultModel>(response.Message);
        //            if (reqRes.Code == 1)
        //            {
        //                viewModel.Users = reqRes.Users;
        //            }
        //        }

        //        return View(viewModel);
        //    }
        //    else
        //    {
        //        return RedirectToAction("Index", "Offer");
        //    }
        //}
        public async Task<IActionResult> AffiliateDetails(string a_Id)
        {
            var loggedUser = await this.userManager.GetUserAsync(this.User);
            var viewModel = new AffiliateDetailsViewModel();
            string url = string.Format("{0}api/User/GetManagerUsers?", this.configuration.GetConnectionString("CoreApiUrl"));
            var req = new
            {
                Manager_Id = loggedUser.Id,
                User_Id = a_Id
            };
            var response = await this.httpClient.PostAsync<BaseResultModel>(url, req);
            if (response.Code == 1)
            {
                var reqRes = JsonSerializer.Deserialize<AffiliateDetailsAffiliate>(response.Message);

                viewModel.Affiliate = reqRes;
                if (viewModel.Affiliate == null)
                {
                    return RedirectToAction("Index", "Manager");
                }

                string date = DateTime.Now.AddDays(-30).ToString("MM/dd/yyyy") + " - " + DateTime.Now.ToString("MM/dd/yyyy");
                viewModel.LeadsAndClickReport = await GetUserReportData(a_Id, date);
                viewModel.PaymentsReport = await GetUserPaymentsData(a_Id, date);
            }

            return View(viewModel);
        }
        [HttpPost]
        public async Task<ActionResult> MakePayment(MakePaymentInputModel input)
        {
            var loggedUser = await this.userManager.GetUserAsync(this.User);
            if (loggedUser.Type == 1)
            {
                string url = string.Format("{0}api/User/InsertPayment?", this.configuration.GetConnectionString("CoreApiUrl"));

                input.PayedByUser_Id = loggedUser.Id;
                var response = await this.httpClient.PostAsync<BaseResultModel>(url, input);
                if (response.Code == 1)
                {
                    var reqRes = JsonSerializer.Deserialize<BaseResultModel>(response.Message);
                    return Json(reqRes);
                }
                else
                {
                    return Json(response);
                }

            }
            else
            {
                return RedirectToAction("Index", "Offer");
            }
        }

        public async Task<AffiliateDetailsLeadsAndClickReport> GetUserReportData(string user_Id, string date)
        {
            string url = string.Format("{0}api/Report/GetUserReportByDate?", this.configuration.GetConnectionString("CoreApiUrl"));
            string fromDate = date.Split('-')[0];
            string toDate = date.Split('-')[1];
            fromDate = DateTime.Parse(fromDate).ToString("yyyyMMdd");
            toDate = DateTime.Parse(toDate).ToString("yyyyMMdd");

            var response = await this.httpClient.PostAsync<BaseResultModel>(url, new { User_Id = user_Id, ToDate = toDate, FromDate = fromDate });
            if (response.Code == 1)
            {
                var offerRes = JsonSerializer.Deserialize<AffiliateDetailsLeadsAndClickReport>(response.Message);
                return offerRes;
            }

            return null;
        }

        public async Task<AffiliateDetailsPaymentsReport> GetUserPaymentsData(string user_Id, string date)
        {
            string url = string.Format("{0}api/Report/GetUserPaymentsData?", this.configuration.GetConnectionString("CoreApiUrl"));

            string fromDate = date.Split('-')[0];
            string toDate = date.Split('-')[1];
            fromDate = DateTime.Parse(fromDate).ToString("yyyyMMdd");
            toDate = DateTime.Parse(toDate).ToString("yyyyMMdd");

            var response = await this.httpClient.PostAsync<BaseResultModel>(url, new { User_Id = user_Id, ToDate = toDate, FromDate = fromDate });
            if (response.Code == 1)
            {
                var offerRes = JsonSerializer.Deserialize<AffiliateDetailsPaymentsReport>(response.Message);

                return offerRes;
            }

            return null;
        }
    }
}
