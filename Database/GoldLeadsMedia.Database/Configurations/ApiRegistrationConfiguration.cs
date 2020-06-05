namespace GoldLeadsMedia.Database.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using GoldLeadsMedia.Database.Models;

    public class ApiRegistrationConfiguration : IEntityTypeConfiguration<ApiRegistration>
    {
        public void Configure(EntityTypeBuilder<ApiRegistration> entity)
        {
            entity.HasKey(entity => entity.Id);
            entity.Property(entity => entity.IpAddress).IsRequired().HasMaxLength(100);
            entity.Property(entity => entity.IpAddress).IsRequired();
            entity.Property(entity => entity.AffiliateId).IsRequired();
        }
    }
}
