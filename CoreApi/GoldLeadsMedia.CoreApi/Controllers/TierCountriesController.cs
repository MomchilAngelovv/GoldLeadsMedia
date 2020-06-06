namespace GoldLeadsMedia.CoreApi.Controllers
{
    using System.Linq;
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Mvc;

    using GoldLeadsMedia.CoreApi.Services.Application.Common;

    public class TierCountriesController : ApiController
    {
        private readonly ITierCountriesService tierCountriesService;

        public TierCountriesController(
            ITierCountriesService tierCountriesService)
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
