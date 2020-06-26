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

            //One-to-one with apiRegistration and clickRegistration -> Lead should have either apiRegistration or clickRegistration
            entity.HasOne(a => a.ApiRegistration).WithOne(b => b.Lead).HasForeignKey<Lead>(b => b.ApiRegistrationId).OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(a => a.ClickRegistration).WithOne(b => b.Lead).HasForeignKey<Lead>(b => b.ClickRegistrationId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
