using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BachelorBackEnd;
using FrontEndBA.Models;
using StudyManagementSystem.DAOImplementations;
using StudyManagementSystem.DAOInterfaces;

namespace FrontEndBA.Utility.HomepageHelpers
{
    public class ResearcherHomepageHelper
    {
        public ResearcherHomepageModel CreateResearcherHompepageModel(int resID)
        {
            Researcher researcher = getResearcher(resID);
            IViewStudyHandler vsh = new ViewStudyHandler(new bachelordbContext());
            ResearcherHomepageModel researcherHomepageModel = new ResearcherHomepageModel();
            researcherHomepageModel.allStudies = vsh.GetAllStudiesDB();
            researcherHomepageModel.myResearcherStudies = vsh.GetMyResearcherStudiesDB(researcher.IdResearcher);
            researcherHomepageModel.researcher = researcher;
            return researcherHomepageModel;
        }

        public Researcher getResearcher(int id)
        {
            UserHandler userHandler = new UserHandler(new bachelordbContext());
            Researcher researcher = userHandler.getResearcher(id);
            return researcher;
        }
    }
}
