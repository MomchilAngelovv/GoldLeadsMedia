namespace GoldLeadsMedia.Database.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using GoldLeadsMedia.Database.Models;

    public class DeveloperErrorConfiguration : IEntityTypeConfiguration<DeveloperError>
    {
        public void Configure(EntityTypeBuilder<DeveloperError> entity)
        {
            entity.HasKey(entity => entity.Id);
            entity.Property(entity => entity.Method).HasMaxLength(100);
            entity.Property(entity => entity.Path).HasMaxLength(100);
        }
    }
}
