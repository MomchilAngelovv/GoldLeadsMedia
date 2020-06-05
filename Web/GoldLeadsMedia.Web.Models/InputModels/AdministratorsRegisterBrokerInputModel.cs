namespace GoldLeadsMedia.Web.Models.InputModels
{
    using System.ComponentModel.DataAnnotations;

    public class AdministratorsRegisterBrokerInputModel
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
    }
}
