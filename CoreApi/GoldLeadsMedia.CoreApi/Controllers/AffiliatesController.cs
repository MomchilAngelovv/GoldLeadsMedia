namespace GoldLeadsMedia.CoreApi.Controllers
{
    using System.Linq;
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;
    using System.Security.Cryptography.X509Certificates;
    using GoldLeadsMedia.CoreApi.Models.CoreApi.Input;
    using GoldLeadsMedia.CoreApi.Models.Services.Input;
    using GoldLeadsMedia.CoreApi.Services.Common;

    public class AffiliatesController : ApiController
    {
        private readonly IManagersService managersService;
        private readonly IAffiliatesService affiliatesService;
        private readonly IOffersService offersService;

        public AffiliatesController(
            IManagersService managersService,
            IAffiliatesService affiliatesService,
            IOffersService offersService)
        {
            this.managersService = managersService;
            this.affiliatesService = affiliatesService;
            this.offersService = offersService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> All()
        {
            var affilaites = (await this.affiliatesService
                .GetAllAsync())
                .Select(affiliate => new 
                { 
                    affiliate.Id,
                    affiliate.UserName,
                    affiliate.Email,
                })
                .ToList();

            return affilaites;
        }
        [HttpGet("{id}")]
        public ActionResult<object> Details(string id)
        {
            var affiliate = this.managersService.GetAffiliateDetailsBy(id);

            var response = new
            {
                affiliate.Id,
                affiliate.UserName,
                affiliate.Email,
                affiliate.DeletedOn.HasValue,
                affiliate.IsVip,
                affiliate.Skype,
                affiliate.Experience,
                Available = 1000, //TODO this is hardcoded for test
                Paid = 5000 //TODO this is hardcoded for test
            };

            return response;
        }
        [HttpGet("{id}/PaymentsStatus")]
        public ActionResult<object> Payments(string id)
        {
            var payments = this.affiliatesService.GetPaymentsStatusBy(id);

            var result = new
            {
                payments.TotalEarned,
                payments.TotalPaid
            };

            return result;
        }
        [HttpGet("{affiliateId}/OfferReports")]
        public ActionResult<IEnumerable<object>> OfferReports(string affiliateId)
        {
            var affiliateOfferReports = this.affiliatesService
                .GetOffersBy(affiliateId)
                .Select(offer => new
                {
                    offer.Id,
                    offer.Number,
                    offer.Name,
                    ClicksCount = offer.ClickRegistrations.Count(),
                    LeadsCount = offer.ApiRegistrations.Count(apiRegistration => apiRegistration.AffiliateId == affiliateId),
                    FtdsCount = this.offersService.CalculateFtdsPerOfferIdAndAffiliateId(offer.Id, affiliateId)
                })
                .ToList();

            return affiliateOfferReports;
        }


        [HttpPost("{affiliateId}/TrackerConfiguration")]
        public async Task<ActionResult<object>> CreateOrUpdateTrackerConfiguration(string affiliateId, AffiliatesCreateOrUdpateTrackerConfigurationInputModel inputModel)
        {
            var serviceModel = new AffiliatesCreateOrUpdateTrackerConfigurationInputServiceModel
            {
                AffiliateId = affiliateId,
                LeadPostbackUrl = inputModel.LeadPostbackUrl,
                FtdPostbackUrl = inputModel.FtdPostbackUrl
            };

            await this.affiliatesService.CreateOrUpdateTrackerConfiguration(serviceModel);

            var response = new
            {
                inputModel.LeadPostbackUrl,
                inputModel.FtdPostbackUrl
            };

            return response;
        }
    }
}
