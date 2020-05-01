using GoldLeadsMedia.CoreApi.Services.Application.Common;
using GoldLeadsMedia.Database;
using GoldLeadsMedia.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoldLeadsMedia.CoreApi.Services.Application
{
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
            var devices = db.TargetDevices.ToList();
            return devices;
        }
    }
}
