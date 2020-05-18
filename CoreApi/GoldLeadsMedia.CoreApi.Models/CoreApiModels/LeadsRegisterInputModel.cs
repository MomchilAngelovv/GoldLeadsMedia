namespace GoldLeadsMedia.CoreApi.Models.InputModels
{
    using System.ComponentModel.DataAnnotations;

    public class LeadsRegisterInputModel
    {
        //SenderId and OfferId comes only from API lead register
        public string SenderId { get; set; }
        public string OfferId { get; set; }

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

        //ClickId comes only from landing pages register
        public string ClickId { get; set; }
    }
}
