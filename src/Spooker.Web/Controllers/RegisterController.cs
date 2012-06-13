using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Spooker.Web.Domain;

namespace Spooker.Web.Controllers
{
    public class RegisterController : Controller
    {
        //
        // GET: /Register/

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterForm form)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Name is required!");
                return View(form);
            }

            new Participant(form.Name).Participate(RoundKeeper.CurrentRound);

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
}
