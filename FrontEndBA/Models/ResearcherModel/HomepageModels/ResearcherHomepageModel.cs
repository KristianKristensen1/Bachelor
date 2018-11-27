using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BachelorBackEnd;

namespace FrontEndBA.Models
{
    public class ResearcherHomepageModel
    {
        public Researcher researcher { get; set; }
        public List<Study> allStudies { get; set; }
        public List<Study> myResearcherStudies { get; set; }
    }
}
