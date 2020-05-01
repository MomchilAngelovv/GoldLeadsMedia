namespace GoldLeadsMedia.Web.Models.InputModels.Administration
{
    using System.ComponentModel.DataAnnotations;

    public class CreateOfferInputModel
    {
        [Required]
        public string Number { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public int? CountryId { get; set; }
        [Required]
        public int? VerticalId { get; set; }
        [Required]
        public int? PaymentTypeId { get; set; }
        [Required]
        public int? TargetDeviceId { get; set; }
        [Required]
        public int? AccessId { get; set; }
        public string ActionFlow { get; set; }
        public decimal PayOut { get; set; }
        public decimal DailyCap { get; set; }
        public decimal EarningPerClick { get; set; }
        [Required]
        public int? LanguageId { get; set; }
    }
}
