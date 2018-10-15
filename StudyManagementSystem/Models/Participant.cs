using System;
using System.Collections.Generic;


namespace BachelorBackEnd
{
    public partial class Participant
    {
        public Participant()
        {
            Questionnaire = new HashSet<Questionnaire>();
            Studyparticipant = new HashSet<Studyparticipant>();
        }

        public int IdParticipant { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public ICollection<Questionnaire> Questionnaire { get; set; }
        public ICollection<Studyparticipant> Studyparticipant { get; set; }
    }
}
