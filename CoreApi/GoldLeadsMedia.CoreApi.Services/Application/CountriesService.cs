namespace GoldLeadsMedia.CoreApi.Services.Application
{
    using System.Linq;
    using System.Collections.Generic;

    using GoldLeadsMedia.Database;
    using GoldLeadsMedia.Database.Models;
    using GoldLeadsMedia.CoreApi.Services.Application.Common;

    public class CountriesService : ICountriesService
    {
        private readonly GoldLeadsMediaDbContext db; 

        public CountriesService(
            GoldLeadsMediaDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<Country> GetAll()
        {
            return this.db.Countries
                .ToList();
        }

        public Country GetBy(string name)
        {
            return this.db.Countries
                .FirstOrDefault(country => country.Name == name);
        }
    }
}
