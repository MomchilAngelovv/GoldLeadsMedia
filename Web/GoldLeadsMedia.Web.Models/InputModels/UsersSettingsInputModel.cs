namespace GoldLeadsMedia.Web.Models.InputModels
{
    using System.ComponentModel.DataAnnotations;

    public class UsersSettingsInputModel
    {
        [Required]
        public string Email { get; set; }
        public string Skype { get; set; }
        public string Experience { get; set; }
    }
}
