namespace GoldLeadsMedia.CoreApi.Services.Application.Common
{
    using System.Collections.Generic;

    using GoldLeadsMedia.Database.Models;
    using GoldLeadsMedia.CoreApi.Models.ServicesModels.OutputModels;
    using System.Threading.Tasks;
    using GoldLeadsMedia.CoreApi.Models.Services.Input;

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
