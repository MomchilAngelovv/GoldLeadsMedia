using GoldLeadsMedia.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoldLeadsMedia.Database.Configurations
{
    public class OfferLandingPageConfiguration : IEntityTypeConfiguration<OfferLandingPage>
    {
        public void Configure(EntityTypeBuilder<OfferLandingPage> builder)
        {
            builder.HasKey(e => new { e.OfferId, e.LandingPageId });
        }
    }
}
