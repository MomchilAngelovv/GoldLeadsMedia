namespace GoldLeadsMedia.CoreApi.Services.Application
{
    using System;
    using System.Threading.Tasks;

    using GoldLeadsMedia.Database;
    using GoldLeadsMedia.Database.Models;
    using GoldLeadsMedia.CoreApi.Services.Application.Common;
    using GoldLeadsMedia.CoreApi.Models.ServicesModels.InputModels;
    using System.Collections.Generic;
    using System.Linq;

    public class ErrorsService : IErrorsService
    {
        private readonly GoldLeadsMediaDbContext db;

        public ErrorsService(
            GoldLeadsMediaDbContext db)
        {
            this.db = db;
        }

        public async Task<DeveloperError> RegisterDeveloperErrorAsync(ErrorsRegisterDeveloperErrorInputServiceModel serviceModel)
        {
            var developerError = new DeveloperError
            {
                Method = serviceModel.Method,
                Path = serviceModel.Path,
                Message = serviceModel.Message,
                StackTrace = serviceModel.StackTrace,
                UserId = serviceModel.LoggedUserId,
                Information = serviceModel.Information
            };

            await db.DeveloperErrors.AddAsync(developerError);
            await db.SaveChangesAsync();

            return developerError;
        }
        public async Task<SendLeadError> RegisterSendLeadErrorAsync(ErrorsRegisterSendLeadErrorInputServiceModel serviceModel)
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

        public IEnumerable<DeveloperError> GetDeveloperErrors()
        {
            return this.db.DeveloperErrors
                .OrderByDescending(developerError => developerError.CreatedOn)
                .Take(10);
        }

        public IEnumerable<FtdScanError> GetFtdScanErrors()
        {
            return this.db.FtdScanErrors.ToList();
        }

        public IEnumerable<SendLeadError> GetSendLeadsErrors()
        {
            return this.db.SendLeadErrors.ToList();
        }
    }
}
