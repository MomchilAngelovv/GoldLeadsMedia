namespace GoldLeadsMedia.CoreApi.Services.Application.Common
{
    using System.Threading.Tasks;

    using GoldLeadsMedia.Database.Models;
    using GoldLeadsMedia.CoreApi.Models.ServicesModels.InputModels;
    using System.Collections.Generic;

    public interface IErrorsService
    {
        Task<SendLeadError> RegisterLeadErrorAsync(ErrorsRegisterLeadErrorInputServiceModel serviceModel);
        Task<FtdScanError> RegisterFtdScanErrorAsync(ErrorsRegisterFtdScanErrorInputServiceModel serviceModel);
        Task<DeveloperError> RegisterDeveloperErrorAsync(ErrorsRegisterDeveloperErrorInputServiceModel serviceModel);
        IEnumerable<DeveloperError> GetDeveloperErrors();
        IEnumerable<FtdScanError> GetFtdScanErrors();
        IEnumerable<SendLeadError> GetSendLeadsErrors();
    }
}
