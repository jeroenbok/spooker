using System;
using System.Linq;
using System.Web.Mvc;
using Spooker.Web.Domain;
using Spooker.Web.Infrastructure.Cookies;

namespace Spooker.Web.Controllers
{
    public class EstimationController : Controller
    {
        private readonly RoundKeeper _roundKeeper = RoundKeeper.Factory.GetInstance();
        private readonly IAppCookies _appCookies;


        public EstimationController(IAppCookies appCookies)
        {
            _appCookies = appCookies;
        }

        public ActionResult Estimate()
        {
            Guid participantId = _appCookies.ParticipantId;
            if (participantId == Guid.Empty || !_roundKeeper.ActiveRound.HasParticipant(participantId))
            {
                return RedirectToAction("Index", "Register");
            }

            ViewBag.TotalParticipants = _roundKeeper.ActiveRound.Status.ParticipantCount;
            ViewBag.UserName = _roundKeeper.ActiveRound.ParticipantById(participantId);
            return View(new EstimationForm());
        }

        [HttpPost]
        public ActionResult Estimate(EstimationForm form)
        {
            var storyPoints = (StoryPoints) Enum.Parse(typeof (StoryPoints), form.Estimate);
            _roundKeeper.ActiveRound.ParticipantById(_appCookies.ParticipantId).Estimate(storyPoints);

            return RedirectToAction("Estimates");
        }

        public ActionResult Estimates()
        {
            return View("Estimates", _roundKeeper.ActiveRound.Status);
        }
    }
}