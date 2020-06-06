namespace GoldLeadsMedia.CoreApi.Services.Application
{
    using System.Linq;
    using System.Collections.Generic;

    using GoldLeadsMedia.Database;
    using GoldLeadsMedia.Database.Models;
    using GoldLeadsMedia.CoreApi.Services.Application.Common;

    public class TargetDevicesService : ITargetDevicesService
    {
        private readonly GoldLeadsMediaDbContext db;

        public TargetDevicesService(
            GoldLeadsMediaDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<TargetDevice> GetAll()
        {
            return this.db.TargetDevices
                .ToList();
        }
    }
}
