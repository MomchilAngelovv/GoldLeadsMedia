using GoldLeadsMedia.CoreApi.Services.Common;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoldLeadsMedia.CoreApi.Controllers
{
    public class ReportsController : ApiController
    {
        private readonly IBrokersService brokersService;

        public ReportsController(
            IBrokersService brokersService)
        {
            this.brokersService = brokersService;
        }

        [HttpGet("Brokers")]
        public ActionResult<IEnumerable<object>> Summary()
        {
            return this.brokersService.Summary().ToList();
        }
    }
}
