namespace GoldLeadsMedia.CoreApi.Services.Common
{
    using System;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    public interface IBroker
    {
        Task<int> SendLeadsAsync(IEnumerable<string> leadIds);
        Task<int> FtdScanAsync(DateTime from, DateTime to);
    }
}
