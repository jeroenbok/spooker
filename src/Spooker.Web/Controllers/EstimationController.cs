using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Spooker.Web.Domain;

namespace Spooker.Web.Controllers
{
    public class EstimationController : Controller
    {
        public ActionResult Estimate(Guid userId)
        {
            if (userId == Guid.Empty || !RoundKeeper.CurrentRound.Partipants.Any(p => p.UserId == userId))
            {
                return RedirectToAction("Index", "Register");
            }

            ViewBag.TotalParticipants = RoundKeeper.CurrentRound.Partipants.Count();
            ViewBag.UserName = RoundKeeper.CurrentRound.Partipants.Single(p => p.UserId == userId).Name;
            ViewBag.UserId = userId;
            return View(new EstimationForm(userId));
        }

        [HttpPost]
        public ActionResult Estimate(EstimationForm form)
        {
            var estimate = (StoryPoints) Enum.Parse(typeof (StoryPoints), form.Estimate);
            RoundKeeper.CurrentRound.RegisterParticipantEstimate(form.UserId, estimate);

            return RedirectToAction("Estimates");
        }

        public ActionResult Estimates()
        {
            return View("Estimates", RoundKeeper.CurrentRound.Status);
        }
    }
}