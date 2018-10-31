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
        public Study ConvertStudy(CreateStudyModel csmodel, int id)
        {
            var study = new Study
            {
                Name = csmodel.currentStudy.Name,
                Pay = (int)csmodel.currentStudy.Pay,
                Preparation = csmodel.currentStudy.Preparation,
                Isdraft = csmodel.currentStudy.Isdraft,
                DirectStudyLink = "Test",
                IdResearcher = id,
                Abstract = csmodel.currentStudy.Abstract,
                DateCreated = DateTime.Now,
                Description = csmodel.currentStudy.Description,
                Duration = csmodel.currentStudy.Duration,
                EligibilityRequirements = csmodel.currentStudy.EligibilityRequirements,
                Location = csmodel.currentStudy.Location
                

            };
            return study;
        }

        public Inclusioncriteria ConvertInclusioncriteria(CreateStudyModel csmodel)
        {
            var inclusioncriteria = new Inclusioncriteria
            {
                English = csmodel.inclusioncriteria.English,
                Female = csmodel.inclusioncriteria.IsFemale,
                Male = csmodel.inclusioncriteria.IsMale,
                MaxAge =  (int)csmodel.inclusioncriteria.MaxAge,
                MinAge = (int)csmodel.inclusioncriteria.MinAge


        };

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
            csmodel.currentStudy.Pay = (int) study.Pay;
            csmodel.currentStudy.Preparation = study.Preparation;


            return csmodel;
        }
    }
}