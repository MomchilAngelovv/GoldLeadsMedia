using GoldLeadsMedia.CoreApi.Models.ServiceModels;
using GoldLeadsMedia.CoreApi.Services.Application.Common;
using GoldLeadsMedia.Database;
using GoldLeadsMedia.Database.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoldLeadsMedia.CoreApi.Services.Application
{
    public class ManagersService : IManagersService
    {
        private readonly GoldLeadsMediaDbContext db;
        private readonly UserManager<GoldLeadsMediaUser> userManager;
        private readonly ILeadsService leadsService;

        public ManagersService(
            GoldLeadsMediaDbContext db, 
            UserManager<GoldLeadsMediaUser> userManager,
            ILeadsService leadsService)
        {
            this.db = db;
            this.userManager = userManager;
            this.leadsService = leadsService;
        }

        public async Task<IEnumerable<GoldLeadsMediaUser>> GetAffiliatesByAsync(string managerId)
        {
            var affiliates = await this.userManager.GetUsersInRoleAsync("Affiliate");
            var managerAffiliates = affiliates.Where(user => user.ManagerId == managerId);

            return affiliates;
        }

        public IEnumerable<Lead> GetNotConfirmedLeads()
        {
            var notConfirmedLeads = this.db.Leads.Where(lead => lead.ConfirmedByManagerId == null).ToList();
            return notConfirmedLeads;
        }

        public IEnumerable<Lead> GetConfirmedLeads()
        {
            var confirmedLeads = this.db.Leads.Where(lead => lead.ConfirmedByManagerId != null).ToList();
            return confirmedLeads;
        }

        public async Task<IEnumerable<Lead>> ConfirmLeadsAsync(ManagersConfirmLeadsServiceModel serviceModel)
        {
            var confirmedLeads = new List<Lead>();

            foreach (var leadId in serviceModel.LeadIds)
            {
                var lead = this.leadsService.GetBy(leadId);

                lead.ConfirmedByManagerId = serviceModel.ManagerId;
                lead.UpdatedOn = DateTime.UtcNow;

                this.db.Leads.Update(lead);
                confirmedLeads.Add(lead);
            }

            await this.db.SaveChangesAsync();

            return confirmedLeads;
        }
    }
}
