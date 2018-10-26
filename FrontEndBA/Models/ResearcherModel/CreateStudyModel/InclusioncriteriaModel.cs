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
        [Display(Name = "Include Male*")]
        [Required(ErrorMessage = "Missing, must assign if study needs male")]
        public bool IsMale { get; set; }
        [Display(Name = "Include Female*")]
        [Required(ErrorMessage = "Missing, must assign if study needs female")]
        public bool IsFemale { get; set; }
        [Required(ErrorMessage = "Missing, must assign Age*")]
        [Display(Name = "Minimum Age")]
        public int? MinAge { get; set; }
        [Display(Name = "Maximum Age")]
        [Required(ErrorMessage = "Missing, must assign Age")]
        public int? MaxAge { get; set; }
        [Display(Name = "Must speak english*")]
        [Required(ErrorMessage = "Missing, must assign if the participant has to speak english")]
        public bool English { get; set; }
        public int IdStudy { get; set; }

        public Study IdStudyNavigation { get; set; }
    }


   
}
