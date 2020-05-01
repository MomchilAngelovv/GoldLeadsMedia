namespace GoldLeadsMedia.CoreApi.Controllers
{
    using System.Linq;
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Mvc;
    using GoldLeadsMedia.CoreApi.Services.Application.Common;

    public class TargetDevicesController : ApiController
    {
        private readonly ITargetDevicesService targetDevicesService;

        public TargetDevicesController(
            ITargetDevicesService targetDevicesService)
        {
            this.targetDevicesService = targetDevicesService;
        }

        public ActionResult<IEnumerable<object>> GetAll()
        {
            var targetDevices = this.targetDevicesService
                .GetAll()
                .Select(targetDevice => new 
                {
                    targetDevice.Id,
                    targetDevice.Name
                })
                .ToList();

            return targetDevices;
        }
    }
}
