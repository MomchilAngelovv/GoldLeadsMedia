namespace GoldLeadsMedia.CoreApi.Services.Application
{
    using System.Linq;
    using System.Collections.Generic;

    using GoldLeadsMedia.Database;
    using GoldLeadsMedia.Database.Models;
    using GoldLeadsMedia.CoreApi.Services.Application.Common;
    using System.Threading.Tasks;
    using GoldLeadsMedia.CoreApi.Models.ServiceModels;

    public class LeadsService : ILeadsService
    {
        private readonly GoldLeadsMediaDbContext db;

        public LeadsService(
            GoldLeadsMediaDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<Lead> GetAllBy(string affiliateId)
        {
            var leads = db.Leads
                .Where(lead => lead.Click.AffiliateId == affiliateId);

            return leads;
        }

        public Lead GetBy(string id)
        {
            var lead = this.db.Leads.FirstOrDefault(lead => lead.Id == id);
            return lead;
        }

        public async Task<Lead> RegisterAsync(LeadsRegisterInputServiceModel serviceModel)
        {
            var lead = new Lead
            {
                FirstName = serviceModel.FirstName,
                LastName = serviceModel.LastName,
                Email = serviceModel.Email,
                Password = serviceModel.Password,
                CountryId = serviceModel.CountryId,
                PhoneNumber = serviceModel.PhoneNumber,
                ClickId = serviceModel.ClickId
            };

            await this.db.Leads.AddAsync(lead);
            await this.db.SaveChangesAsync();

            return lead;
        }
    }
}
