namespace GoldLeadsMedia.CoreApi.Services.Common
{
    using System.Collections.Generic;

    using GoldLeadsMedia.Database.Models;

    public interface ITargetDevicesService
    {
        IEnumerable<TargetDevice> GetAll();
    }
}
