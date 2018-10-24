using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BachelorBackEnd
{
    public class ManageStudyHandler : IManageStudyHandler
    {
        private bachelordbContext _context;
        public ManageStudyHandler()
        {
        }
        public ManageStudyHandler(bachelordbContext context)
        {
            _context = context;
        }

        public void AddParticipantDB(string email, Study study)
        {
            throw new NotImplementedException();
        }

        public void CreateStudyDB(Study study, Inclusioncriteria inclusioncriteria)
        {
            using (bachelordbContext DBmodel = new bachelordbContext())
            {
                //Adds the study to the database and saves
                DBmodel.Study.Add(study);
                DBmodel.SaveChanges();

                //Retrieves the id from the study just saved and sets the study_id in inclusioncriteria.
                int id = (DBmodel.Study.FirstOrDefault(stud => stud.Name == study.Name && stud.DateCreated == study.DateCreated)).IdStudy;
                inclusioncriteria.IdStudy = id;

                //Saves the inclusioncriteria 
                DBmodel.Inclusioncriteria.Add(inclusioncriteria);
                DBmodel.SaveChanges();
            }
        }

        public void DeleteStudyDB(Study study)
        {
            using (bachelordbContext DBmodel = new bachelordbContext())
            {
                DBmodel.Study.First(stud => stud.IdStudy == study.IdStudy);
            }
        }

        public void RemoveParticipantDB(Participant participant, Study study)
        {
            throw new NotImplementedException();
        }

        public void SaveAsDraftDB(Study study)
        {
            throw new NotImplementedException();
        }

        //OBS! Change diagrams to match changes.
        public List<Study> GetAllStudiesDB()
        {
            List<Study> allStudies;

            using (bachelordbContext DBmodel = new bachelordbContext())
            {
                if (_context != null)
                {
                    allStudies = _context.Study.ToList();
                }
                else
                {
                    allStudies = DBmodel.Study.ToList();
                }

                return allStudies;
            }
        }

        //OBS! Change diagrams to match changes.
        public List<Study> GetMyParticipantStudiesDB(int participantID)
        {
            using (bachelordbContext DBmodel = new bachelordbContext())
            {
                List<Study> myStudies = new List<Study>();
                List<int> myStudyIDs = DBmodel.Studyparticipant.Where
                    (studpart => studpart.IdParticipant == participantID).ToList()
                    .Select(partID => partID.IdStudy).ToList();

                foreach (var id in myStudyIDs)
                {
                    myStudies.Add(DBmodel.Study.FirstOrDefault(stud => stud.IdStudy == id));
                }
                return myStudies;
            }
        }

        //OBS! Change diagrams to match changes.
        public List<Study> GetMyResearcherStudiesDB(int reseacherID)
        {
            using (bachelordbContext DBmodel = new bachelordbContext())
            {
                List<Study> myStudies;
                if (_context != null)
                {
                    myStudies = _context.Study.Where(stud => stud.IdResearcher == reseacherID).ToList();
                }
                else
                {
                    myStudies = DBmodel.Study.Where(stud => stud.IdResearcher == reseacherID).ToList();
                }                
                return myStudies;
            }
        }

        //OBS! Change diagrams to match changes.
        public List<Study> GetRelevantStudiesDB(Participant participant)
        {
            using (bachelordbContext DBmodel = new bachelordbContext())
            {
                List<Study> relevantStudies = new List<Study>();
                int participantAge = participant.Age.Year - DateTime.Now.Year;

                //BEHOLD! The Sorterings-Algorithm!
                List<int> relevantStudyIDs = DBmodel.Inclusioncriteria.Where(crit =>
                    //Sorts by age
                    (crit.MinAge >= participantAge &&
                    participantAge <= crit.MaxAge) &&
                    //Sorts by gender
                    (crit.Male == Convert.ToSByte(participant.Gender) ||
                    crit.Female != Convert.ToSByte(participant.Gender)) &&
                    //Sorts by english language
                    ((participant.English == true) ?
                    crit.English == Convert.ToSByte(participant.English) ||
                    crit.English != Convert.ToSByte(participant.English) :
                    crit.English != Convert.ToSByte(participant.English)))
                    //Saves the relevant IDs to a list
                    .ToList().Select(studID => studID.IdStudy).ToList();

                foreach (var id in relevantStudyIDs)
                {
                    relevantStudies.Add(DBmodel.Study.FirstOrDefault(stud => stud.IdStudy == id));
                }
                return relevantStudies;
            }
        }
    }
}
