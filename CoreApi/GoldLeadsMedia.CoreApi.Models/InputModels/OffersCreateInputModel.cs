namespace GoldLeadsMedia.CoreApi.Models.InputModels
{
    public class OffersCreateInputModel
    {
        public string Number { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CountryId { get; set; }
        public int VerticalId { get; set; }
        public int PayTypeId { get; set; }
        public int TargetDeviceId { get; set; }
        public int AccessId { get; set; }
        public string ActionFlow { get; set; }
        public decimal PayOut { get; set; }
        public decimal DailyCap { get; set; }
        public decimal PayPerClick { get; set; }
        public int LanguageId { get; set; }
        public string CreatedByManagerId { get; set; }
    }
}
