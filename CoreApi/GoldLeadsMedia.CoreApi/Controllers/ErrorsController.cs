using GoldLeadsMedia.CoreApi.Services.Application.Common;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoldLeadsMedia.CoreApi.Controllers
{
    public class ErrorsController : ApiController
    {
        private readonly IErrorsService errorsService;

        public ErrorsController(
            IErrorsService errorsService)
        {
            this.errorsService = errorsService;
        }
        [HttpGet("Developer")]
        public ActionResult<IEnumerable<object>> DeveloperErros()
        {
            var developerErros = this.errorsService
                .GetDeveloperErrors()
                .Select(developerError => new 
                { 
                    developerError.Id,
                    developerError.Message,
                    developerError.CreatedOn
                })
                .ToList();

            return developerErros;
        }
    }
}
