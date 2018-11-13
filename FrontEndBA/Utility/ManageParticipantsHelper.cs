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
            ManageParticipantHandler mph = new ManageParticipantHandler(new bachelordbContext());
            ManageParticipantModel manageParticipantModel = new ManageParticipantModel();
            manageParticipantModel.participants = mph.GetParticipantsInStudyDB(studyID);
            manageParticipantModel.nameOfStudy = studyName;
            manageParticipantModel.studyID = studyID;
            return manageParticipantModel;
        }
    }
}
