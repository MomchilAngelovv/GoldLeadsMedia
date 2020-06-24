namespace GoldLeadsMedia.Database.Models
{
    using System;

    using GoldLeadsMedia.Database.Models.Common;

    public class ClickRegistration : IEntityMetaData
    {
        public ClickRegistration()
        {
            this.Id = Guid.NewGuid().ToString();
            this.CreatedOn = DateTime.UtcNow;
        }

        public string Id { get; set; }
        public string IpAddress { get; set; }
        public string SubAffiliate1 { get; set; }
        public string SubAffiliate2 { get; set; }
        public string SubAffiliate3 { get; set; }
        public string SubAffiliate4 { get; set; }
        public string SubAffiliate5 { get; set; }

        public string LeadId { get; set; }
        public string OfferId { get; set; }
        public string AffiliateId { get; set; }
        public string LandingPageId { get; set; }
        public string AffiliateTrackerClickId { get; set; }

        public virtual Lead Lead { get; set; }
        public virtual Offer Offer { get; set; }
        public virtual LandingPage LandingPage { get; set; }
        public virtual GoldLeadsMediaUser Affiliate { get; set; }

        public DateTime CreatedOn { get ; set; }
        public DateTime? UpdatedOn { get; set; }
        public DateTime? DeletedOn { get; set; }
        public string Information { get; set; }
    }
}
