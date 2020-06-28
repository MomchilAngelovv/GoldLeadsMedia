namespace GoldLeadsMedia.CoreApi.Models.Services.Input
{
    using System.Collections.Generic;

    public class OffersAssignLandingPagesInputServiceModel
    {
        public string OfferId { get; set; }
        public IEnumerable<string> LandingPageIds { get; set; }
    }
}
