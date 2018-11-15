using System.ComponentModel.DataAnnotations;

namespace FrontEndBA.Models.SharedModels
{
    public class ResearcherModel
    {
        [Required(ErrorMessage = "Missing Email")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Missing FirstName")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Missing LastName")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Old Password")]
        [Compare("OldPassword", ErrorMessage = "The old password is not correct.")]
        public string OldPassword { get; set; }

        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New Password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Admin Rights")]
        public bool AdminRights { get; set; }

        [Display(Name = "Verified")]
        public bool Verified { get; set; }

        [Display(Name = "Researcher ID")]
        public int ResID { get; set; }
    }
}
