namespace GoldLeadsMedia.CoreApi.Services.Application.Common
{
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using GoldLeadsMedia.Database.Models;
    using GoldLeadsMedia.CoreApi.Models.InputModels;
    using GoldLeadsMedia.CoreApi.Models.ServiceModels;

    public interface IOffersService
    {
        IEnumerable<Offer> GetAll(OffersGetAllFilterServiceModel filterServiceModel);
        Task<Offer> CreateAsync(OffersCreateServiceModel serviceModel);
        Offer GetBy(string id);
        Task<int> AssignLandingPagesAsync(OffersAssignLandingPagesServiceModel serviceModel);
    }
}
