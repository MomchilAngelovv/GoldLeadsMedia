namespace GoldLeadsMedia.PartnersApi.Models.InputModels
{
    using System.ComponentModel.DataAnnotations;

    public class LeadsRegisterInputModel
    {
        //Sender information UserId = the id of the sending Affilaite and offerId = the id of the offer which leads will be send
        [Required]
        public string UserId { get; set; }
        [Required]
        public string OfferId { get; set; }

        //Lead data
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
