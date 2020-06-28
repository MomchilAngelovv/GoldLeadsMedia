namespace GoldLeadsMedia.CoreApi.Controllers
{
    using System.Linq;
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Mvc;
    using GoldLeadsMedia.CoreApi.Services.Common;

    public class VerticalsController : ApiController
    {
        private readonly IVerticalsService verticalsService;

        public VerticalsController(
            IVerticalsService verticalsService)
        {
            this.verticalsService = verticalsService;
        }

        public ActionResult<IEnumerable<object>> GetAll()
        {
            var verticals = this.verticalsService
                .GetAll()
                .Select(vertial => new  
                { 
                    vertial.Id,
                    vertial.Name
                })
                .ToList();

            return verticals;
        }
    }
}
