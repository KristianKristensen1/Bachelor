using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BachelorBackEnd
{
    public partial class Inclusioncriteria
    {
        public int IdInclusionCriteria { get; set; }
        public bool Male { get; set; }
        public bool Female { get; set; }
        public int MinAge { get; set; }
        public int MaxAge { get; set; }
        public bool English { get; set; }
        public int IdStudy { get; set; }

        public Study IdStudyNavigation { get; set; }
    }
}
