namespace GoldLeadsMedia.CoreApi.Services.Partners.Common
{
    using System;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    public interface IPartner
    {
        Task<int> SendLeadsAsync(IEnumerable<string> leadIds, string partnerId, string partnerOfferId);
        Task<int> FtdScanAsync(DateTime from, DateTime to);
    } 
}
