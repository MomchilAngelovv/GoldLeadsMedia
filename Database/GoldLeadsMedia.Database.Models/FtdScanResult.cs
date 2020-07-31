using GoldLeadsMedia.Database.Models.Common;
using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;

namespace GoldLeadsMedia.Database.Models
{
    public class FtdScanResult : IEntityMetaData
    {
        public FtdScanResult()
        {
            this.Id = Guid.NewGuid().ToString();
            this.CreatedOn = DateTime.UtcNow;
        }

        public string Id { get; set; }
        public int ScannedBrokers { get; set; }
        public int NewFtds { get; set; }

        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public DateTime? DeletedOn { get; set; }
        public string Information { get; set; }
    }
}
