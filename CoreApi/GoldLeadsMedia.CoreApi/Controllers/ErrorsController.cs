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
            var developerErrors = this.errorsService
                .GetDeveloperErrors()
                .Select(developerError => new 
                { 
                    developerError.Id,
                    developerError.Message,
                    developerError.CreatedOn,
                    developerError.Information
                })
                .ToList();

            return developerErrors;
        }
        [HttpGet("FtdScan")]
        public ActionResult<int> FtdScanErros()
        {
            var ftdScanErrors = this.errorsService
                .GetFtdScanErrors()
                .Where(ftdScanError => ftdScanError.CreatedOn.Date == DateTime.UtcNow.Date)
                .Count();

            return ftdScanErrors;
        }

        [HttpGet("SendLeads")]
        public ActionResult<int> SendLeadsErros()
        {
            var sendLeadsErrors = this.errorsService
                .GetSendLeadsErrors()
                .Where(sendLeadsError => sendLeadsError.CreatedOn.Date == DateTime.UtcNow.Date)
                .Count();

            return sendLeadsErrors;
        }
    }
}
