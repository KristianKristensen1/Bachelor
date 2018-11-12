using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEndBA.Models.ResearcherModel.AccountViewModels
{
    public class ResearcherProfileViewModel
    {
        [Required(ErrorMessage = "Missing Email")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Missing Firstname")]
        [Display(Name = "Firstname")]
        public string Firstname { get; set; }

        [Required(ErrorMessage = "Missing Lastname")]
        [Display(Name = "Lastname")]
        public string Lastname { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Admin Rights")]
        public string AdminRights { get; set; }

        [Display(Name = "Verified")]
        public string Verified { get; set; }

        [Display(Name = "Researcher ID")]
        public string ResID { get; set; }

        [Display(Name = "Name")]
        public string ResName { get; set; }
    }
}
