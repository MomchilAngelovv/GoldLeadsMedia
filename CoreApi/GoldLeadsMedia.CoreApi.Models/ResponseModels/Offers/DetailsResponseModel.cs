namespace GoldLeadsMedia.CoreApi.Models.ResponseModels.Offers
{
    using System.Collections.Generic;

    public class DetailsResponseModel
    {
        public string Id { get; set; }
        public string Number { get; set; }
        public string Description { get; set; }
        public string Access { get; set; }
        public string Name { get; set; }
        public decimal PayOut { get; set; }
        public decimal EarningPerClick { get; set; }
        public string PaymentType { get; set; }
        public string Language { get; set; }
        public string Country { get; set; }
        public string Vertical { get; set; }
        public string Device { get; set; }
        public IEnumerable<DetailsLandingPage> LandingPages { get; set; }
    }
}
