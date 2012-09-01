using System.Web.Mvc;
using Spooker.Web.Domain;
using Spooker.Web.Infrastructure;
using Spooker.Web.Infrastructure.Cookies;

namespace Spooker.Web.Controllers
{
    public class RegisterController : Controller
    {
        private readonly RoundKeeper _roundKeeper = RoundKeeper.Factory.GetInstance();

        private readonly IAppCookies _appCookies;

        public RegisterController(IAppCookies appCookies)
        {
            _appCookies = appCookies;
        }

        //
        // GET: /Register/

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(RegisterForm form)
        {
            if (!ModelState.IsValid) 
            {
                ModelState.AddModelError("", "Name is required!");
                return View(form);
            }

            var participant = new Participant(form.Name);
            participant.Participate(_roundKeeper.ActiveRound);
            _appCookies.ParticipantId = participant.Id;
            return RedirectToAction("Estimate", "Estimation");
        }
    }
}
