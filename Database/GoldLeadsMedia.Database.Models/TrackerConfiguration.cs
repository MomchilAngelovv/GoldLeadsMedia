namespace GoldLeadsMedia.Database.Models
{
    using System;

    using GoldLeadsMedia.Database.Models.Common;

    public class TrackerConfiguration : IEntityMetaData
    {
        public TrackerConfiguration()
        {
            this.Id = Guid.NewGuid().ToString();
            this.CreatedOn = DateTime.UtcNow;
        }

        public string Id { get; set; }
        public string LeadPostbackUrl { get; set; }
        public string FtdPostbackUrl { get; set; }

        public string AffiliateId { get; set; }

        public virtual GoldLeadsMediaUser Affiliate { get; set; }

        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public DateTime? DeletedOn { get; set; }
        public string Information { get; set; }
    }
}
