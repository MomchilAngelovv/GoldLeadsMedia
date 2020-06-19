namespace GoldLeadsMedia.AffiliatesApi.Models.InputModels
{
    using System.ComponentModel.DataAnnotations;

    public class LeadsRegisterInputModel
    {
        [Required]
        public string AffiliateId { get; set; }
        [Required]
        public string OfferId { get; set; }
        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }
        [Required]
        [MaxLength(100)]
        public string Password { get; set; }
        [Required]
        [MaxLength(100)]
        public string Email { get; set; }
        [Required]
        [MaxLength(100)]
        public string PhoneNumber { get; set; }
        [Required]
        public string CountryName { get; set; }
    }
}
