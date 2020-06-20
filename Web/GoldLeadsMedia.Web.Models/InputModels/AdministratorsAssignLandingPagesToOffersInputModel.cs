namespace GoldLeadsMedia.Web.Models.InputModels
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class AdministratorsAssignLandingPagesToOffersInputModel
    {
        [Required]
        public string OfferId { get; set; }
        public IEnumerable<string> LandingPageIds { get; set; }
    }
}
