namespace GoldLeadsMedia.CoreApi.Services.Partners.Common
{
    using System.Collections.Generic;

    public interface IPartner
    {
        int SendLeads(IEnumerable<string> leadIds);
        int FtdScan();
    } 
}
