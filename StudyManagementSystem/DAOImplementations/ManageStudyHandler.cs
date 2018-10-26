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
            if (_context.Study != null)
            {
                //Adds the study to the database and saves
                _context.Study.Add(study);
                _context.SaveChanges();

                //Retrieves the id from the study just saved and sets the study_id in inclusioncriteria.
                int id = (_context.Study.FirstOrDefault(stud => stud.Name == study.Name && stud.DateCreated == study.DateCreated)).IdStudy;
                inclusioncriteria.IdStudy = id;

                //Saves the inclusioncriteria 
                _context.Inclusioncriteria.Add(inclusioncriteria);
                _context.SaveChanges();
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
            List<Study> allStudies = new List<Study>();
            {
                if (_context.Study != null)
                {
                    allStudies = _context.Study.ToList();
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
        public List<Study> GetRelevantStudiesDB(Participant participant)
        {
            List<Study> relevantStudies = new List<Study>();

            if (_context.Study != null && _context.Inclusioncriteria != null)
            {
                int participantAge = DateTime.Now.Year - participant.Age.Year;

                //BEHOLD! The Sorterings-Algorithm!
                List<int> relevantStudyIDs = _context.Inclusioncriteria.Where(crit =>
                    //Sorts by age
                    Enumerable.Range(crit.MinAge, crit.MaxAge - crit.MinAge).Contains(participantAge) &&
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

        public Study getStudyDB(int id)
        {
            Study study = _context.Study.FirstOrDefault(stud => stud.IdStudy == id);

            return study;
        }

        public Inclusioncriteria getInclusioncriteriaDB(int id)
        {
            Inclusioncriteria incCrit = _context.Inclusioncriteria.FirstOrDefault(inc => inc.IdStudy == id);

            return incCrit;
        }
    }
}
