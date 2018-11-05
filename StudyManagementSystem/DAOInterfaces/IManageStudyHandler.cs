using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BachelorBackEnd
{
    public interface IManageStudyHandler
    {
        void AddParticipantDB(string email, Study study);

        void DeleteStudyDB(int studyID);

        void RemoveParticipantDB(Participant participant, Study study);        

        void CreateStudyDB(Study study, Inclusioncriteria inclusioncriteria);


        //List<Study> GetRelevantStudiesDB(Participant participant);

        //List<Study> GetMyParticipantStudiesDB(int participantID);

        //List<Study> GetMyResearcherStudiesDB(int reseacherID);

        //List<Study> GetAllStudiesDB();
    }
}
