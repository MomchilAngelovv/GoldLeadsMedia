namespace GoldLeadsMedia.Database.Models
{
    using System;

    using Microsoft.AspNetCore.Identity;

    using GoldLeadsMedia.Database.Models.Common;

    public class GoldLeadsMediaRole : IdentityRole<string>, IEntityMetaData
    {
        public GoldLeadsMediaRole()
        {
            this.Id = Guid.NewGuid().ToString();
            this.CreatedOn = DateTime.UtcNow;
        }

        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public DateTime? DeletedOn { get; set; }
        public string Information { get; set; }
    }
}
