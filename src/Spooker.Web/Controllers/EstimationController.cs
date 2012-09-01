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
            ViewBag.StoryPointsSelectList = StoryPoints.QuestionMark.ToSelectList();
            return View();
        }

        public ActionResult Estimates()
        {
            return View(RoundKeeper.CurrentRound.Status);
        }
    }

    public static class EnumExtensions
    {
        public static SelectList ToSelectList<TEnum>(this TEnum enumObj)
        {
            var values = from TEnum e in Enum.GetValues(typeof(TEnum))
                         select new { Id = e, Name = e.ToString() };

            return new SelectList(values, "Id", "Name", enumObj);
        }
    }
}