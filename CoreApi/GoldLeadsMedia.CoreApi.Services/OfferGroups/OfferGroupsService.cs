namespace GoldLeadsMedia.CoreApi.Services.OfferGroups
{
    using System.Linq;
    using System.Collections.Generic;

    using GoldLeadsMedia.Database;
    using GoldLeadsMedia.Database.Models;

    public class OfferGroupsService : IOfferGroupsService
    {
        private readonly GoldLeadsMediaDbContext db;

        public OfferGroupsService(
            GoldLeadsMediaDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<OfferGroup> GetAll()
        {
            var offerGroups = this.db.OfferGroups.ToList();
            return offerGroups;
        }
    }
}
