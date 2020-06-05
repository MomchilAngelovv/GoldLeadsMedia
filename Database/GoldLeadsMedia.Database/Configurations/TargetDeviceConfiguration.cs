namespace GoldLeadsMedia.Database.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using GoldLeadsMedia.Database.Models;

    public class TargetDeviceConfiguration : IEntityTypeConfiguration<TargetDevice>
    {
        public void Configure(EntityTypeBuilder<TargetDevice> entity)
        {
            entity.HasKey(entity => entity.Id);
            entity.Property(entity => entity.Name).IsRequired().HasMaxLength(100);
        }
    }
}
