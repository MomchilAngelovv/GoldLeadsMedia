namespace GoldLeadsMedia.Database.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using GoldLeadsMedia.Database.Models;

    public class TrackerSettingsConfiguration : IEntityTypeConfiguration<AffiliateTrackerSettings>
    {
        public void Configure(EntityTypeBuilder<AffiliateTrackerSettings> entity)
        {
            entity.HasKey(entity => entity.Id);
            entity.Property(entity => entity.LeadPostbackUrl).HasMaxLength(450);
            entity.Property(entity => entity.FtdPostbackUrl).HasMaxLength(450);
        }
    }
}
