namespace GoldLeadsMedia.CoreApi.Services.Partners.Common
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IPartner
    {
        Task<int> SendLeadsAsync(IEnumerable<string> leadIds, string partnerId);
        int FtdScan();
    } 
}
