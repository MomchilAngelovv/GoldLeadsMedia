namespace GoldLeadsMedia.Database.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using GoldLeadsMedia.Database.Models;

    public class TrackerSettingsConfiguration : IEntityTypeConfiguration<TrackerSettings>
    {
        public void Configure(EntityTypeBuilder<TrackerSettings> entity)
        {
            entity.HasKey(entity => entity.Id);
            entity.Property(entity => entity.LeadPostbackUrl).HasMaxLength(450);
            entity.Property(entity => entity.FtdPostbackUrl).HasMaxLength(450);
        }
    }
}
