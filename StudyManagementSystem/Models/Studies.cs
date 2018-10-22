using BachelorBackEnd;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyManagementSystem.Models
{
    public partial class Studies
    {
        List<Study> allStudies = new List<Study>();
        List<Study> myResearcherStudies = new List<Study>();
        List<Study> myParticipantStudies = new List<Study>();
        List<Study> relevantStudies = new List<Study>();
    }
}
