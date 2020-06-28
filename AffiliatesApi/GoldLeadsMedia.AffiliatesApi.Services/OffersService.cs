namespace GoldLeadsMedia.AffiliatesApi.Services
{
    using System.Linq;

    using GoldLeadsMedia.Database;
    using GoldLeadsMedia.Database.Models;
    using GoldLeadsMedia.AffiliatesApi.Services.Common;

    public class OffersService : IOffersService
    {
        private readonly GoldLeadsMediaDbContext db;

        public OffersService(
            GoldLeadsMediaDbContext db)
        {
            this.db = db;
        }

        public bool ExistsCheckBy(string offerId)
        {
            var offer = db.Offers
                .SingleOrDefault(offer => offer.Id == offerId);

            if (offer == null)
            {
                return false;
            }

            return true;
        }
        public Offer GetBy(string offerId)
        {
            return db.Offers
                .SingleOrDefault(offer => offer.Id == offerId);
        }
    }
}
