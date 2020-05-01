namespace GoldLeadsMedia.CoreApi.Services.Leads
{
    using System.Linq;
    using System.Collections.Generic;

    using GoldLeadsMedia.Database;
    using GoldLeadsMedia.Database.Models;

    public class LeadsService : ILeadsService
    {
        private readonly GoldLeadsMediaDbContext db;

        public LeadsService(
            GoldLeadsMediaDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<Lead> GetAllBy(string userId)
        {
            var leads = this.db.Leads
                .Where(lead => lead.OfferClick.UserId == userId);

            return leads;
        }
    }
}
