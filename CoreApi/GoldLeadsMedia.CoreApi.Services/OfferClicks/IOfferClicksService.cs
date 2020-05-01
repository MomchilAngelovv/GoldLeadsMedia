namespace GoldLeadsMedia.CoreApi.Services.OfferClicks
{
    using System.Threading.Tasks;

    using GoldLeadsMedia.Database.Models;
    using GoldLeadsMedia.CoreApi.Models.InputModels.OfferClicks;

    public interface IOfferClicksService
    {
        Task<OfferClick> RegisterClickAsync(RegisterOfferClickInputModel inputModel);
    }
}
