namespace GoldLeadsMedia.CoreApi.Services.Common
{
    using System.Collections.Generic;

    using GoldLeadsMedia.Database.Models;
    using System.Threading.Tasks;
    using GoldLeadsMedia.CoreApi.Models.Services.Input;
    using GoldLeadsMedia.CoreApi.Models.Services.Output;

    public interface IAffiliatesService
    {
        Task<IEnumerable<GoldLeadsMediaUser>> GetAllAsync();
        IEnumerable<Lead> GetLeadsBy(string affiliateId);
        IEnumerable<Offer> GetOffersBy(string affiliateId);
        AffiliatesGetPaymentsStatusByOutputServiceModel GetPaymentsStatusBy(string affiliateId);
        TrackerConfiguration GetTrackerSettings(string affiliateId);
        Task<string> CreateOrUpdateTrackerConfiguration(AffiliatesCreateOrUpdateTrackerConfigurationInputServiceModel serviceModel);
    }
}
