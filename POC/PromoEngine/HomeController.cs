using Filuet.Hrbl.Ordering.Abstractions.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Filuet.Hrbl.Ordering.POC.PromoEngine
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RunMocked()
        {
            ServerState.DataSource = DataSource.Mock;
            return null;
        }

        [HttpPost]
        public IActionResult RunCached()
        {
            ServerState.DataSource = DataSource.Cached;
            return null;
        }
    }
}
