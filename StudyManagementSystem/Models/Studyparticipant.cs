using System;
using System.Collections.Generic;


namespace BachelorBackEnd
{
    public partial class Studyparticipant
    {
        public int IdStudyParticipant { get; set; }
        public int IdParticipant { get; set; }
        public int IdStudy { get; set; }

        public Participant IdParticipantNavigation { get; set; }
        public Study IdStudyNavigation { get; set; }
    }
}
