namespace GoldLeadsMedia.Database.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using GoldLeadsMedia.Database.Models;

    public class LeadConfiguration : IEntityTypeConfiguration<Lead>
    {
        public void Configure(EntityTypeBuilder<Lead> entity)
        {
            entity.HasKey(entity => entity.Id);
            entity.Property(entity => entity.FirstName).IsRequired().HasMaxLength(100);
            entity.Property(entity => entity.LastName).IsRequired().HasMaxLength(100);
            entity.Property(entity => entity.Email).IsRequired().HasMaxLength(100);
            entity.Property(entity => entity.Password).HasMaxLength(100);
            entity.Property(entity => entity.Password).IsRequired().HasMaxLength(100);
            entity.Property(entity => entity.IdInBroker).HasMaxLength(450);
            entity.Property(entity => entity.CallStatus).HasMaxLength(100);
            entity.Property(entity => entity.FtdAmmount).HasColumnType("decimal(18,4)");
        }
    }
}
