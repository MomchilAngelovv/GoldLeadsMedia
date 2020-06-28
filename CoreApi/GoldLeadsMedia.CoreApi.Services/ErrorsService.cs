namespace GoldLeadsMedia.CoreApi.Services
{
    using System;
    using System.Threading.Tasks;

    using GoldLeadsMedia.Database;
    using GoldLeadsMedia.Database.Models;
    using System.Collections.Generic;
    using System.Linq;
    using GoldLeadsMedia.CoreApi.Services.Common;
    using GoldLeadsMedia.CoreApi.Models.Services.Input;

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

            await db.SendLeadErrors.AddAsync(sendLeadError);
            await db.SaveChangesAsync();

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

            await db.FtdScanErrors.AddAsync(ftdScanError);
            await db.SaveChangesAsync();

            return ftdScanError;
        }

        public IEnumerable<DeveloperError> GetDeveloperErrors()
        {
            return db.DeveloperErrors
                .OrderByDescending(developerError => developerError.CreatedOn)
                .Take(10);
        }

        public IEnumerable<FtdScanError> GetFtdScanErrors()
        {
            return db.FtdScanErrors.ToList();
        }

        public IEnumerable<SendLeadError> GetSendLeadsErrors()
        {
            return db.SendLeadErrors.ToList();
        }
    }
}
