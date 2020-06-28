namespace GoldLeadsMedia.CoreApi.Services
{
    using System.Linq;
    using System.Collections.Generic;

    using GoldLeadsMedia.Database;
    using GoldLeadsMedia.Database.Models;
    using GoldLeadsMedia.CoreApi.Services.Common;

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
            return db.OfferGroups
                .ToList();
        }
    }
}
