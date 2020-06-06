namespace GoldLeadsMedia.CoreApi.Services.Application
{
    using System.Linq;
    using System.Collections.Generic;

    using GoldLeadsMedia.Database;
    using GoldLeadsMedia.Database.Models;
    using GoldLeadsMedia.CoreApi.Services.Application.Common;

    public class TierCountriesService : ITierCountriesService
    {
        private readonly GoldLeadsMediaDbContext db;

        public TierCountriesService(
            GoldLeadsMediaDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<TierCountry> GetAll()
        {
            return this.db.TierCountries
                .ToList();
        }
    }
}
