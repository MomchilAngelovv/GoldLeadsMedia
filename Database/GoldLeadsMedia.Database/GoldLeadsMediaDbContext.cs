namespace GoldLeadsMedia.Database
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

    using GoldLeadsMedia.Database.Models;

    public class GoldLeadsMediaDbContext : IdentityDbContext<GoldLeadsMediaUser, GoldLeadsMediaRole, string>
    {
        public GoldLeadsMediaDbContext(DbContextOptions options) 
            : base(options)
        {

        }

        public DbSet<Access> Accesses { get; set; }
        public DbSet<AffiliatePayment> AffiliatePayments { get; set; }
        public DbSet<ApiRegistration> ApiRegistrations { get; set; }
        public DbSet<Broker> Brokers { get; set; }
        public DbSet<ClickRegistration> ClickRegistrations { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<DeveloperError> DeveloperErrors { get; set; }
        public DbSet<FtdScanError> FtdScanErrors { get; set; }
        public DbSet<LandingPage> LandingPages { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Lead> Leads { get; set; }
        public DbSet<OfferGroup> OfferGroups { get; set; }
        public DbSet<Offer> Offers { get; set; }
        public DbSet<OfferLandingPage> OffersLandingPages { get; set; }
        public DbSet<OfferOfferGroup> OffersOfferGroups { get; set; }
        public DbSet<PayType> PaymentTypes { get; set; }
        public DbSet<SendLeadError> SendLeadErrors { get; set; }
        public DbSet<TargetDevice> TargetDevices { get; set; }
        public DbSet<TierCountry> TierCountries { get; set; }
        public DbSet<Vertical> Verticals { get; set; }

        //TODO MAKE configure classes for EVERY Entityt
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<OfferLandingPage>().HasKey(e => new { e.OfferId, e.LandingPageId });

            builder.Entity<OfferOfferGroup>().HasKey(e => new { e.OfferId, e.OfferGroupId });

            builder.Entity<Offer>().Property(p => p.PayPerClick).HasColumnType("decimal(18,4)");
            builder.Entity<Offer>().Property(p => p.PayPerAction).HasColumnType("decimal(18,4)");
            builder.Entity<AffiliatePayment>().Property(p => p.Amount).HasColumnType("decimal(18,4)");
            builder.Entity<Lead>().Property(p => p.FtdAmmount).HasColumnType("decimal(18,4)");
            builder.Entity<Offer>().Property(p => p.PayPerLead).HasColumnType("decimal(18,4)");

            base.OnModelCreating(builder);
        }
    }
}
