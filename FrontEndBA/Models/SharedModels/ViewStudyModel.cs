using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BachelorBackEnd;

namespace FrontEndBA.Models
{
    public class ViewStudyModel
    {
        public Study study { get; set; }

        public Inclusioncriteria inclusioncriteria { get; set; }

        public Researcher researcher { get; set; }
    }
}
