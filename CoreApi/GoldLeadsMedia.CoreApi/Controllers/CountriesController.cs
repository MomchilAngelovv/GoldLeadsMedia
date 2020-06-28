namespace GoldLeadsMedia.CoreApi.Controllers
{
    using System.Linq;
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Mvc;
    using GoldLeadsMedia.CoreApi.Services.Common;

    public class CountriesController : ApiController
    {
        private readonly ICountriesService countriesService;

        public CountriesController(
            ICountriesService countriesService)
        {
            this.countriesService = countriesService;
        }

        public ActionResult<IEnumerable<object>> All()
        {
            var countries = this.countriesService
                .GetAll()
                .Select(country => new 
                {
                    country.Id,
                    country.Name,
                    country.IsoCode,
                    country.PhonePrefix
                })
                .ToList();

            return countries;
        }
    }
}
