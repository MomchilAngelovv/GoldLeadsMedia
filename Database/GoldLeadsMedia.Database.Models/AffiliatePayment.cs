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

        [Required]
        public string PayerId { get; set; }
        [Required]
        public string ReceiverId { get; set; }

        public virtual GoldLeadsMediaUser Payer { get; set; }
        public virtual GoldLeadsMediaUser Receiver { get; set; }

        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public DateTime? DeletedOn { get; set; }
        public string Information { get; set; }
    }
}
