namespace GoldLeadsMedia.CoreApi.Models.ResponseModels.Offers
{
    public class OfferResponseModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Number { get; set; }
        public string Country { get; set; }
        public string TargetDevice { get; set; }
        public decimal EarningPerClick { get; set; }
        public string Language { get; set; }
        public string PaymentType { get; set; }
        public decimal PayOut { get; set; }
        public string Vertical { get; set; }
        public string Access { get; set; }
    }
}
