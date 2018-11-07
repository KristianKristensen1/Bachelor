using System.ComponentModel.DataAnnotations;

namespace FrontEndBA.Models.SharedModels
{
    public class RetrieveModel
    {
        [Required(ErrorMessage = "Missing Email")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
