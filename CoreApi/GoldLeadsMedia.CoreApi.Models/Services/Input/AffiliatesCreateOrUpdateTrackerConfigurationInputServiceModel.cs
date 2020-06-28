namespace GoldLeadsMedia.CoreApi.Models.Services.Input
{
    public class AffiliatesCreateOrUpdateTrackerConfigurationInputServiceModel
    {
        public string AffiliateId { get; set; }
        public string LeadPostbackUrl { get; set; }
        public string FtdPostbackUrl { get; set; }
    }
}
