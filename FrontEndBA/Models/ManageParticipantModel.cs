using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BachelorBackEnd;

namespace FrontEndBA.Models
{
    public class ManageParticipantModel
    {
        public List<Participant> participants { get; set; }

        public string nameOfStudy { get; set; }

        public int studyID { get; set; }

        [Display(Name = "participantID")]
        public int participantID { get; set; }

        public string participantEmail { get; set; }

    }
}
