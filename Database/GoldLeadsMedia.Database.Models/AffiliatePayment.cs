namespace GoldLeadsMedia.Database.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using GoldLeadsMedia.Database.Models.Common;

    public class AffiliatePayment : IEntityMetaData
    {
        public AffiliatePayment()
        {
            this.Id = Guid.NewGuid().ToString();
            this.CreatedOn = DateTime.UtcNow;
        }

        [Key]
        public string Id { get; set; }
        public decimal Amount { get; set; }
        [Required]
        [MaxLength(100)]
        public string Reason { get; set; }
        [MaxLength(100)]
        public string InvoiceNumber { get; set; }

        [Required]
        public string AffiliateId { get; set; }
        [Required]
        public string OfferId { get; set; }
        [Required]
        public string LeadId { get; set; }

        public virtual GoldLeadsMediaUser Affiliate { get; set; }
        public virtual Offer Offer { get; set; }
        public virtual Lead Lead { get; set; }

        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public DateTime? DeletedOn { get; set; }
        public string Information { get; set; }
    }
}
