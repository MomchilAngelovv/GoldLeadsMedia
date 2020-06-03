using GoldLeadsMedia.CoreApi.Services.Application.Common;
using GoldLeadsMedia.Database;
using GoldLeadsMedia.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoldLeadsMedia.CoreApi.Services.Application
{
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
            var tierCountries = this.db.TierCountries.ToList();
            return tierCountries;
        }
    }
}
