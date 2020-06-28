namespace GoldLeadsMedia.CoreApi.Services
{
    using System.Linq;
    using System.Collections.Generic;

    using GoldLeadsMedia.Database;
    using GoldLeadsMedia.Database.Models;
    using GoldLeadsMedia.CoreApi.Services.Common;

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
            return db.TargetDevices
                .ToList();
        }
    }
}
