namespace GoldLeadsMedia.CoreApi.Services.Accesses
{
    using System.Collections.Generic;

    using GoldLeadsMedia.Database.Models;

    public interface IAccessesService
    {
        IEnumerable<Access> GetAll();
    }
}
