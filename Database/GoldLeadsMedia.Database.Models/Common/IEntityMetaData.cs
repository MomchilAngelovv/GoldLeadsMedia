namespace GoldLeadsMedia.Database.Models.Common
{
    using System;

    public interface IEntityMetaData
    {
        DateTime CreatedOn { get; set; }
        DateTime? UpdatedOn { get; set; }
        DateTime? DeletedOn { get; set; }
        bool IsDeleted { get; set; }
    }
}
