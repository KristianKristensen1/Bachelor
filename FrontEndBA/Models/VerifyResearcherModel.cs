using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BachelorBackEnd;

namespace FrontEndBA.Models
{
    public class VerifyResearcherModel
    {
        public List<Researcher> UnverifiedResearchers { get; set; }

        public List<Researcher> AllResearchers { get; set; }

        [Display(Name = "researcherID")]
        public int researcherID { get; set; }

        public string researcherEmail { get; set; }
    }
}
