namespace GoldLeadsMedia.CoreApi.Services.Application.Common
{
    using System.Collections.Generic;

    using GoldLeadsMedia.Database.Models;
    using GoldLeadsMedia.CoreApi.Models.ServicesModels.OutputModels;
    using GoldLeadsMedia.CoreApi.Models.ServicesModels.InputModels;
    using System.Threading.Tasks;

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
