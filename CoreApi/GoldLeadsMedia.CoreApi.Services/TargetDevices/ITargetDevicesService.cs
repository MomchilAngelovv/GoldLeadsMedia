namespace GoldLeadsMedia.CoreApi.Services.TargetDevices
{
    using System.Collections.Generic;

    using GoldLeadsMedia.Database.Models;

    public interface ITargetDevicesService
    {
        IEnumerable<TargetDevice> GetAll();
    }
}
