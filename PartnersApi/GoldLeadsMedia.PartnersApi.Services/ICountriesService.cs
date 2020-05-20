using GoldLeadsMedia.Database.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoldLeadsMedia.PartnersApi.Services
{
    public interface ICountriesService
    {
        Country GetBy(string name);
    }
}
