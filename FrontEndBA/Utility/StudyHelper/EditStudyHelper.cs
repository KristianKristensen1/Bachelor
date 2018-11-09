using FrontEndBA.Models.CreateStudy;
using FrontEndBA.Models.ResearcherModel.CreateStudyModel;
using FrontEndBA.Models.SharedModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BachelorBackEnd;

namespace FrontEndBA.Utility
{
    public class EditStudyHelper
    {
        public CreateStudyModel CreateEditStudyModel(int StudyID)
        {
            //Creates a CreateStudyModel containing information about the Study that should be edited. 
            CreateStudyModel cs = new CreateStudyModel();
            ManageStudyHandler msh = new ManageStudyHandler(new bachelordbContext());
            Study study = msh.getStudyDB(StudyID);
            Inclusioncriteria inc = msh.getInclusioncriteriaDB(StudyID);
            cs.currentStudy = new StudyModel() { Name = study.Name, Abstract = study.Abstract, Description = study.Description, Duration = study.Duration, DateCreated = study.DateCreated, Location = study.Location,
                Isdraft = study.Isdraft, Pay = (int?)study.Pay, EligibilityRequirements = study.EligibilityRequirements, Preparation = study.Preparation, DirecetStudyLink = study.DirectStudyLink, IdStudy = study.IdStudy };
            cs.inclusioncriteria = new InclusioncriteriaModel() { IsMale = inc.Male, IsFemale = inc.Female, English = inc.English, MaxAge = inc.MaxAge, MinAge = inc.MinAge,
                IdInclusionCriteria = inc.IdInclusionCriteria, IdStudy = inc.IdStudy };

            return cs;

        }
    }
}
