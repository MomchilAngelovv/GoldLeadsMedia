using GoldLeadsMedia.CoreApi.Services.Application.Common;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoldLeadsMedia.CoreApi.Controllers
{
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
