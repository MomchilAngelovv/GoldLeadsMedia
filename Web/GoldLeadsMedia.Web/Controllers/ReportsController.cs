
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

        [HttpGet]
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
        [HttpGet]
        public IActionResult Affiliates(string id)
        {
            return this.View();
        }
    }
}
