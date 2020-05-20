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

        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        //No idea why need to keep this field -> something Mike thinking -> ask Mike for more info if you are curiuos too
        public string Password { get; set; }

        public string CallStatus { get; set; }
        public string ConfirmedByManagerId { get; set; }
        public DateTime? FtdBecameOn { get; set; }
        public string IdInPartner { get; set; }

        public int CountryId { get; set; }
        public string PartnerId { get; set; }

        //Either click or api registration will be filled up since we have 2 ways of saving leads
        public string ClickId { get; set; }
        public string ApiRegistrationId { get; set; }

        public virtual Country Country { get; set; }
        public virtual Partner Partner { get; set; }
        public virtual Click Click { get; set; }
        public virtual ApiRegistration ApiRegistration { get; set; }

        public DateTime CreatedOn { get ;set; }
        public DateTime? UpdatedOn { get; set; }
        public DateTime? DeletedOn { get; set; }
    }
}
