namespace GoldLeadsMedia.Web.Models.ViewModels
{
    using System.Collections.Generic;

    public class OffersDashboardViewModel
    {
        public IEnumerable<OffersDashboardOfferGroup> OfferGroups { get; set; }
        public IEnumerable<OffersDashboardOffer> Offers { get; set; }
    }
}
