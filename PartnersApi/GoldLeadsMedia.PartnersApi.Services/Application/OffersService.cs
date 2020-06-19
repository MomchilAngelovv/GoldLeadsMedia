namespace GoldLeadsMedia.AffiliatesApi.Services.Application
{
    using System.Linq;
    using GoldLeadsMedia.AffiliatesApi.Services.Application.Common;
    using GoldLeadsMedia.Database;
    using GoldLeadsMedia.Database.Models;

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
            var offer = db.Offers.SingleOrDefault(offer => offer.Id == offerId);

            if (offer == null)
            {
                return false;
            }

            return true;
        }

        public Offer GetBy(string offerId)
        {
            var offer = db.Offers.SingleOrDefault(offer => offer.Id == offerId);
            return offer;
        }
    }
}
