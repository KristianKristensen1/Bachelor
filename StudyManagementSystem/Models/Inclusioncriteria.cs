using System;
using System.Collections.Generic;

namespace BachelorBackEnd
{
    public partial class Inclusioncriteria
    {
        public int IdInclusionCriteria { get; set; }
        public sbyte Male { get; set; }
        public sbyte Female { get; set; }
        public int MinAge { get; set; }
        public int MaxAge { get; set; }
        public sbyte English { get; set; }
        public int IdStudy { get; set; }

        public Study IdStudyNavigation { get; set; }
    }
}
