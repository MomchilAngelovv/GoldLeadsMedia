namespace GoldLeadsMedia.CoreApi.Controllers
{
    using System.Linq;
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Mvc;
    using GoldLeadsMedia.CoreApi.Services.Common;

    public class OfferGroupsController : ApiController
    {
        private readonly IOfferGroupsService offerGroupService;

        public OfferGroupsController(
            IOfferGroupsService offerGroupService)
        {
            this.offerGroupService = offerGroupService;
        }

        public ActionResult<IEnumerable<object>> GetAll()
        {
            var offerGroups = this.offerGroupService
                .GetAll()
                .Select(offerGroup => new 
                {
                    offerGroup.Id,
                    offerGroup.Name
                })
                .ToList();

            return offerGroups;
        }
    }
}
