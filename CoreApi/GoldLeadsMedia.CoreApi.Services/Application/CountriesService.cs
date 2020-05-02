using GoldLeadsMedia.CoreApi.Services.Application.Common;
using GoldLeadsMedia.Database;
using GoldLeadsMedia.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoldLeadsMedia.CoreApi.Services.Application
{
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
            var countries = db.Countries.ToList();
            return countries;
        }

        public Country GetBy(string name)
        {
            var country = this.db.Countries.FirstOrDefault(country => country.Name == name);
            return country;
        }
    }
}
