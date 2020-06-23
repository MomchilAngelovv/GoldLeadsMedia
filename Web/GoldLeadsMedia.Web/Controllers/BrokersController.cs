using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoldLeadsMedia.Web.Controllers
{
    public class BrokersController : Controller
    {
        public IActionResult All()
        {
            return this.View();
        }
    }
}
