namespace GoldLeadsMedia.Web.Models.InputModels
{
    using System.ComponentModel.DataAnnotations;

    public class AdministratorsCreateOfferInputModel
    {
        [Required]
        [MaxLength(100)]
        public string Number { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [Required]
        [MaxLength(400)]
        public string Description { get; set; }
        public int TierCountryId { get; set; }
        public int VerticalId { get; set; }
        public int PayTypeId { get; set; }
        public int TargetDeviceId { get; set; }
        public int AccessId { get; set; }
        [Required]
        [MaxLength(400)]
        public string ActionFlow { get; set; }
        public decimal? PayPerAction { get; set; }
        public decimal? PayPerLead { get; set; }
        public decimal? PayPerClick { get; set; }
        public int LanguageId { get; set; }
    }
}
