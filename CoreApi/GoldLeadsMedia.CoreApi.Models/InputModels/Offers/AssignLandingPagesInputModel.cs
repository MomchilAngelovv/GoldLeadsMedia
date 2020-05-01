namespace GoldLeadsMedia.CoreApi.Models.InputModels.Offers
{
    using System.Collections.Generic;

    public class AssignLandingPagesInputModel
    {
        public string OfferId { get; set; }
        public IEnumerable<string> LandingPageIds { get; set; }
    }
}
