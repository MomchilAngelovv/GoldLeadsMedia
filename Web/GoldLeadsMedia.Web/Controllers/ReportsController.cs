
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using GoldLeadsMedia.Database.Models;
using GoldLeadsMedia.Web.Infrastructure.HttpHelper;
using GoldLeadsMedia.Web.Models.ViewModels;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace GoldLeadsMedia.Web.Controllers
{
    public class ReportsController : Controller
    {
        private readonly IConfiguration configuration;
        private readonly IAsyncHttpClient httpClient;
        private readonly UserManager<GoldLeadsMediaUser> userManager;

        public ReportsController(
            IConfiguration configuration,
            IAsyncHttpClient httpClient,
            UserManager<GoldLeadsMediaUser> userManager)
        {
            this.configuration = configuration;
            this.httpClient = httpClient;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Summary()
        {
            var loggedUser = await this.userManager.GetUserAsync(this.User);

            var offerReports = await this.httpClient.GetAsync<List<ReportsSummaryOfferReport>>($"Api/Affiliates/{loggedUser.Id}/OfferReports");

            var viewModel = new ReportsSummaryViewModel
            {
                AffiliateName = loggedUser.UserName,
                OfferReports = offerReports,
                TotalClicks = offerReports.Sum(offerReport => offerReport.ClicksCount),
                TotalLeads = offerReports.Sum(offerReport => offerReport.LeadsCount),
                TotalFtds = offerReports.Sum(offerReport => offerReport.FtdsCount),
            };

            return this.View(viewModel);
        }

        public IActionResult Affiliates(string id)
        {
            return this.View();
        }

        //public async Task<IActionResult> GetUserReport(string userId, string date)
        //{
        //    if (this.ModelState.IsValid == false)
        //    {
        //        return this.Redirect("/Manager/Index");
        //    }

        //    var viewModel = new AffiliateDetailsLeadsAndClickReport();

        //    string url = string.Format("{0}api/Report/GetUserReportByDate?", this.configuration.GetConnectionString("CoreApiUrl"));

        //    string fromDate = date.Split('-')[0];
        //    string toDate = date.Split('-')[1];
        //    fromDate = DateTime.Parse(fromDate).ToString("yyyyMMdd");
        //    toDate = DateTime.Parse(toDate).ToString("yyyyMMdd");

        //    var response = await this.httpClient.PostAsync<BaseResultModel>(url, new { User_Id = userId, ToDate = toDate, FromDate = fromDate });
        //    if (response.Code == 1)
        //    {
        //        var offerRes = JsonSerializer.Deserialize<AffiliateDetailsLeadsAndClickReport>(response.Message);
        //        viewModel.Offers = offerRes.Offers;
        //    }

        //    return PartialView("_UserReportTable", viewModel);
        //}
        //[HttpPost]
        //public async Task<IActionResult> GetUserPaymentsData(string userId, string date)
        //{
        //    var viewModel = new AffiliateDetailsPaymentsReport();
        //    string url = string.Format("{0}api/Report/GetUserPaymentsData?", this.configuration.GetConnectionString("CoreApiUrl"));

        //    string fromDate = date.Split('-')[0];
        //    string toDate = date.Split('-')[1];
        //    fromDate = DateTime.Parse(fromDate).ToString("yyyyMMdd");
        //    toDate = DateTime.Parse(toDate).ToString("yyyyMMdd");

        //    var response = await this.httpClient.PostAsync<BaseResultModel>(url, new { User_Id = userId, FromDate = fromDate, ToDate = toDate });
        //    if (response.Code == 1)
        //    {
        //        var offerRes = JsonSerializer.Deserialize<AffiliateDetailsPaymentsReport>(response.Message);

        //        viewModel.Payments = offerRes.Payments;

        //    }


        //    return PartialView("_UserPaymentsTable", viewModel);
        //}
    }
}
