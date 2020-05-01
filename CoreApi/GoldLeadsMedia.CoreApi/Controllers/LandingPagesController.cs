using GoldLeadsMedia.CoreApi.Services.Application.Common;
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

        public ActionResult<IEnumerable<object>> All()
        {
            var landingPages = this.landingPagesService
                .GetAll()
                .Select(landingPage => new 
                { 
                    landingPage.Id,
                    landingPage.Name
                })
                .ToList();

            return landingPages;
        }
    }
}
