namespace GoldLeadsMedia.CoreApi.Services.Application.Common
{
    using System.Collections.Generic;

    using GoldLeadsMedia.Database.Models;
    using GoldLeadsMedia.CoreApi.Models.ServicesModels.OutputModels;

    public interface IAffiliatesService
    {
        IEnumerable<Lead> GetLeadsBy(string affiliateId);
        AffiliatesGetPaymentsStatusByOutputServiceModel GetPaymentsStatusBy(string affiliateId);
    }
}
