namespace GoldLeadsMedia.Database.Models
{
    using System;
    using System.Collections.Generic;

    using GoldLeadsMedia.Database.Models.Common;

    public class Lead : IEntityMetaData
    {
        public Lead()
        {
            this.Id = Guid.NewGuid().ToString();
            this.CreatedOn = DateTime.UtcNow;
        }

        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsConfirmed { get; set; }
        public bool HasAffiliatePayments { get; set; }
        public string IdInBroker { get; set; }
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

        public virtual ICollection<SendLeadError> SendLeadErrors { get; set; }

        public DateTime CreatedOn { get ;set; }
        public DateTime? UpdatedOn { get; set; }
        public DateTime? DeletedOn { get; set; }
        public string Information { get; set; }
    }
}
