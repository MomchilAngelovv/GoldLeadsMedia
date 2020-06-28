namespace GoldLeadsMedia.CoreApi.Controllers
{
    using System.Linq;
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Mvc;
    using GoldLeadsMedia.CoreApi.Services.Common;

    //TODO Make every api response OBJECT not array of objects
    public class AccessesController : ApiController
    {
        private readonly IAccessesService accessesService;

        public AccessesController(
            IAccessesService accessesService)
        {
            this.accessesService = accessesService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<object>> All()
        {
            var accesses = this.accessesService
                .GetAll()
                .Select(access => new
                {
                    access.Id,
                    access.Name,
                })
                .ToList();

            return accesses;
        }
    }
}
