namespace GoldLeadsMedia.Database.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Identity;

    using GoldLeadsMedia.Database.Models.Common;
    using System.Collections;
    using System.Collections.Generic;

    public class GoldLeadsMediaUser : IdentityUser<string>, IEntityMetaData
    {
        public GoldLeadsMediaUser()
        {
            this.Id = Guid.NewGuid().ToString();
            this.CreatedOn = DateTime.UtcNow;
        }

        [MaxLength(100)]
        public string Skype { get; set; }
        [MaxLength(100)]
        public string Experience { get; set; }
        public bool IsVip { get; set; }

        public string ManagerId { get; set; }

        public virtual GoldLeadsMediaUser Manager { get; set; }


        public virtual IEnumerable<AffiliatePayment> AffiliatePayments { get; set; }
        public virtual IEnumerable<ApiRegistration> ApiRegistrations { get; set; }
        public virtual IEnumerable<ClickRegistration> ClickRegistrations { get; set; }
        public virtual IEnumerable<DeveloperError> DeveloperErrors { get; set; }

        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public DateTime? DeletedOn { get; set; }
        public string Information { get; set; }
    }
}
