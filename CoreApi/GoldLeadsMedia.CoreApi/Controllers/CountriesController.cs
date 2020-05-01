namespace GoldLeadsMedia.CoreApi.Controllers
{
    using System.Linq;
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Mvc;

    using GoldLeadsMedia.CoreApi.Services.Countries;
    using GoldLeadsMedia.CoreApi.Models.ResponseModels.Countries;

    public class CountriesController : ApiController
    {
        private readonly ICountriesService countriesService;

        public CountriesController(
            ICountriesService countriesService)
        {
            this.countriesService = countriesService;
        }

        public ActionResult<IEnumerable<CountryResponseModel>> All()
        {
            var countries = this.countriesService
                .GetAll()
                .Select(country => new CountryResponseModel
                {
                    Id = country.Id,
                    Name = country.Name,
                    IsoCode = country.IsoCode,
                    PhonePrefix = country.PhonePrefix
                })
                .ToList();

            return countries;
        }
    }
}
