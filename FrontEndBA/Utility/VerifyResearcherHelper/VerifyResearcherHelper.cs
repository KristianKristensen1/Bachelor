using BachelorBackEnd;
using FrontEndBA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEndBA.Utility.VerifyResearcherHelper
{
    public class VerifyResearcherHelper
    {
        public VerifyResearcherModel CreateVerifyResearcherModel()
        {
            VerifyResearcherModel verifyResearcherModel = new VerifyResearcherModel();
            verifyResearcherModel.UnverifiedResearchers = getUnverifiedResearchers();
            verifyResearcherModel.AllResearchers = getAllResearchers();
            return verifyResearcherModel;

        }

        public List<Researcher> getUnverifiedResearchers()
        {
            UserHandler userHandler = new UserHandler(new bachelordbContext());
            return userHandler.getUnverifiedResearchersDB();
        }

        public List<Researcher> getAllResearchers()
        {
            UserHandler userHandler = new UserHandler(new bachelordbContext());
            return userHandler.getAllResearchersDB();
        }
    }
}
