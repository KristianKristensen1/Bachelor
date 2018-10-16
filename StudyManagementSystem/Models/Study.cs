﻿using System;
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
        public string Tag { get; set; }
        public int IdResearcher { get; set; }

        public Researcher IdResearcherNavigation { get; set; }
        public ICollection<Inclusioncriteria> Inclusioncriteria { get; set; }
        public ICollection<Studyparticipant> Studyparticipant { get; set; }
    }
}
