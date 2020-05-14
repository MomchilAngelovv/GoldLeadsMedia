using GoldLeadsMedia.Database.Models.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoldLeadsMedia.Database.Models
{
    public class FtdScanError : IEntityMetaData
    {
        public FtdScanError()
        {
            this.Id = Guid.NewGuid().ToString();
            this.CreatedOn = DateTime.UtcNow;
        }

        public string Id { get; set; }
        public string Message { get; set; }
        public string Information { get; set; }
        public string PartnerName { get; set; }

        public DateTime CreatedOn { get; set ; }
        public DateTime? UpdatedOn { get; set; }
        public DateTime? DeletedOn { get; set; }
    }
}
