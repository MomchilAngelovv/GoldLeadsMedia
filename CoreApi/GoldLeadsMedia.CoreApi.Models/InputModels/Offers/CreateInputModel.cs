namespace GoldLeadsMedia.CoreApi.Models.InputModels.Offers
{
    public class CreateInputModel
    {
        public string Number { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CountryId { get; set; }
        public int VerticalId { get; set; }
        public int PaymentTypeId { get; set; }
        public int TargetDeviceId { get; set; }
        public int AccessId { get; set; }
        public string ActionFlow { get; set; }
        public decimal PayOut { get; set; }
        public decimal DailyCap { get; set; }
        public decimal EarningPerClick { get; set; }
        public int LanguageId { get; set; }
        public string CreatedBy { get; set; }
    }
}
