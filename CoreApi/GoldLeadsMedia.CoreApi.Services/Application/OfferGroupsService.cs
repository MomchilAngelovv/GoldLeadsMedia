namespace GoldLeadsMedia.CoreApi.Services.Application
{
    using System.Linq;
    using System.Collections.Generic;

    using GoldLeadsMedia.Database;
    using GoldLeadsMedia.Database.Models;
    using GoldLeadsMedia.CoreApi.Services.Application.Common;

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
            var offerGroups = db.OfferGroups.ToList();
            return offerGroups;
        }
    }
}
