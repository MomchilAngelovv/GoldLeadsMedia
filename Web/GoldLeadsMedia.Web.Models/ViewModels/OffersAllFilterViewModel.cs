namespace GoldLeadsMedia.Web.Models.ViewModels
{
    using System.Collections.Generic;

    public class OffersAllFilterViewModel
    {
        public string NumberOrName { get; set; }
        public string Country { get; set; }
        public int VerticalId { get; set; }
        public int PayTypeId { get; set; }
        public int DeviceId { get; set; }
        public int AccessId { get; set; }

        public IEnumerable<OffersAllOffer> Offers { get; set; }
    }
}
