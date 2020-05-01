namespace GoldLeadsMedia.CoreApi.Services.OfferGroups
{
    using System.Collections.Generic;

    using GoldLeadsMedia.Database.Models;

    public interface IOfferGroupsService
    {
        IEnumerable<OfferGroup> GetAll();
    }
}
