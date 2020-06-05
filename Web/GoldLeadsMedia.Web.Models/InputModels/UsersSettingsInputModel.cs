namespace GoldLeadsMedia.Web.Models.InputModels
{
    using System.ComponentModel.DataAnnotations;

    public class UsersSettingsInputModel
    {
        [Required]
        public string Email { get; set; }
        [MaxLength(100)]
        public string Skype { get; set; }
        [MaxLength(100)]
        public string Experience { get; set; }
    }
}
