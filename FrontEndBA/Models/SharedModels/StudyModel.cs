﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEndBA.Models.SharedModels
{
    public class StudyModel
    {
        [Display(Name = "Description")]
        [Required(ErrorMessage = "Missing Description")]
        public string Description { get; set; }

        [Display(Name = "Create as a draft")]
        public bool Isdraft { get; set; }

        public int IdResearcher { get; set; }

        public int IdStudy { get; set; }

        [Display(Name = "Name*")]
        [Required(ErrorMessage = "Missing Study Name")]
        public string Name { get; set; }

        [Display(Name = "Pay")]
        [Required(ErrorMessage = "Missing payment amount")]
        public int? Pay { get; set; }
        [Display(Name = "Abstract*")]
        [Required(ErrorMessage = "Missing Abstract")]
        public string Abstract { get; set; }

        [Display(Name = "Duration*")]
        [Required(ErrorMessage = "Missing duration input")]
        public int? Duration { get; set; }

        [Display(Name = "Preparation*")]
        [Required(ErrorMessage = "Missing Preparation input")]
        public string Preparation { get; set; }

        [Display(Name = "Eligibility Requirements*")]
        [Required(ErrorMessage = "Missing Eligibility Requirements")]
        public string EligibilityRequirements { get; set; }

        public DateTime DateCreated { get; set; }

        public string DirecetStudyLink { get; set; }
    }
}
