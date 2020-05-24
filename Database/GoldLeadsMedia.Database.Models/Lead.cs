namespace GoldLeadsMedia.Database.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using GoldLeadsMedia.Database.Models.Common;

    public class Lead : IEntityMetaData
    {
        public Lead()
        {
            this.Id = Guid.NewGuid().ToString();
            this.CreatedOn = DateTime.UtcNow;
        }

        [Key]
        public string Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }
        [Required]
        [MaxLength(100)]
        public string Email { get; set; }
        [MaxLength(100)]
        public string Password { get; set; }
        [Required]
        [MaxLength(100)]
        public string PhoneNumber { get; set; }
        public bool IsConfirmed { get; set; }
        public bool HasAffiliatePayments { get; set; }

        [MaxLength(450)]
        public string IdInBroker { get; set; }
        [MaxLength(100)]
        public string CallStatus { get; set; }
        public DateTime? FtdBecameOn { get; set; }
        public decimal? FtdAmmount { get; set; }

        public string ClickRegistrationId { get; set; }
        public string ApiRegistrationId { get; set; }
        public int CountryId { get; set; }
        public string BrokerId { get; set; }

        public virtual ClickRegistration ClickRegistration { get; set; }
        public virtual ApiRegistration ApiRegistration { get; set; }
        public virtual Country Country { get; set; }
        public virtual Broker Broker { get; set; }

        public virtual ICollection<AffiliatePayment> AffiliatePayments { get; set; }
        public virtual ICollection<SendLeadError> SendLeadErrors { get; set; }

        public DateTime CreatedOn { get ;set; }
        public DateTime? UpdatedOn { get; set; }
        public DateTime? DeletedOn { get; set; }
        public string Information { get; set; }
    }
}
