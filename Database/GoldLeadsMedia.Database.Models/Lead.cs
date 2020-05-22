namespace GoldLeadsMedia.Database.Models
{
    using System;

    using GoldLeadsMedia.Database.Models.Common;

    public class Lead : IEntityMetaData
    {
        public Lead()
        {
            this.Id = Guid.NewGuid().ToString();
            this.CreatedOn = DateTime.UtcNow;
        }

        //Lead information
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsConfirmed { get; set; }

        //Information for lead in broker's system
        public string IdInBroker { get; set; }
        public string CallStatus { get; set; }
        public DateTime? FtdBecameOn { get; set; }

        //Lead will have either ClickRegistrationId or ApiRegistrationId -> depends from where lead is registered
        public string ClickRegistrationId { get; set; }
        public string ApiRegistrationId { get; set; }
        public int CountryId { get; set; }
        public string BrokerId { get; set; }

        public virtual ClickRegistration ClickRegistration { get; set; }
        public virtual ApiRegistration ApiRegistration { get; set; }
        public virtual Country Country { get; set; }
        public virtual Broker Broker { get; set; }

        public DateTime CreatedOn { get ;set; }
        public DateTime? UpdatedOn { get; set; }
        public DateTime? DeletedOn { get; set; }
        public string Information { get; set; }
    }
}
