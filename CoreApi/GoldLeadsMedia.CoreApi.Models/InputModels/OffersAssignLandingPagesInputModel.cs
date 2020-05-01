namespace GoldLeadsMedia.CoreApi.Models.InputModels
{
    using System.Collections.Generic;

    public class OffersAssignLandingPagesInputModel
    {
        public string OfferId { get; set; }
        public IEnumerable<string> LandingPageIds { get; set; }
    }
}
