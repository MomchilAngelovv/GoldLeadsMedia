using GoldLeadsMedia.CoreApi.Models.ResponseModels.LandingPages;
using GoldLeadsMedia.CoreApi.Services.LandingPages;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoldLeadsMedia.CoreApi.Controllers
{
    public class LandingPagesController : ApiController
    {
        private readonly ILandingPagesService landingPagesService;

        public LandingPagesController(
            ILandingPagesService landingPagesService)
        {
            this.landingPagesService = landingPagesService;
        }

        public ActionResult<IEnumerable<LandingPageResponseModel>> All()
        {
            var landingPages = this.landingPagesService
                .GetAll()
                .Select(landingPage => new LandingPageResponseModel 
                { 
                    Id = landingPage.Id,
                    Name = landingPage.Name
                })
                .ToList();

            return landingPages;
        }
    }
}
