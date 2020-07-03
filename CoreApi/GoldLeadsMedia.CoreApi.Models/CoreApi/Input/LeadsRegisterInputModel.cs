namespace GoldLeadsMedia.CoreApi.Models.CoreApi.Input
{
    using System.ComponentModel.DataAnnotations;

    public class LeadsRegisterInputModel
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        [RegularExpression(@"^\d+$", ErrorMessage = "Invalid phone number!")]
        public string PhoneNumber { get; set; }
        [Required]
        public string CountryName { get; set; }
        [Required]
        public string ClickRegistrationId { get; set; }
    }
}
