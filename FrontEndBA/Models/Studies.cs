using BachelorBackEnd;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontEndBA.Models
{
    public class Studies
    {
        public List<Study> allStudies { get; set; }
        public List<Study> myResearcherStudies { get; set; }
        public List<Study> myParticipantStudies{ get; set; }
        public List<Study> relevantStudies{ get; set; } 
    }
}