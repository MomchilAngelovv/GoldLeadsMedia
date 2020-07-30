namespace GoldLeadsMedia.CoreApi.Models.Services.Input
{
    public class OffersGetAllFilterInputServiceModel
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
