using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Spooker.Web.Domain;

namespace Spooker.Web.Controllers
{
    public class EstimationController : Controller
    {
        public ActionResult Estimate()
        {
            Guid userId = GetUserId();
            if (userId == Guid.Empty || RoundKeeper.CurrentRound.Partipants.All(p => p.UserId != userId))
            {
                return RedirectToAction("Index", "Register");
            }

            ViewBag.TotalParticipants = RoundKeeper.CurrentRound.Partipants.Count();
            ViewBag.UserName = RoundKeeper.CurrentRound.Partipants.Single(p => p.UserId == userId).Name;
            return View(new EstimationForm());
        }

        public Guid GetUserId()
        {
            HttpCookie userCookie = ControllerContext.HttpContext.Request.Cookies["SpookerUserCookie"];
            if (userCookie == null)
                return Guid.Empty;
            Guid userId;
            if (Guid.TryParse(userCookie.Values["UserId"], out userId))
                return userId;
            return Guid.Empty;
        }

        [HttpPost]
        public ActionResult Estimate(EstimationForm form)
        {
            var estimate = (StoryPoints) Enum.Parse(typeof (StoryPoints), form.Estimate);
            RoundKeeper.CurrentRound.RegisterParticipantEstimate(GetUserId(), estimate);

            return RedirectToAction("Estimates");
            return View("Estimates", RoundKeeper.CurrentRound.Status);
        }

        public ActionResult Estimates()
        {
            return View("Estimates", RoundKeeper.CurrentRound.Status);
        }
    }
}