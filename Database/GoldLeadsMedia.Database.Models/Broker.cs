namespace GoldLeadsMedia.Database.Models
{
    using System;
    using System.Collections.Generic;

    using GoldLeadsMedia.Database.Models.Common;

    public class Broker : IEntityMetaData
    {
        public Broker()
        {
            this.Id = Guid.NewGuid().ToString();
            this.CreatedOn = DateTime.UtcNow;
        }

        public string Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Lead> Leads { get; set; }
        public virtual ICollection<FtdScanError> FtdScanErrors { get; set; }
        public virtual ICollection<SendLeadError> SendLeadErrors { get; set; }

        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public DateTime? DeletedOn { get; set; }
        public string Information { get; set; }
    }
}
