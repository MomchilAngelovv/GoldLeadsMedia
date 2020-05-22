namespace GoldLeadsMedia.Database.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using GoldLeadsMedia.Database.Models.Common;

    public class ClickRegistration : IEntityMetaData
    {
        public ClickRegistration()
        {
            this.Id = Guid.NewGuid().ToString();
            this.CreatedOn = DateTime.UtcNow;
        }

        [Key]
        public string Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string IpAddress { get; set; }

        [Required]
        public string OfferId { get; set; }
        [Required]
        public string LandingPageId { get; set; }
        [Required]
        public string AffiliateId { get; set; }

        public virtual Offer Offer { get; set; }
        public virtual LandingPage LandingPage { get; set; }
        public virtual GoldLeadsMediaUser Affiliate { get; set; }

        public DateTime CreatedOn { get ; set; }
        public DateTime? UpdatedOn { get; set; }
        public DateTime? DeletedOn { get; set; }
        public string Information { get; set; }
    }
}
