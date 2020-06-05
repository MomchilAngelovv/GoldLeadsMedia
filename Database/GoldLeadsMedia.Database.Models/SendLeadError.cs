namespace GoldLeadsMedia.Database.Models
{
    using System;

    using GoldLeadsMedia.Database.Models.Common;

    public class SendLeadError : IEntityMetaData
    {
        public SendLeadError()
        {
            this.Id = Guid.NewGuid().ToString();
            this.CreatedOn = DateTime.UtcNow;
        }

        public string Id { get; set; }
        public string Message { get; set; }

        public string LeadId { get; set; }
        public string BrokerId { get; set; }

        public virtual Lead Lead { get; set; }
        public virtual Broker Broker { get; set; }

        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public DateTime? DeletedOn { get; set; }
        public string Information { get; set; }
    }
}
