namespace GoldLeadsMedia.CoreApi.Controllers
{
    using System.Linq;
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Mvc;

    using GoldLeadsMedia.CoreApi.Services.Accesses;
    using GoldLeadsMedia.CoreApi.Models.ResponseModels.Accesses;

    public class AccessesController : ApiController
    {
        private readonly IAccessesService accessesService;

        public AccessesController(
            IAccessesService accessesService)
        {
            this.accessesService = accessesService;
        }

        public ActionResult<IEnumerable<AccessResponseModel>> All()
        {
            var accesses = this.accessesService
                .GetAll()
                .Select(access => new AccessResponseModel
                {
                    Id = access.Id,
                    Name = access.Name,
                })
                .ToList();

            return accesses;
        }
    }
}
