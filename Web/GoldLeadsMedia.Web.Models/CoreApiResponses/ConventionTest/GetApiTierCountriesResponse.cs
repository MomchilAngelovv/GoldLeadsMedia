using System;
using System.Collections.Generic;
using System.Text;

namespace GoldLeadsMedia.Web.Models.CoreApiResponses.ConventionTest
{
    public class GetApiTierCountriesResponse
    {
        public IEnumerable<GetApiTierCountriesCountry> TierCountries { get; set; }
    }
}
