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
            IUserHandler ush = new UserHandler(new bachelordbContext());

            VerifyResearcherModel verifyResearcherModel = new VerifyResearcherModel();
            verifyResearcherModel.UnverifiedResearchers = ush.GetUnverifiedResearchersDB();
            verifyResearcherModel.AllResearchers = ush.GetAllVerifiedResearchersDB(); 
            return verifyResearcherModel;

        }
    }
}
