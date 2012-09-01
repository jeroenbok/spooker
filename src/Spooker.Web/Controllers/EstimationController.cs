using System;
using System.Linq;
using System.Web.Mvc;
using Spooker.Web.Domain;
using Spooker.Web.Infrastructure.Cookies;

namespace Spooker.Web.Controllers
{
    public class EstimationController : Controller
    {
        private readonly IAppCookies _appCookies;

        public EstimationController(IAppCookies appCookies)
        {
            _appCookies = appCookies;
        }

        public ActionResult Estimate()
        {
            Guid userId = _appCookies.UserId;
            if (userId == Guid.Empty || RoundKeeper.CurrentRound.Partipants.All(p => p.UserId != userId))
            {
                return RedirectToAction("Index", "Register");
            }

            ViewBag.TotalParticipants = RoundKeeper.CurrentRound.Partipants.Count();
            ViewBag.UserName = RoundKeeper.CurrentRound.Partipants.Single(p => p.UserId == userId).Name;
            return View(new EstimationForm());
        }

        [HttpPost]
        public ActionResult Estimate(EstimationForm form)
        {
            var estimate = (StoryPoints) Enum.Parse(typeof (StoryPoints), form.Estimate);
            RoundKeeper.CurrentRound.RegisterParticipantEstimate(_appCookies.UserId, estimate);

            return RedirectToAction("Estimates");
        }

        public ActionResult Estimates()
        {
            return View("Estimates", RoundKeeper.CurrentRound.Status);
        }
    }
}