using AspNetCoreHero.ToastNotification.Abstractions;
using Filuet.Hrbl.Ordering.Abstractions.Models;
using Filuet.Hrbl.Ordering.Abstractions.Serializers;
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
        private readonly INotyfService _toastNotification;

        public HomeController(INotyfService toastNotification)
        {
            _toastNotification = toastNotification;
        }
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Promotions(string uid)
        {
            List<Promotion> promotions = ServerState.PromotionTests[uid];
            return View(promotions);
        }

        //[HttpPost]
        //public IActionResult Toast(string msg)
        //{
        //    _toastNotification.Success("FFfff");
        //    return Ok();
        //}

        [HttpPost]
        public IActionResult Index(List<Promotion> promo)
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
