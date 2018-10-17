﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachelorBackEnd
{
    public class ManageStudyHandler : IManageStudyHandler
    {
        public void AddParticipantDB(string email, Study study)
        {
            throw new NotImplementedException();
        }

        public void CreateStudyDB(Study study, Inclusioncriteria inclusioncriteria)
        {
            using (bachelordbContext DBmodel = new bachelordbContext())
            {
                DBmodel.Study.Add(study);
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
            using (bachelordbContext DBmodel = new bachelordbContext())
            {
                List<Study> allStudies = DBmodel.Study.ToList();
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
                List<Study> myStudies = new List<Study>();
                myStudies = DBmodel.Study.Where(stud => stud.IdResearcher == reseacherID).ToList();
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
