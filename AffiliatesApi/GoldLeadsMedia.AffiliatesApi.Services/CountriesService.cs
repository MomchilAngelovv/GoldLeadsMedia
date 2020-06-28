namespace GoldLeadsMedia.AffiliatesApi.Services
{
    using System.Linq;

    using GoldLeadsMedia.Database;
    using GoldLeadsMedia.Database.Models;
    using GoldLeadsMedia.AffiliatesApi.Services.Common;

    public class CountriesService : ICountriesService
    {
        private readonly GoldLeadsMediaDbContext db;

        public CountriesService(
            GoldLeadsMediaDbContext db)
        {
            this.db = db;
        }

        public bool ExistsCheckBy(string name)
        {
            var country = db.Countries
                .SingleOrDefault(country => country.Name == name);

            if (country == null)
            {
                return false;
            }

            return true;
        }
        public Country GetBy(string name)
        {
            return db.Countries
                .FirstOrDefault(country => country.Name == name);
        }
    }
}
