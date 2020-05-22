namespace GoldLeadsMedia.Database.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using GoldLeadsMedia.Database.Models.Common;

    public class DeveloperError : IEntityMetaData
    {
        public DeveloperError()
        {
            this.Id = Guid.NewGuid().ToString();
            this.CreatedOn = DateTime.UtcNow;
        }

        [Key]
        public string Id { get; set; }
        public string Method { get; set; }
        public string Path { get; set; }
        public string Message { get; set; }
        public string StackTrace { get; set; }

        public string UserId { get; set; }

        public virtual GoldLeadsMediaUser User { get; set; }

        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set ; }
        public DateTime? DeletedOn { get ; set ; }
        public string Information { get; set; }
    }
}
