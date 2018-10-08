using System;
using System.Collections.Generic;

namespace BachelorBackEnd
{
    public partial class Questionnaire
    {
        public int IdQuestionnaire { get; set; }
        public sbyte Gender { get; set; }
        public int Age { get; set; }
        public sbyte Medicin { get; set; }
        public int IdParticipant { get; set; }

        public Participant IdParticipantNavigation { get; set; }
    }
}
