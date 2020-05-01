﻿namespace GoldLeadsMedia.Database
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
        public DbSet<Country> Countries { get; set; }
        public DbSet<LandingPage> LandingPages { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Lead> Leads { get; set; }
        public DbSet<Offer> Offers { get; set; }
        public DbSet<OfferClick> OfferClicks { get; set; }
        public DbSet<OfferGroup> OfferGroups { get; set; }
        public DbSet<OfferLandingPage> OffersLandingPages { get; set; }
        public DbSet<Partner> Partners { get; set; }
        public DbSet<PaymentType> PaymentTypes { get; set; }
        public DbSet<TargetDevice> TargetDevices { get; set; }
        public DbSet<Vertical> Verticals { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<OfferLandingPage>().HasKey(e => new { e.OfferId, e.LandingPageId });

            builder.Entity<Offer>().Property(p => p.EarningPerClick).HasColumnType("decimal(18,4)");
            builder.Entity<Offer>().Property(p => p.PayOut).HasColumnType("decimal(18,4)");

            base.OnModelCreating(builder);
        }
    }
}
