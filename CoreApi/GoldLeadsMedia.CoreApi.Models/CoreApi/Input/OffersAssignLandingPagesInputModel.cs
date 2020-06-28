namespace GoldLeadsMedia.CoreApi.Models.CoreApi.Input
{
    using System.Collections.Generic;

    public class OffersAssignLandingPagesInputModel
    {
        public string OfferId { get; set; }
        public IEnumerable<string> LandingPageIds { get; set; }
    }
}
