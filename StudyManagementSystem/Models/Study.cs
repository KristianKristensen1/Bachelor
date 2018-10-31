using System;
using System.Collections.Generic;

namespace BachelorBackEnd
{
    public partial class Study
    {
        public Study()
        {
            Inclusioncriteria = new HashSet<Inclusioncriteria>();
            Studyparticipant = new HashSet<Studyparticipant>();
        }

        public int IdStudy { get; set; }
        public string Description { get; set; }
        public bool Isdraft { get; set; }
        public int IdResearcher { get; set; }
        public string Name { get; set; }
        public double Pay { get; set; }
        public string Abstract { get; set; }
        public string Duration { get; set; }
        public DateTime DateCreated { get; set; }
        public string DirectStudyLink { get; set; }
        public string Preparation { get; set; }
        public string EligibilityRequirements { get; set; }

        public string Location { get; set; }

        public Researcher IdResearcherNavigation { get; set; }
        public ICollection<Inclusioncriteria> Inclusioncriteria { get; set; }
        public ICollection<Studyparticipant> Studyparticipant { get; set; }
    }
}
