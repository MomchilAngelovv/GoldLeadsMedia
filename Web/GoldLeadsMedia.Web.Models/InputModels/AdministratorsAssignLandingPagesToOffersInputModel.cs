namespace GoldLeadsMedia.Web.Models.InputModels
{
    using System.Collections.Generic;

    public class AdministratorsAssignLandingPagesToOffersInputModel
    {
        public string OfferId { get; set; }
        public IEnumerable<string> LandingPageIds { get; set; }
    }
}
