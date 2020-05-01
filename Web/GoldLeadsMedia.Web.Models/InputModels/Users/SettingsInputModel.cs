namespace GoldLeadsMedia.Web.Models.InputModels.Users
{
    using System.ComponentModel.DataAnnotations;

    public class SettingsInputModel
    {
        [Required]
        public string Email { get; set; }
        public string Skype { get; set; }
        public string Experience { get; set; }
    }
}
