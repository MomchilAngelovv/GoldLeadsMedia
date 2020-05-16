using GoldLeadsMedia.CoreApi.Models.ServicesModels.InputModels;
using GoldLeadsMedia.CoreApi.Services.Application.Common;
using GoldLeadsMedia.Database;
using GoldLeadsMedia.Database.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GoldLeadsMedia.CoreApi.Services.Application
{
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
            var leadError = new SendLeadError
            {
                LeadId = serviceModel.LeadId,
                PartnerId = serviceModel.PartnerId,
                Information = serviceModel.Information,
                Message = serviceModel.ErrorMessage
            };

            await this.db.LeadErrors.AddAsync(leadError);
            await this.db.SaveChangesAsync();

            return leadError;
        }
        public async Task<FtdScanError> RegisterFtdScanErrorAsync(ErrorsRegisterFtdScanErrorInputServiceModel serviceModel)
        {
            var ftdScanError = new FtdScanError
            {
                Message = serviceModel.Message,
                PartnerName = serviceModel.PartnerName,
                Information = serviceModel.Information,
            };

            await this.db.FtdScanErrors.AddAsync(ftdScanError);
            await this.db.SaveChangesAsync();

            return ftdScanError;
        }
    }
}
