namespace GoldLeadsMedia.Web.Controllers
{
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Mvc;

    using GoldLeadsMedia.Web.Models.ViewModels;
    using GoldLeadsMedia.Web.Infrastructure.HttpHelper;
    using GoldLeadsMedia.Web.Models.InputModels;
    using Microsoft.AspNetCore.Identity;
    using GoldLeadsMedia.Database.Models;
    using GoldLeadsMedia.Web.Models.CoreApiResponses;
    using System.Linq;

    public class AffiliatesController : Controller
    {
        private readonly IAsyncHttpClient httpClient;
        private readonly UserManager<GoldLeadsMediaUser> userManager;

        public AffiliatesController(
            IAsyncHttpClient httpClient,
            UserManager<GoldLeadsMediaUser> userManager)
        {
            this.httpClient = httpClient;
            this.userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var affiliates = await this.httpClient.GetAsync<List<AffiliatesAllAffiliate>>($"Api/Affiliates");

            var viewModel = new AffiliatesAllViewModel
            {
                Affiliates = affiliates
            };

            return this.View(viewModel);
        }
        [HttpGet]
        public async Task<IActionResult> Details(string id)
        {
            var affiliate = await this.httpClient.GetAsync<AffiliatesDetailsAffiliate>($"Api/Affiliates/{id}");
            var offerReports = await this.httpClient.GetAsync<List<AffiliatesDetailsReportSummaryOfferReport>>($"Api/Affiliates/{id}/OfferReports");

            var reportSummary = new AffiliatesDetailsReportSummary
            {
                AffiliateName = affiliate.UserName,
                OfferReports = offerReports,
                TotalClicks = offerReports.Sum(offerReport => offerReport.ClicksCount),
                TotalLeads = offerReports.Sum(offerReport => offerReport.LeadsCount),
                TotalFtds = offerReports.Sum(offerReport => offerReport.FtdsCount),
            };

            var viewModel = new AffiliatesDetailsViewModel
            {
                Affiliate = affiliate,
                ReportSummary = reportSummary
            };

            return this.View(viewModel);
        }


        [HttpPost]
        public async Task<IActionResult> UpdateTrackerConfiguration(AffiliatesUpdateTrackerConfigurationInputModel inputModel)
        {
            var loggedUser = await this.userManager.GetUserAsync(this.User);

            var requestBody = new
            {
                inputModel.LeadPostbackUrl,
                inputModel.FtdPostbackUrl
            };

            var response = await this.httpClient.PostAsync<PostApiAffiliatesIdTrackerConfiguration>($"Api/Affiliates/{loggedUser.Id}/TrackerConfiguration", requestBody);
            return this.Ok(response);
        }
    }
}
