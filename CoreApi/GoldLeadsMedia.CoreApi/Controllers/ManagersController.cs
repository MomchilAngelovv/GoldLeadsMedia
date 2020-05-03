using GoldLeadsMedia.CoreApi.Models.InputModels;
using GoldLeadsMedia.CoreApi.Models.ServiceModels;
using GoldLeadsMedia.CoreApi.Services.Application.Common;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;

namespace GoldLeadsMedia.CoreApi.Controllers
{
    public class ManagersController : ApiController
    {
        private readonly IManagersService managersService;

        public ManagersController(
            IManagersService managersService)
        {
            this.managersService = managersService;
        }

        [HttpGet("{managerId}/Affiliates")]
        public async Task<ActionResult<IEnumerable<object>>> Affiliates(string managerId)
        {
            var managersAffiliates = await this.managersService.GetAffiliatesByAsync(managerId);

            var response = managersAffiliates
                .Where(affiliate => affiliate.ManagerId == managerId)
                .Select(affiliate => new
                {
                    affiliate.Id,
                    affiliate.UserName,
                    IsDeleted = affiliate.DeletedOn != null,
                    affiliate.Skype,
                    IsVip = false, //TODO implement logic
                    affiliate.Experience
                })
                .ToList();

            return response;
        }
        [HttpGet("NotConfirmedLeads")]
        public ActionResult<IEnumerable<object>> NotConfirmedLeads()
        {
            var notConfirmedLeads = this.managersService.GetNotConfirmedLeads();

            var response = notConfirmedLeads
                .Select(lead => new 
                { 
                    lead.Id,
                    lead.FirstName,
                    lead.LastName,
                    lead.Email,
                    lead.PhoneNumber,
                    CountryName = lead.Country.Name,
                    OfferName = lead.Click.Offer.Name
                })
                .ToList();

            return response;
        }
        [HttpGet("ConfirmedLeads")]
        public ActionResult<IEnumerable<object>> ConfirmedLeads()
        {
            var confirmedLeads = this.managersService.GetConfirmedLeads();

            var response = confirmedLeads
                .Select(lead => new
                {
                    lead.Id,
                    lead.FirstName,
                    lead.LastName,
                    lead.Email,
                    lead.PhoneNumber,
                    CountryName = lead.Country.Name,
                    OfferName = lead.Click.Offer.Name
                })
                .ToList();

            return response;
        }
        [HttpPost("ConfirmLeads")]
        public async Task<ActionResult<int>> ConfirmLeads(ManagersConfirmLeadsInputModel inputModel)
        {
            var serviceModel = new ManagersConfirmLeadsServiceModel
            {
                ManagerId = inputModel.ManagerId,
                LeadIds = inputModel.LeadIds
            };

            var confirmedLeads = await this.managersService.ConfirmLeadsAsync(serviceModel);
            return confirmedLeads.Count();
        }
    }
}
