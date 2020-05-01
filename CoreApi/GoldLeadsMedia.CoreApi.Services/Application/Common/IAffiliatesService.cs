using GoldLeadsMedia.Database.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoldLeadsMedia.CoreApi.Services.Application.Common
{
    public interface IAffiliatesService
    {
        IEnumerable<Lead> GetLeadsBy(string affiliateId);
    }
}
