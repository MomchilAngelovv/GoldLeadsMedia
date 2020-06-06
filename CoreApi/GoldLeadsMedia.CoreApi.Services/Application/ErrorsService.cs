namespace GoldLeadsMedia.CoreApi.Services.Application
{
    using System;
    using System.Threading.Tasks;

    using GoldLeadsMedia.Database;
    using GoldLeadsMedia.Database.Models;
    using GoldLeadsMedia.CoreApi.Services.Application.Common;
    using GoldLeadsMedia.CoreApi.Models.ServicesModels.InputModels;

    public class ErrorsService : IErrorsService
    {
        private readonly GoldLeadsMediaDbContext db;

        public ErrorsService(
            GoldLeadsMediaDbContext db)
        {
            this.db = db;
        }

        public Task<DeveloperError> RegisterDeveloperErrorAsync(ErrorsRegisterDeveloperErrorInputServiceModel serviceModel)
        {
            throw new NotImplementedException();
        }
        public async Task<SendLeadError> RegisterLeadErrorAsync(ErrorsRegisterLeadErrorInputServiceModel serviceModel)
        {
            var sendLeadError = new SendLeadError
            {
                LeadId = serviceModel.LeadId,
                BrokerId = serviceModel.BrokerId,
                Information = serviceModel.Information,
                Message = serviceModel.ErrorMessage
            };

            await this.db.SendLeadErrors.AddAsync(sendLeadError);
            await this.db.SaveChangesAsync();

            return sendLeadError;
        }
        public async Task<FtdScanError> RegisterFtdScanErrorAsync(ErrorsRegisterFtdScanErrorInputServiceModel serviceModel)
        {
            var ftdScanError = new FtdScanError
            {
                Message = serviceModel.Message,
                BrokerId = serviceModel.BrokerId,
                Information = serviceModel.Information,
            };

            await this.db.FtdScanErrors.AddAsync(ftdScanError);
            await this.db.SaveChangesAsync();

            return ftdScanError;
        }
    }
}
