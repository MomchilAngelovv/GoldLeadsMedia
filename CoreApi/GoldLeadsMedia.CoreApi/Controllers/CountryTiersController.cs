namespace GoldLeadsMedia.CoreApi.Controllers
{
    using System.Linq;
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Mvc;
    using GoldLeadsMedia.CoreApi.Services.Common;

    public class CountryTiersController : ApiController
    {
        private readonly ICountryTiersService tierCountriesService;

        public CountryTiersController(
            ICountryTiersService tierCountriesService)
        {
            this.tierCountriesService = tierCountriesService;
        }

        public ActionResult<IEnumerable<object>> Get()
        {
            var tierCountries = this.tierCountriesService
                .GetAll()
                .Select(tierCountry => new
                {
                    tierCountry.Id,
                    tierCountry.Name
                })
                .ToList();

            return tierCountries;
        }
    }
}
