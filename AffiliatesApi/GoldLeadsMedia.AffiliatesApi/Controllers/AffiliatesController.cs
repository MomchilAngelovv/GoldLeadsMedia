using GoldLeadsMedia.AffiliatesApi.Common;
using GoldLeadsMedia.AffiliatesApi.Services.Application.Common;
using GoldLeadsMedia.Database.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoldLeadsMedia.AffiliatesApi.Controllers
{
    public class AffiliatesController : ApiController
    {
        private readonly UserManager<GoldLeadsMediaUser> userManager;
        private readonly ILeadsService leadsService;

        public AffiliatesController(
            UserManager<GoldLeadsMediaUser> userManager,
            ILeadsService leadsService)
        {
            this.userManager = userManager;
            this.leadsService = leadsService;
        }

        [HttpGet("{affiliateId}/Leads")]
        public async Task<ActionResult<object>> LeadsByAffiliateId(string affiliateId)
        {
            var affiliate = await this.userManager.FindByIdAsync(affiliateId);
            if (affiliate == null)
            {
                return BadRequest(ErrorMessages.AffiliateNotFound);
            }

            var leads = leadsService
                .GetAllBy(affiliateId)
                .Select(lead => new
                {
                    lead.Id,
                    lead.FirstName,
                    lead.LastName,
                    lead.Password,
                    lead.Country.PhonePrefix,
                    lead.PhoneNumber,
                    lead.Email,
                    CountryName = lead.Country.Name,
                    lead.FtdBecameOn,
                    lead.CallStatus,
                    lead.CreatedOn,
                    affiliateId,
                });

            var response = new
            {
                Leads = leads
            };

            return response;
        }
    }
}
