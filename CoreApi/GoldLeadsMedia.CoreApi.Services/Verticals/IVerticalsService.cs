using GoldLeadsMedia.Database.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoldLeadsMedia.CoreApi.Services.Verticals
{
    public interface IVerticalsService
    {
        IEnumerable<Vertical> GetAll();
    }
}
