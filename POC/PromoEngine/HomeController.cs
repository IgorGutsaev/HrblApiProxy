using AspNetCoreHero.ToastNotification.Abstractions;
using Filuet.Hrbl.Ordering.Abstractions.Helpers;
using Filuet.Hrbl.Ordering.Abstractions.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

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
            string testUid = promo.Count > 0 ? promo.First().TestId : string.Empty;

            if (string.IsNullOrEmpty(testUid) || !promo.Any())
                return StatusCode(500, $"Unable to find test id {testUid}");

            if (promo.First().State == PromotionsState.Selection)
            {
                foreach (var p in ServerState.PromotionTests[testUid])
                {
                    Promotion modified = promo.FirstOrDefault(x => x.RuleId == p.RuleId);
                    if (modified != null)
                        p.ApplySelection(modified);
                }

                ServerState.PromotionTests[testUid].ForEach(x => x.State = PromotionsState.Verification);
            }

            return View("Promotions", ServerState.PromotionTests[testUid]);
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
