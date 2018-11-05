using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BachelorBackEnd;

namespace StudyManagementSystem.DAOInterfaces
{
    public interface IViewStudyHandler
    {
        List<Study> GetRelevantStudiesDB(Participant participant);

        List<Study> GetMyParticipantStudiesDB(int participantID);

        List<Study> GetMyResearcherStudiesDB(int reseacherID);

        List<Study> GetAllStudiesDB();
    }
}
