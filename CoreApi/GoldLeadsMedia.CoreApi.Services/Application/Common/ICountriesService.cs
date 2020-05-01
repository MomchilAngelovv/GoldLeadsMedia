using GoldLeadsMedia.Database.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GoldLeadsMedia.CoreApi.Services.Application.Common
{
    public interface ICountriesService
    {
        IEnumerable<Country> GetAll();
    }
}
