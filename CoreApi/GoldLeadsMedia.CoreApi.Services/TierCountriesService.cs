namespace GoldLeadsMedia.CoreApi.Services
{
    using System.Linq;
    using System.Collections.Generic;

    using GoldLeadsMedia.Database;
    using GoldLeadsMedia.Database.Models;
    using GoldLeadsMedia.CoreApi.Services.Common;

    public class TierCountriesService : ICountryTiersService
    {
        private readonly GoldLeadsMediaDbContext db;

        public TierCountriesService(
            GoldLeadsMediaDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<CountryTier> GetAll()
        {
            return db.CountryTiers
                .ToList();
        }
    }
}
