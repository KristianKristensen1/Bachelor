using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachelorBackEnd
{
    public interface IManageProfileHandler
    {
        DbStatus ChangePasswordResearcherDB(Researcher researcher, string oldPassword);

        DbStatus ChangePasswordParticipantDB(Participant participant, string oldPassword);

        bool ChangeProfileResearcherDB(Researcher researcher);

        bool ChangeProfileParticipantDB(Participant participant);

        void DeleteAccountParticipantDB(int partID);

        void DeleteAcoountResearcherDB(Researcher researcher);
    }
}
