namespace GoldLeadsMedia.Web.Models.ViewModels
{
    using System.Collections.Generic;

    //TODO remove number from search
    //TODO remove country id and implement country tier id
    public class OffersAllFilterViewModel
    {
        public string NumberOrName { get; set; }
        public int? CountryId { get; set; }
        public int? VerticalId { get; set; }
        public int? PayTypeId { get; set; }
        public int? DeviceId { get; set; }
        public int? AccessId { get; set; }
        public IEnumerable<OffersAllOffer> Offers { get; set; }
    }
}
