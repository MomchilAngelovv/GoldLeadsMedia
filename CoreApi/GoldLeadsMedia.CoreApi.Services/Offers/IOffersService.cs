namespace GoldLeadsMedia.CoreApi.Services.Offers
{
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using GoldLeadsMedia.Database.Models;
    using GoldLeadsMedia.CoreApi.Models.InputModels.Offers;

    public interface IOffersService
    {
        IEnumerable<Offer> GetAll();
        Task<Offer> CreateAsync(CreateInputModel inputModel);
        Offer GetBy(string id);
        Task<int> AssignLandingPagesAsync(AssignLandingPagesInputModel inputModel);
    }
}
