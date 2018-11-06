using BachelorBackEnd;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEndBA.Models
{
    public class ParticipantHomepageModel
    {
        public List<Study> myParticipantStudies { get; set; }
        public List<Study> relevantStudies { get; set; }
    }
}
