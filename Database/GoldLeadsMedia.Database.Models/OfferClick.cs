namespace GoldLeadsMedia.Database.Models
{
    using System;

    using GoldLeadsMedia.Database.Models.Common;

    public class OfferClick : IEntityMetaData
    {
        public OfferClick()
        {
            this.Id = Guid.NewGuid().ToString();
            this.CreatedOn = DateTime.UtcNow;
        }

        public string Id { get; set; }
        public string VisitorIpAddress { get; set; }

        public string OfferId { get; set; }
        public string LandingPageId { get; set; }
        public string UserId { get; set; }

        public virtual Offer Offer { get; set; }
        public virtual LandingPage LandingPage { get; set; }
        public virtual GoldLeadsMediaUser User { get; set; }

        public DateTime CreatedOn { get ; set; }
        public DateTime? UpdatedOn { get; set; }
        public DateTime? DeletedOn { get; set; }
        public bool IsDeleted { get; set; }
    }
}
