namespace GoldLeadsMedia.CoreApi.Services.Common
{
    using System.Threading.Tasks;

    using GoldLeadsMedia.Database.Models;
    using GoldLeadsMedia.CoreApi.Models.Services.Input;

    public interface IClicksRegistrationsService
    {
        Task<ClickRegistration> RegisterAsync(ClicksRegisterInputServiceModel serviceModel);
        ClickRegistration GetBy(string clickRegistrationId);
    }
}
