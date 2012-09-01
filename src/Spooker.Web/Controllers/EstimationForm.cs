using System.ComponentModel.DataAnnotations;

namespace Spooker.Web.Controllers
{
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