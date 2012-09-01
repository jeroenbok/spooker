using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Spooker.Web.Domain;
using Spooker.Web.Infrastructure;
using Spooker.Web.Infrastructure.Cookies;

namespace Spooker.Web.Controllers
{
    public class RegisterController : Controller
    {
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
            participant.Participate(RoundKeeper.CurrentRound);
            _appCookies.UserId = participant.UserId;
            return RedirectToAction("Estimate", "Estimation");
        }
    }

    public class RoundKeeper
    {
        public static EstimationRound CurrentRound = new EstimationRound();
    }

    public class RegisterForm
    {
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }
    }

    public class EstimationForm
    {
        public EstimationForm()
        {
            // Required by MVC
        }

        [Required]
        [Display(Name = "Estimate")]
        public string Estimate { get; set; }
    }
}
