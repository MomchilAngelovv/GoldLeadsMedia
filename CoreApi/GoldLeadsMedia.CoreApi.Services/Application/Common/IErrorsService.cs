namespace GoldLeadsMedia.CoreApi.Services.Application.Common
{
    using System.Threading.Tasks;

    using GoldLeadsMedia.Database.Models;
    using GoldLeadsMedia.CoreApi.Models.ServicesModels.InputModels;

    public interface IErrorsService
    {
        Task<SendLeadError> RegisterLeadErrorAsync(ErrorsRegisterLeadErrorInputServiceModel serviceModel);
        Task<FtdScanError> RegisterFtdScanErrorAsync(ErrorsRegisterFtdScanErrorInputServiceModel serviceModel);
        Task<DeveloperError> RegisterDeveloperErrorAsync(ErrorsRegisterDeveloperErrorInputServiceModel serviceModel);
    }
}
