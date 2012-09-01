using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Spooker.Web.Domain;

namespace Spooker.Web.Controllers
{
    public class EstimationController : Controller
    {
        private readonly RoundKeeper _roundKeeper = RoundKeeper.Factory.GetInstance();

        public ActionResult Estimate()
        {
            Guid participantId = GetUserId();
            if (participantId == Guid.Empty || _roundKeeper.ActiveRound.HasParticipant(participantId))
            {
                return RedirectToAction("Index", "Register");
            }

            ViewBag.TotalParticipants = _roundKeeper.ActiveRound.Status.ParticipantCount;
            ViewBag.UserName = _roundKeeper.ActiveRound.ParticipantById(participantId);
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
            var storyPoints = (StoryPoints) Enum.Parse(typeof (StoryPoints), form.Estimate);
            _roundKeeper.ActiveRound.ParticipantById(GetUserId()).Estimate(storyPoints);

            return RedirectToAction("Estimates");
        }

        public ActionResult Estimates()
        {
            return View("Estimates", _roundKeeper.ActiveRound.Status);
        }
    }
}