namespace GoldLeadsMedia.Web.Models.InputModels
{
    using System.ComponentModel.DataAnnotations;

    public class ClicksRegisterInputModel
    {
        [Required]
        public string OfferId { get; set; }
        [Required]
        public string LandingPageId { get; set; }
        [Required]
        public string AffiliateId { get; set; }
        public string ClickId { get; set; }
    }
}
