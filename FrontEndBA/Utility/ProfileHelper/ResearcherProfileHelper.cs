using BachelorBackEnd;
using FrontEndBA.Models.ResearcherModel.AccountViewModels;
using FrontEndBA.Models.SharedModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEndBA.Utility.ProfileHelper
{
    public class ResearcherProfileHelper
    {
        public ResearcherProfileViewModel CreateResearcherProfileModel(int resID)
        {
            //Creates a CreateStudyModel containing information about the study that should be edited. 
            ResearcherProfileViewModel rpv = new ResearcherProfileViewModel();
            ManageStudyHandler msh = new ManageStudyHandler(new bachelordbContext());
            Researcher researcher = msh.getResearcherDB(resID);
            if (researcher != null)
            {
                rpv.CurrentResearcher = new ResearcherModel()
                {
                    ResID = researcher.IdResearcher,
                    FirstName = researcher.FirstName,
                    LastName = researcher.LastName,
                    Email = researcher.Email,
                    OldPassword = researcher.Password,
                    NewPassword = researcher.Password,
                    ConfirmPassword = researcher.Password,
                    AdminRights = researcher.Isadmin,
                    Verified = researcher.Isverified,
                };
            }           
            return rpv;
        }
    }
}
