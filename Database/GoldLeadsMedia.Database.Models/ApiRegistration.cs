namespace GoldLeadsMedia.Database.Models
{
    using System;

    using GoldLeadsMedia.Database.Models.Common;

    public class ApiRegistration : IEntityMetaData
    {
        public ApiRegistration()
        {
            this.Id = Guid.NewGuid().ToString();
            this.CreatedOn = DateTime.UtcNow;
        }

        public string Id { get; set; }
        public string IpAddress { get; set; }

        public string LeadId { get; set; }
        public string OfferId { get; set; }
        public string AffiliateId { get; set; }

        public virtual Lead Lead { get; set; }
        public virtual Offer Offer { get; set; }
        public virtual GoldLeadsMediaUser Affiliate { get; set; }

        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public DateTime? DeletedOn { get; set; }
        public string Information { get; set; }
    }
}
