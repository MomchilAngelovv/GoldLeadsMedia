namespace GoldLeadsMedia.Web.Models.ViewModels
{
    public class OffersAllOffer
    {
        public string Id { get; set; }
        public string Number { get; set; }
        public string Name { get; set; }
        public string ActionFlow { get; set; }
        public decimal? PayPerAction { get; set; }
        public decimal? PayPerLead { get; set; }
        public decimal? PayPerClick { get; set; }
        public string TierCountry { get; set; }
        public string Language { get; set; }
        public string Vertical { get; set; }
        public string PayType { get; set; }
        public string Access { get; set; }
    }
}
