namespace GoldLeadsMedia.Web.Models.ViewModels
{
    using System.Collections.Generic;

    public class OffersDetailsViewModel
    {
        public string Id { get; set; }
        public string Number { get; set; }
        public string Access { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl => $"/images/offers/{Id}.jpg";
        public decimal PayOut { get; set; }
        public decimal EarningPerClick { get; set; }
        public string PaymentType { get; set; }
        public string Language { get; set; }
        public string Country { get; set; }
        public string Vertical { get; set; }
        public string Device { get; set; }
        public string RedirectUrl { get; set; }
        public string PostbackUrl { get; set; }
        public bool IsVip => Access == "Vip";
        public IEnumerable<OffersDetailsLandingPage> LandingPages { get; set; }
    }
}
