namespace GoldLeadsMedia.Database.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using GoldLeadsMedia.Database.Models;

    public class FtdScanErrorConfiguration : IEntityTypeConfiguration<FtdScanError>
    {
        public void Configure(EntityTypeBuilder<FtdScanError> entity)
        {
            entity.HasKey(entity => entity.Id);
            entity.Property(entity => entity.Message).IsRequired().HasMaxLength(400);
            entity.Property(entity => entity.BrokerId).IsRequired();
        }
    }
}
