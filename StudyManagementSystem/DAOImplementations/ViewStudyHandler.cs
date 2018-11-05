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

            //We select the StudyIDs from the StudyParticipant table where the ParticipantIDs match our participant.
            //Furthermore, after finding the studies, we convert the list of studies to select only the actual IDs -
            //- and not the studies as a whole before converting it back to the list we use later.
            //The same procedure is used in GetRelevantStudiesDB().
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
                    TimeSpan ts = DateTime.Now.Subtract(participant.Age);
                    var participantAge = (int)Math.Floor(ts.TotalDays / 365.25);

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
                        //Sorts through the list of IDs to find matching studies that are NOT drafts (as drafts are only visible for their respective researcher)
                        var relevantStudiesTemp = (from stud in _context.Study
                                                   where (stud.Isdraft == false && stud.IdStudy == id)
                                                   select stud).ToList();
                        //In case of an ID match on a non-draft study, the Temp list contains an object that we add to the final Study List.
                        if (relevantStudiesTemp.Count > 0)
                        {
                            relevantStudies.Add(relevantStudiesTemp[0]);
                        }
                    }
                }
                return relevantStudies;
            }
        }
}
