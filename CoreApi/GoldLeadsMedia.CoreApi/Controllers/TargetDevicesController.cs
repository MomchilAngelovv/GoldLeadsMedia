namespace GoldLeadsMedia.CoreApi.Controllers
{
    using System.Linq;
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Mvc;

    using GoldLeadsMedia.CoreApi.Services.TargetDevices;
    using GoldLeadsMedia.CoreApi.Models.ResponseModels.TargetDevices;

    public class TargetDevicesController : ApiController
    {
        private readonly ITargetDevicesService targetDevicesService;

        public TargetDevicesController(
            ITargetDevicesService targetDevicesService)
        {
            this.targetDevicesService = targetDevicesService;
        }

        public ActionResult<IEnumerable<TargetDeviceResponseModel>> GetAll()
        {
            var targetDevices = this.targetDevicesService
                .GetAll()
                .Select(targetDevice => new TargetDeviceResponseModel
                {
                    Id = targetDevice.Id,
                    Name = targetDevice.Name
                })
                .ToList();

            return targetDevices;
        }
    }
}
