namespace GoldLeadsMedia.CoreApi.Models.CoreApi.Input
{
    public class OffersAllFilterModel
    {
        public string Name { get; set; }
        public int? CountryTierId { get; set; }
        public int? VerticalId { get; set; }
        public int? PayTypeId { get; set; }
        public int? TargetDeviceId { get; set; }
        public int? AccessId { get; set; }
        public int? GroupId { get; set; }
    }
}
