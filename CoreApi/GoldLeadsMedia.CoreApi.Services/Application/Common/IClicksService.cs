namespace GoldLeadsMedia.CoreApi.Services.Application.Common
{
    using System.Threading.Tasks;

    using GoldLeadsMedia.Database.Models;
    using GoldLeadsMedia.CoreApi.Models.InputModels;
    using GoldLeadsMedia.CoreApi.Models.ServiceModels;

    public interface IClicksService
    {
        Task<Click> RegisterAsync(ClicksRegisterServiceModel serviceModel);
    }
}
