namespace GoldLeadsMedia.CoreApi.Controllers
{
    using System.Linq;
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Mvc;

    using GoldLeadsMedia.CoreApi.Services.Verticals;
    using GoldLeadsMedia.CoreApi.Models.ResponseModels.Verticals;

    public class VerticalsController : ApiController
    {
        private readonly IVerticalsService verticalsService;

        public VerticalsController(
            IVerticalsService verticalsService)
        {
            this.verticalsService = verticalsService;
        }

        public ActionResult<IEnumerable<VerticalResponseModel>> GetAll()
        {
            var verticals = this.verticalsService
                .GetAll()
                .Select(vertial => new VerticalResponseModel 
                { 
                    Id = vertial.Id,
                    Name = vertial.Name
                })
                .ToList();

            return verticals;
        }
    }
}
