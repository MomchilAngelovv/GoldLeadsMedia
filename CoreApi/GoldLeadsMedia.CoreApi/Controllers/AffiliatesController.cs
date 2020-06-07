namespace GoldLeadsMedia.CoreApi.Controllers
{
    using System.Linq;
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Mvc;

    using GoldLeadsMedia.CoreApi.Services.Application.Common;

    public class AffiliatesController : ApiController
    {
        private readonly IManagersService managersService;
        private readonly IAffiliatesService affiliatesService;

        public AffiliatesController(
            IManagersService managersService,
            IAffiliatesService affiliatesService)
        {
            this.managersService = managersService;
            this.affiliatesService = affiliatesService;
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
                    LeadsCount = 0, //HARD CODED,
                    FtdsCount = 0 // HARD CODED
                })
                .ToList();

            return affiliateOfferReports;
        }
    }
}
