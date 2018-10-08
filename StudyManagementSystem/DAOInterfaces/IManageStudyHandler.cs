using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachelorBackEnd
{
    interface IManageStudyHandler
    {
        void AddParticipantDB(string email, Study study);

        void DeleteStudyDB(Study study);

        void RemoveParticipantDB(Participant participant, Study study);

        void GetStudyDB(string studyID);

        void CreateStudyDB(Study study);

        void SaveAsDraftDB(Study study);

        void ShowStudyDB(string studyID);
    }
}
