using GoldLeadsMedia.CoreApi.Services.Application.Common;
using Microsoft.AspNetCore.Mvc;
using System;

namespace GoldLeadsMedia.CoreApi.Controllers
{
    public class AffiliatesController : ApiController
    {
        private readonly IManagersService managersService;

        public AffiliatesController(
            IManagersService managersService)
        {
            this.managersService = managersService;
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
                IsVip = true,//TODO this is hardcoded need to implement some logic
                affiliate.Experience,
                Available = 1000,
                Paid = 5000
            };

            return response;
        }
    }
}
