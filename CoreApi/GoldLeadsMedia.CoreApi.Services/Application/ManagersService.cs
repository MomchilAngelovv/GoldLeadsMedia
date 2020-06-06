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
            return (await this.userManager
                .GetUsersInRoleAsync("Affiliate"))
                .Where(user => user.ManagerId == managerId);
        }
        public IEnumerable<Lead> GetNotConfirmedLeads()
        {
            return this.db.Leads
                .Where(lead => lead.IsConfirmed == false)
                .ToList();
        }
        public IEnumerable<Lead> GetConfirmedLeads()
        {
            return this.db.Leads
                .Where(lead => lead.IsConfirmed)
                .ToList();
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
            return this.userManager.Users
                .FirstOrDefault(user => user.Id == id);
        }

        public async Task<IEnumerable<GoldLeadsMediaUser>> GetAllAffiliates()
        {
            return await this.userManager
                .GetUsersInRoleAsync("Affiliate");
        }
    }
}
