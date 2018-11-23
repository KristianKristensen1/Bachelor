using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BachelorBackEnd;
using FrontEndBA.Models.ProfileModel;
using FrontEndBA.Utility.HomepageHelpers;

namespace FrontEndBA.Utility.ProfileHelper
{
    public static class ProfileResearcherHelper
    {
        public static ResearcherProfileModel getdefaultResearcher(int id)
        {
            ResearcherHomepageHelper helper = new ResearcherHomepageHelper();
            ResearcherProfileModel rpm = new ResearcherProfileModel();
            var curResearcher = helper.getResearcher(id);
            rpm.Id = id;
            rpm.Verify = curResearcher.Isverified;
            rpm.Admin = curResearcher.Isadmin;
            rpm.Firstname = curResearcher.FirstName;
            rpm.Lastname = curResearcher.LastName;
            rpm.Email = curResearcher.Email;
            rpm.OldPassword = curResearcher.Password;
            return rpm;
        }

        public static ResearcherProfileModel convertToModel(Researcher researcher, bool status,bool validInput)
        {
            ResearcherProfileModel rpm = new ResearcherProfileModel();
            rpm.Id = researcher.IdResearcher;
            rpm.Verify = researcher.Isverified;
            rpm.Admin = researcher.Isadmin;
            rpm.Firstname = researcher.FirstName;
            rpm.Lastname = researcher.LastName;
            rpm.Email = researcher.Email;
            rpm.OldPassword = researcher.Password;
            rpm.SuccesChangePassword = status;
            rpm.ValidInput = validInput;
            return rpm;
        }
    }
}
