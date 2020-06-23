namespace GoldLeadsMedia.AffiliatesApi.Services.Application
{
    using System.Linq;
    using GoldLeadsMedia.AffiliatesApi.Services.Application.Common;
    using GoldLeadsMedia.Database;
    using GoldLeadsMedia.Database.Models;

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
            var country = db.Countries.FirstOrDefault(country => country.Name == name);

            if (country == null)
            {
                return false;
            }

            return true;
        }

        public Country GetBy(string name)
        {
            var country = db.Countries.FirstOrDefault(country => country.Name == name);
            return country;
        }
    }
}
