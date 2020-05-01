namespace GoldLeadsMedia.CoreApi.Services.Application
{
    using System.Linq;
    using System.Collections.Generic;

    using GoldLeadsMedia.Database;
    using GoldLeadsMedia.Database.Models;
    using GoldLeadsMedia.CoreApi.Services.Application.Common;

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
    }
}
