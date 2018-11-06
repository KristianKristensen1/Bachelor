using FrontEndBA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BachelorBackEnd;

namespace FrontEndBA.Utility
{
    public class ManageParticipantsHelper
    {

        public ManageParticipantModel CreateManageParticipantModel(int studyID, string studyName)
        {
            ManageStudyHandler msh = new ManageStudyHandler(new bachelordbContext());
            ManageParticipantModel manageParticipantModel = new ManageParticipantModel();
            manageParticipantModel.participants = msh.getParticipantsDB(studyID);
            manageParticipantModel.nameOfStudy = studyName;
            manageParticipantModel.studyID = studyID;
            return manageParticipantModel;
        }
    }
}
