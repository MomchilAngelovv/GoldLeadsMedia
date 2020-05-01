using GoldLeadsMedia.Database.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoldLeadsMedia.CoreApi.Services.LandingPages
{
    public interface ILandingPagesService
    {
        IEnumerable<LandingPage> GetAll();
    }
}
