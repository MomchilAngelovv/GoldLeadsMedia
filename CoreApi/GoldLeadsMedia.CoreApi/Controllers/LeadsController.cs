namespace GoldLeadsMedia.CoreApi.Controllers
{
    using System.Linq;
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Mvc;
    using GoldLeadsMedia.CoreApi.Services.Application.Common;

    public class LeadsController : ApiController
    {
        private readonly ILeadsService leadsService;

        public LeadsController(
            ILeadsService leadsService)
        {
            this.leadsService = leadsService;
        }

        [HttpGet("{id}")]
        public ActionResult<IEnumerable<object>> AllByUserId(string userId)
        {
            var leads = this.leadsService
                .GetAllBy(userId)
                .Select(lead => new  
                { 
                    lead.Id,
                    lead.FirstName,
                    lead.LastName,
                    lead.Email,
                    Country = lead.Country.Name,
                    Offer = lead.Click.Offer.Name,
                    lead.PhoneNumber,
                })
                .ToList();

            return leads;
        }
    }
}
