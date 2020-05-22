namespace GoldLeadsMedia.CoreApi.Services.Partners.Common
{
    using System;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    public interface IBroker
    {
        Task<int> SendLeadsAsync(IEnumerable<string> leadIds, string brokerId, string partnerOfferId);
        Task<int> FtdScanAsync(DateTime from, DateTime to);
    } 
}
