using GoldLeadsMedia.CoreApi.Models.ServiceModels;
using GoldLeadsMedia.CoreApi.Models.ServicesModels.InputModels;
using GoldLeadsMedia.Database.Models;
using System.Threading.Tasks;

namespace GoldLeadsMedia.CoreApi.Services.Application.Common
{
    public interface IErrorsService
    {
        Task<SendLeadError> RegisterLeadErrorAsync(ErrorsRegisterLeadErrorInputServiceModel serviceModel);
        Task<FtdScanError> RegisterFtdScanErrorAsync(ErrorsRegisterFtdScanErrorInputServiceModel serviceModel);
        Task<DeveloperError> RegisterDeveloperErrorAsync(ErrorsRegisterDeveloperErrorInputServiceModel serviceModel);
    }
}
