namespace GoldLeadsMedia.Web.Models.InputModels
{
    using System.ComponentModel.DataAnnotations;

    //TODO Check form input password fields to show **** insted of actual password
    public class AdministratorsRegisterAffiliateInputModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
