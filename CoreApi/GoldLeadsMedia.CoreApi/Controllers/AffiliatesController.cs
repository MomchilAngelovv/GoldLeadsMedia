using GoldLeadsMedia.CoreApi.Services.Application.Common;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GoldLeadsMedia.CoreApi.Controllers
{
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
                IsBlocked = true, //TODO this is hardcoded need to implement some logic
                affiliate.IsVip,
                affiliate.Experience,
                Available = 1000,
                Paid = 5000
            };

            return response;
        }
        [HttpGet("{id}/Payments")]
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
