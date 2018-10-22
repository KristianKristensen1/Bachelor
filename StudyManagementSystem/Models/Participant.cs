using System;
using System.Collections.Generic;
using BachelorBackEnd;


namespace StudyManagementSystem.Models
{
    public partial class Participant
    {
        public Participant()
        {
            Studyparticipant = new HashSet<Studyparticipant>();
        }

        public int IdParticipant { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public DateTime Age { get; set; }

        public bool Gender { get; set; }

        public bool English { get; set; }

        public ICollection<Studyparticipant> Studyparticipant { get; set; }
    }
}
