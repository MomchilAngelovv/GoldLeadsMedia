namespace GoldLeadsMedia.CoreApi.Services.Application
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Identity;

    using GoldLeadsMedia.Database;
    using GoldLeadsMedia.Database.Models;
    using GoldLeadsMedia.CoreApi.Models.ServiceModels;
    using GoldLeadsMedia.CoreApi.Services.Application.Common;

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

            return managerAffiliates;
        }
        public IEnumerable<Lead> GetNotConfirmedLeads()
        {
            var notConfirmedLeads = this.db.Leads.Where(lead => lead.IsConfirmed == false).ToList();
            return notConfirmedLeads;
        }
        public IEnumerable<Lead> GetConfirmedLeads()
        {
            var confirmedLeads = this.db.Leads.Where(lead => lead.IsConfirmed).ToList();
            return confirmedLeads;
        }
        public async Task<IEnumerable<Lead>> ConfirmLeadsAsync(ManagersConfirmLeadsInputServiceModel serviceModel)
        {
            var confirmedLeads = new List<Lead>();

            foreach (var leadId in serviceModel.LeadIds)
            {
                var lead = this.leadsService.GetBy(leadId);

                lead.IsConfirmed = true;
                lead.Information += $"[Confirmed by: {serviceModel.ManagerId}]";
                lead.UpdatedOn = DateTime.UtcNow;

                this.db.Leads.Update(lead);
                confirmedLeads.Add(lead);
            }

            await this.db.SaveChangesAsync();

            return confirmedLeads;
        }
        public GoldLeadsMediaUser GetAffiliateDetailsBy(string id)
        {
            var affiliate = this.userManager.Users.FirstOrDefault(user => user.Id == id);
            return affiliate;
        }

        public async Task<IEnumerable<GoldLeadsMediaUser>> GetAll()
        {
            var affiliates = await this.userManager.GetUsersInRoleAsync("Affiliate");
            return affiliates;
        }
    }
}
