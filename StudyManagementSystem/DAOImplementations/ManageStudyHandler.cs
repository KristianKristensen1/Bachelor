using Microsoft.EntityFrameworkCore;
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
            //Adds the study to the database and saves
            _context.Study.Add(study);
            var t = Task.Run(() => _context.SaveChanges());
            t.Wait();


            //Retrieves the id from the study just saved and sets the study_id in inclusioncriteria.            
            var dbStudy = (_context.Study.FirstOrDefault(stud =>
                stud.Name == study.Name && stud.DateCreated == study.DateCreated)) ?? _context.Study.Local.FirstOrDefault(stud => stud.Name == study.Name && stud.DateCreated == study.DateCreated);
            inclusioncriteria.IdStudy = dbStudy.IdStudy;

            //Saves the inclusioncriteria 
            _context.Inclusioncriteria.Add(inclusioncriteria);
            _context.SaveChanges();
        }

        public void EditStudy(Study study, Inclusioncriteria inclusioncriteria)
        {
            Study oldStudy = _context.Study.FirstOrDefault(stud => stud.IdStudy == study.IdStudy);
            if (oldStudy != null)
            {
                oldStudy.Name = study.Name; oldStudy.Description = study.Description; oldStudy.Abstract = study.Abstract; oldStudy.Duration = study.Duration; oldStudy.EligibilityRequirements = study.EligibilityRequirements;
                oldStudy.Inclusioncriteria = study.Inclusioncriteria; oldStudy.Isdraft = study.Isdraft; oldStudy.Pay = study.Pay; oldStudy.Preparation = study.Preparation; oldStudy.Location = study.Location;
                _context.Study.Update(oldStudy);
                _context.SaveChanges();
            }

            Inclusioncriteria oldInc = _context.Inclusioncriteria.FirstOrDefault(inc => inc.IdStudy == study.IdStudy);
            if (oldInc != null)
            {
                oldInc.Male = inclusioncriteria.Male; oldInc.Female = inclusioncriteria.Female; oldInc.English = inclusioncriteria.English; oldInc.MinAge = inclusioncriteria.MinAge; oldInc.MaxAge = inclusioncriteria.MaxAge;
                _context.Inclusioncriteria.Update(oldInc);
                _context.SaveChanges();
            }
        }

        public void DeleteStudyDB(int studyID)
        {
            Study study = _context.Study.FirstOrDefault(stud => stud.IdStudy == studyID);
            _context.Study.Remove(study);
            _context.SaveChanges();
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

        public Researcher getResearcherDB(int id)
        {
            Researcher researcher = _context.Researcher.FirstOrDefault(res => res.IdResearcher == id);
            return researcher;
        }
    }
}
