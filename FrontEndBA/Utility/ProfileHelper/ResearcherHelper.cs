using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FrontEndBA.Models.ProfileModel;
using FrontEndBA.Utility.HomepageHelpers;

namespace FrontEndBA.Utility.ProfileHelper
{
    public static class ResearcherHelper
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
    }
}
