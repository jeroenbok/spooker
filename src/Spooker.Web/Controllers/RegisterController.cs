using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Spooker.Web.Domain;

namespace Spooker.Web.Controllers
{
    public class RegisterController : Controller
    {
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
            StoreUserIdInCookie(participant.UserId);
            return RedirectToAction("Estimate", "Estimation");
        }

        private void StoreUserIdInCookie(Guid userId)
        {
            HttpCookie userIdCookie = new HttpCookie("SpookerUserCookie");
            userIdCookie.Expires = DateTime.Now.AddDays(1);
            userIdCookie.Values["UserId"] = userId.ToString();
            this.ControllerContext.HttpContext.Response.Cookies.Add(userIdCookie);
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
