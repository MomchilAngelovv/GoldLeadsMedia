namespace GoldLeadsMedia.CoreApi.Controllers
{
    using System.Linq;
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Mvc;

    using GoldLeadsMedia.CoreApi.Services.OfferGroups;
    using GoldLeadsMedia.CoreApi.Models.ResponseModels.OfferGroups;

    public class OfferGroupsController : ApiController
    {
        private readonly IOfferGroupsService offerGroupService;

        public OfferGroupsController(
            IOfferGroupsService offerGroupService)
        {
            this.offerGroupService = offerGroupService;
        }

        public ActionResult<IEnumerable<OfferGroupResponseModel>> GetAll()
        {
            var offerGroups = this.offerGroupService
                .GetAll()
                .Select(offerGroup => new OfferGroupResponseModel
                {
                    Id = offerGroup.Id,
                    Name = offerGroup.Name
                })
                .ToList();

            return offerGroups;
        }
    }
}
