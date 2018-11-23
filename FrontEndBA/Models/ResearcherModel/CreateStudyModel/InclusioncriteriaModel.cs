using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BachelorBackEnd;
using FrontEndBA.Models.ParticipantModel.AccountViewModels;

namespace FrontEndBA.Models.ResearcherModel.CreateStudyModel
{
    public class InclusioncriteriaModel
    {
        public int IdInclusionCriteria { get; set; }

        [Display(Name = "Include Men*")]
        [Required(ErrorMessage = "Missing, must assign if Study needs male")]
        public bool IsMale { get; set; }

        [Display(Name = "Include Women*")]
        [Required(ErrorMessage = "Missing, must assign if Study needs female")]
        public bool IsFemale { get; set; }

        [Display(Name = "Minimum Age")]
        [Required(ErrorMessage = "Missing, must assign Age*")]
        public int? MinAge { get; set; }

        [Required(ErrorMessage = "Missing, must assign Age")]
        [Display(Name = "Maximum Age")]
        public int? MaxAge { get; set; }

        [Display(Name = "Must speak english*")]
        [Required(ErrorMessage = "Missing, must assign if the participant has to speak english")]
        public bool English { get; set; }

        public int IdStudy { get; set; }

        public Study IdStudyNavigation { get; set; }
    }


   
}
