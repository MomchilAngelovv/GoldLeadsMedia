namespace GoldLeadsMedia.CoreApi.Models.InputModels
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
        public string PhoneNumber { get; set; }
        [Required]
        public string CountryName { get; set; }
        [Required]
        public string ClickRegistrationId { get; set; }
    }
}
