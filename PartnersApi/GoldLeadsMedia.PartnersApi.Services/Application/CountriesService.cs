namespace GoldLeadsMedia.PartnersApi.Services.Application
{
    using System.Linq;

    using GoldLeadsMedia.Database;
    using GoldLeadsMedia.Database.Models;
    using GoldLeadsMedia.PartnersApi.Services.Application.Common;

    public class CountriesService : ICountriesService
    {
        private readonly GoldLeadsMediaDbContext db;

        public CountriesService(
            GoldLeadsMediaDbContext db)
        {
            this.db = db;
        }

        public Country GetBy(string name)
        {
            var country = db.Countries.FirstOrDefault(country => country.Name == name);
            return country;
        }
    }
}
