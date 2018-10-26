using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BachelorBackEnd;
using FrontEndBA.Models.CreateStudy;
using FrontEndBA.Models.ResearcherModel.CreateStudyModel;
using FrontEndBA.Models.SharedModels;

namespace FrontEndBA.Utility
{
    public class CreateStudyHelper
    {
        public Study ConvertStudy(Study study,CreateStudyModel csmodel,int id)
        {
            if(csmodel.currentStudy==null)
                csmodel.currentStudy= new StudyModel();
            study.Isdraft = csmodel.currentStudy.Isdraft;
            study.DirectStudyLink = "Test";
            study.IdResearcher = id;
            return study;
        }

        public Inclusioncriteria ConvertInclusioncriteria(Inclusioncriteria inclusioncriteria, CreateStudyModel csmodel)
        {
            inclusioncriteria.English = csmodel.inclusioncriteria.English;
            inclusioncriteria.Female = csmodel.inclusioncriteria.IsFemale;
            inclusioncriteria.Male = csmodel.inclusioncriteria.IsMale;
            return inclusioncriteria;
        }

        public CreateStudyModel DefaultCreateStudyModel()
        {
            CreateStudyModel cs = new CreateStudyModel();
            cs.inclusioncriteria = new InclusioncriteriaModel();
            cs.currentStudy = new StudyModel();
            
            cs.inclusioncriteria.English = false;
            cs.inclusioncriteria.IsFemale = false;
            cs.inclusioncriteria.IsMale = false;

            return cs;
        }

        public CreateStudyModel ErrorHandle(Inclusioncriteria inclusioncriteria, CreateStudyModel csmodel, Study study)
        {

            //Handle inclusioncriteria
            csmodel.inclusioncriteria.MaxAge = inclusioncriteria.MaxAge;
            csmodel.inclusioncriteria.MinAge = inclusioncriteria.MinAge;
            //Handle study
            csmodel.currentStudy.Abstract = study.Abstract;
            csmodel.currentStudy.Description = study.Description;
            csmodel.currentStudy.DirecetStudyLink = study.DirectStudyLink;
            csmodel.currentStudy.Duration = study.Duration;
            csmodel.currentStudy.EligibilityRequirements = study.EligibilityRequirements;
            csmodel.currentStudy.Name = study.Name;
            csmodel.currentStudy.Pay = (int)study.Pay;
            csmodel.currentStudy.Preparation = study.Preparation;


            return csmodel;
        }

    }
}
