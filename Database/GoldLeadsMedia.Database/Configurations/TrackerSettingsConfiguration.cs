namespace GoldLeadsMedia.Database.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using GoldLeadsMedia.Database.Models;

    public class TrackerSettingsConfiguration : IEntityTypeConfiguration<TrackerConfiguration>
    {
        public void Configure(EntityTypeBuilder<TrackerConfiguration> entity)
        {
            entity.HasKey(entity => entity.Id);
            entity.Property(entity => entity.LeadPostbackUrl).HasMaxLength(450);
            entity.Property(entity => entity.FtdPostbackUrl).HasMaxLength(450);
        }
    }
}
