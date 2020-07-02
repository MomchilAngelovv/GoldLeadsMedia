using GoldLeadsMedia.CoreApi.Services.Common;
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
        public ActionResult<IEnumerable<object>> FtdScanErros()
        {
            var ftdScanErrors = this.errorsService
                .GetFtdScanErrors()
                .OrderByDescending(ftdScanError => ftdScanError.CreatedOn)
                .Select(ftdScanError => new
                {
                    ftdScanError.Id,
                    ftdScanError.Message,
                    Broker = ftdScanError.Broker.Name,
                    CreatedOn = ftdScanError.CreatedOn.ToString(),
                    ftdScanError.Information
                })
                .ToList();

            return ftdScanErrors;
        }
        [HttpGet("SendLead")]
        public ActionResult<IEnumerable<object>> SendLeadsErros()
        {
            var sendLeadsErrors = this.errorsService
                .GetSendLeadsErrors()
                .OrderByDescending(sendLeadError => sendLeadError.CreatedOn)
                .Select(sendLeadError => new 
                {
                    sendLeadError.Id,
                    sendLeadError.Message,
                    Broker = sendLeadError.Broker.Name,
                    Lead = sendLeadError.Lead.Email,
                    CreatedOn = sendLeadError.CreatedOn.ToString(),
                    sendLeadError.Information
                })
                .ToList();

            return sendLeadsErrors;
        }
    }
}
