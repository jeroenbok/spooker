using System.ComponentModel.DataAnnotations;

namespace Spooker.Web.Controllers
{
    public class RegisterForm
    {
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }
    }
}