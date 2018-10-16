using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Routing.Constraints;

namespace FrontEndBA.Models.ParticipantModel.AccountViewModels
{
    public class ParticipantRegisterViewModel
    {
        [Required(ErrorMessage = "Missing Email")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Must pick a gender")]
        [Display(Name = "Pick a gender")]
        [EnumDataType(typeof(Gender))]
        public Gender GenderType { get; set; }

        [Display(Name = "Do you speak English?")]
        [Required(ErrorMessage = "Must enter if you speak English or not")]
        public Boolean Language { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        [Required(ErrorMessage = "Must enter a Age")]
        public DateTime Age { get; set; }




    }
    public enum Gender
    {
        Male = 1,
        Female = 0
    }


}