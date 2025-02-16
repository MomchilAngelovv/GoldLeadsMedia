﻿namespace GoldLeadsMedia.Web.Models.ViewModels
{
    using System.Collections.Generic;

    public class OffersDetailsViewModel
    {
        public string Id { get; set; }
        public string Number { get; set; }
        public string Access { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl => $"/images/offers/{this.Vertical}.jpg";
        public string PayPerAction { get; set; }
        public string PayPerClick { get; set; }
        public string PayPerLead { get; set; }
        public string PayType { get; set; }
        public string Language { get; set; }
        public string CountryTier { get; set; }
        public string Vertical { get; set; }
        public string Device { get; set; }
        public string RedirectUrl { get; set; }
        public string LeadPostbackUrl { get; set; }
        public string FtdPostbackUrl { get; set; }
        public bool IsVip { get; set; }
        public IEnumerable<OffersDetailsLandingPage> LandingPages { get; set; }
    }
}
