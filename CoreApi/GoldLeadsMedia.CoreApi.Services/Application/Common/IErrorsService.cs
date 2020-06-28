namespace GoldLeadsMedia.CoreApi.Services.Application.Common
{
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using GoldLeadsMedia.Database.Models;
    using GoldLeadsMedia.CoreApi.Models.ServicesModels.InputModels;

    public interface IErrorsService
    {
        Task<SendLeadError> RegisterSendLeadErrorAsync(ErrorsRegisterSendLeadErrorInputServiceModel serviceModel);
        Task<FtdScanError> RegisterFtdScanErrorAsync(ErrorsRegisterFtdScanErrorInputServiceModel serviceModel);
        Task<DeveloperError> RegisterDeveloperErrorAsync(ErrorsRegisterDeveloperErrorInputServiceModel serviceModel);
        IEnumerable<DeveloperError> GetDeveloperErrors();
        IEnumerable<FtdScanError> GetFtdScanErrors();
        IEnumerable<SendLeadError> GetSendLeadsErrors();
    }
}
