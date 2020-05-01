namespace GoldLeadsMedia.Database.Models
{
    using System;

    using Microsoft.AspNetCore.Identity;

    using GoldLeadsMedia.Database.Models.Common;

    public class GoldLeadsMediaUser : IdentityUser<string>, IEntityMetaData
    {
        public GoldLeadsMediaUser()
        {
            this.Id = Guid.NewGuid().ToString();
            this.CreatedOn = DateTime.UtcNow;
        }

        public string Skype { get; set; }
        public string Experience { get; set; }
        public int Type { get; set; }

        public string ManagerId { get; set; }
        public virtual GoldLeadsMediaUser Manager { get; set; }

        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public DateTime? DeletedOn { get; set; }
        public bool IsDeleted { get; set; }
    }
}
