using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BachelorBackEnd;
using FrontEndBA.Models.ResearcherModel.CreateStudyModel;
using FrontEndBA.Models.SharedModels;

namespace FrontEndBA.Models.CreateStudy
{
    public class CreateStudy
    {
        public StudyModel currentStudy { get; set; }
        public InclusioncriteriaModel inclusioncriteria { get; set; }
    }
}
