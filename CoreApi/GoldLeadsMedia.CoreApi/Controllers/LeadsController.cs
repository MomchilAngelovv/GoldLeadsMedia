namespace GoldLeadsMedia.CoreApi.Controllers
{
    using System.Linq;
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Mvc;

    using GoldLeadsMedia.CoreApi.Services.Leads;
    using GoldLeadsMedia.CoreApi.Models.ResponseModels.Leads;

    public class LeadsController : ApiController
    {
        private readonly ILeadsService leadsService;

        public LeadsController(
            ILeadsService leadsService)
        {
            this.leadsService = leadsService;
        }

        [HttpGet("{id}")]
        public ActionResult<IEnumerable<LeadResponseModel>> AllByUserId(string userId)
        {
            var leads = this.leadsService
                .GetAllBy(userId)
                .Select(lead => new LeadResponseModel 
                { 
                    Id = lead.Id,
                    FirstName = lead.FirstName,
                    LastName = lead.LastName,
                    Email = lead.Email,
                    Country = lead.Country.Name,
                    Offer = lead.OfferClick.Offer.Name,
                    PhoneNumber = lead.PhoneNumber,
                })
                .ToList();

            return leads;
        }
    }
}
