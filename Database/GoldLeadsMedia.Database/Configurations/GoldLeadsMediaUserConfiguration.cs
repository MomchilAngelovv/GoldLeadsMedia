namespace GoldLeadsMedia.Database.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using GoldLeadsMedia.Database.Models;

    public class GoldLeadsMediaUserConfiguration : IEntityTypeConfiguration<GoldLeadsMediaUser>
    {
        public void Configure(EntityTypeBuilder<GoldLeadsMediaUser> entity)
        {
            entity.Property(entity => entity.Skype).HasMaxLength(100);
            entity.Property(entity => entity.Experience).HasMaxLength(100);
        }
    }
}
