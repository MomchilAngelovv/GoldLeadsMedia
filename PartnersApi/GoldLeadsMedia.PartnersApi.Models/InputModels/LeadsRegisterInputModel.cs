namespace GoldLeadsMedia.PartnersApi.Models.InputModels
{
    using System.ComponentModel.DataAnnotations;

    public class LeadsRegisterInputModel
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        public string OfferId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string CountryName { get; set; }
    }
}
