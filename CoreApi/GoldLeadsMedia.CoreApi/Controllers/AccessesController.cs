using System.Linq;
using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;
using GoldLeadsMedia.CoreApi.Services.Application.Common;

namespace GoldLeadsMedia.CoreApi.Controllers
{
    public class AccessesController : ApiController
    {
        private readonly IAccessesService accessesService;

        public AccessesController(
            IAccessesService accessesService)
        {
            this.accessesService = accessesService;
        }

        public ActionResult<IEnumerable<object>> All()
        {
            var accesses = this.accessesService.GetAll();
            
            var response = accesses
                .Select(access => new
                {
                    access.Id,
                    access.Name,
                })
                .ToList();

            return response;
        }
    }
}
