namespace GoldLeadsMedia.CoreApi.Services.Application.Common
{
    using System.Collections.Generic;

    using GoldLeadsMedia.Database.Models;

    public interface IAccessesService
    {
        IEnumerable<Access> GetAll();
    }
}
