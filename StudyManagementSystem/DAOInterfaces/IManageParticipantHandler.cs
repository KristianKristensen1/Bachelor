using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachelorBackEnd
{
    public interface IManageParticipantHandler
    {
        DbStatus AddParticipantToStudyDB(int partID, int studyID);

        DbStatus RemoveParticipantFromStudyDB(int partID, int studyID);

        List<Participant> GetParticipantsInStudyDB(int studyID);

        DbStatus GetParticipantEmailDB(int partID);
    }
}
