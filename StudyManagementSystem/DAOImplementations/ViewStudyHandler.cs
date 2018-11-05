using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BachelorBackEnd;
using StudyManagementSystem.DAOInterfaces;

namespace StudyManagementSystem.DAOImplementations
{
    public class ViewStudyHandler: IViewStudyHandler
    {
        private bachelordbContext _context;

        public ViewStudyHandler(bachelordbContext context)
        {
            _context = context;
        }

        public List<Study> GetAllStudiesDB()
        {
            List<Study> allStudies = new List<Study>();
            {
                if (_context.Study != null)
                {
                    allStudies = _context.Study.Where(stud => stud.Isdraft != true).ToList();
                }

                return allStudies;
            }
        }

        //OBS! Change diagrams to match changes.
        public List<Study> GetMyResearcherStudiesDB(int reseacherID)
        {
            List<Study> myStudies = new List<Study>();
            if (_context.Study != null)
            {
                myStudies = _context.Study.Where(stud => stud.IdResearcher == reseacherID).ToList();
            }

            return myStudies;
        }

        //OBS! Change diagrams to match changes.
        public List<Study> GetMyParticipantStudiesDB(int participantID)
        {
            List<Study> myStudies = new List<Study>();
            List<int> myStudyIDs = _context.Studyparticipant.Where
                (studpart => studpart.IdParticipant == participantID).ToList()
                .Select(partID => partID.IdStudy).ToList();

            foreach (var id in myStudyIDs)
            {
                myStudies.Add(_context.Study.FirstOrDefault(stud => stud.IdStudy == id));
            }
            return myStudies;

        }

        //OBS! Change diagrams to match changes.
        public List<Study> GetRelevantStudiesDB(Participant participant)
        {
            List<Study> relevantStudies = new List<Study>();

            if (_context.Study != null && _context.Inclusioncriteria != null)
            {
                int participantAge = DateTime.Now.Year - participant.Age.Year;

                //BEHOLD! The Sorterings-Algorithm!
                List<int> relevantStudyIDs = _context.Inclusioncriteria.Where(crit =>
                    //Sorts by age
                    crit.MinAge < participantAge && crit.MaxAge > participantAge &&
                    //Sorts by gender
                    (crit.Male == participant.Gender ||
                    crit.Female != participant.Gender) &&
                    //Sorts by english language
                    ((participant.English == true) ?
                    crit.English == participant.English ||
                    crit.English != participant.English :
                    crit.English == participant.English))
                    //Saves the relevant IDs to a list
                    .ToList().Select(studID => studID.IdStudy).ToList();

                foreach (var id in relevantStudyIDs)
                {
                    relevantStudies.Add(_context.Study.FirstOrDefault(stud => stud.IdStudy == id));
                }
            }

            return relevantStudies;
        }
    }
}
