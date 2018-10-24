using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEndBA.Models.SharedModels
{
    public class StudyModel
    {
        //[Required(ErrorMessage = "Missing Description")]
        public string Description { get; set; }
    
        public Boolean Isdraft { get; set; }


        public int IdResearcher { get; set; }
        //[Required(ErrorMessage = "Missing Study Name")]
        public string Name { get; set; }
        //[Required(ErrorMessage = "Must input payment amount")]
        public int Pay { get; set; }
        //[Required(ErrorMessage = "Missing Abstract")]
        public string Abstract { get; set; }
        //[Required(ErrorMessage = "Missing Duration")]
        public int Duration { get; set; }

        public string Preparation { get; set; }

        public string EligibilityRequirements { get; set; }

        public DateTime DateCreated { get; set; }

        public string DirecetStudyLink { get; set; }
    }
}
