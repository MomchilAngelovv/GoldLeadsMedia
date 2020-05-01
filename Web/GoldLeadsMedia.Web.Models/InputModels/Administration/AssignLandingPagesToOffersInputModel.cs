namespace GoldLeadsMedia.Web.Models.InputModels.Administration
{
    using System.Collections.Generic;

    public class AssignLandingPagesToOffersInputModel
    {
        public string OfferId { get; set; }
        public IEnumerable<string> LandingPageIds { get; set; }
    }
}
