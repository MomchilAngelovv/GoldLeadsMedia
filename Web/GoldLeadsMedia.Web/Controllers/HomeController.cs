using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using GoldLeadsMedia.Web.Models;
using GoldLeadsMedia.Database;
using GoldLeadsMedia.Database.Models;

namespace GoldLeadsMedia.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly GoldLeadsMediaDbContext db;

        public HomeController(
            ILogger<HomeController> logger,
            GoldLeadsMediaDbContext db)
        {
            _logger = logger;
            this.db = db;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Privacy()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Error()
        {
            return this.View();
        }
    }
}
