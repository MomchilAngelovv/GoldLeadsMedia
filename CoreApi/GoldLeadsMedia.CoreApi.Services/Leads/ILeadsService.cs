namespace GoldLeadsMedia.CoreApi.Services.Leads
{
    using System.Collections.Generic;

    using GoldLeadsMedia.Database.Models;

    public interface ILeadsService
    {
        IEnumerable<Lead> GetAllBy(string userId);
    }
}
