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
            _context.SaveChanges();


            //Retrieves the id from the study just saved and sets the study_id in inclusioncriteria.            
            var dbStudy = (_context.Study.FirstOrDefault(stud =>
                              stud.Name == study.Name && stud.DateCreated == study.DateCreated)) ??
                          _context.Study.Local.FirstOrDefault(stud =>
                              stud.Name == study.Name && stud.DateCreated == study.DateCreated);
            inclusioncriteria.IdStudy = dbStudy.IdStudy;

            //Sets the direct study link with the generated ID
            if (dbStudy != null)
            {
                dbStudy.DirectStudyLink = "http://localhost:61728/ViewStudy/ViewStudy?studyID=" + dbStudy.IdStudy;
                _context.Study.Update(dbStudy);
            }

            //Saves the inclusioncriteria and the study link
            _context.Inclusioncriteria.Add(inclusioncriteria);
            _context.SaveChanges();    
        }

        public void EditStudy(Study study, Inclusioncriteria inclusioncriteria)
        {
            Study oldStudy = _context.Study.FirstOrDefault(stud => stud.IdStudy == study.IdStudy);
            if (oldStudy != null)
            {
                oldStudy.Name = study.Name;
                oldStudy.Description = study.Description;
                oldStudy.Abstract = study.Abstract;
                oldStudy.Duration = study.Duration;
                oldStudy.EligibilityRequirements = study.EligibilityRequirements;
                oldStudy.Inclusioncriteria = study.Inclusioncriteria;
                oldStudy.Isdraft = study.Isdraft;
                oldStudy.Pay = study.Pay;
                oldStudy.Preparation = study.Preparation;
                oldStudy.Location = study.Location;
                _context.Study.Update(oldStudy);
                _context.SaveChanges();
            }

            Inclusioncriteria oldInc = _context.Inclusioncriteria.FirstOrDefault(inc => inc.IdStudy == study.IdStudy);
            if (oldInc != null)
            {
                oldInc.Male = inclusioncriteria.Male;
                oldInc.Female = inclusioncriteria.Female;
                oldInc.English = inclusioncriteria.English;
                oldInc.MinAge = inclusioncriteria.MinAge;
                oldInc.MaxAge = inclusioncriteria.MaxAge;
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

        public List<Participant> getParticipantsDB(int studyID)
        {
            List<Participant> participants = new List<Participant>();

            List<int> PartIDs = _context.Studyparticipant.Where(x => x.IdStudy == studyID).Select(partID => partID.IdParticipant).ToList();

            foreach (var id in PartIDs)
            {
                participants.Add(_context.Participant.FirstOrDefault(part => part.IdParticipant == id));

            }
            return participants;

        }

        public ManageParticipantStatus AddParticipantToStudyDB(int partID, int studyID)
        {
            ManageParticipantStatus manageParticipantStatus = new ManageParticipantStatus();
            Studyparticipant studpart = _context.Studyparticipant.FirstOrDefault(x => x.IdStudy == studyID && x.IdParticipant == partID);
            if(studpart == null)
            {
                Participant participant = _context.Participant.FirstOrDefault(part => part.IdParticipant == partID);
                if (participant != null)
                {
                    //Participant exists, but is not enrolled.
                    studpart = new Studyparticipant();
                    studpart.IdParticipant = partID;
                    studpart.IdStudy = studyID;
                    _context.Studyparticipant.Add(studpart);
                    _context.SaveChanges();
                    manageParticipantStatus.success = true;
                }
                else
                {
                    //Participant with this ID does not exists. 
                    manageParticipantStatus.success = false;
                    manageParticipantStatus.errormessage = "Participant with this ID does not exist in the system";
                }


            }
            else
            {
                //Participant allready enrolled in study
                manageParticipantStatus.success = false;
                manageParticipantStatus.errormessage = "Participant is all ready enrolled in study";
            }
            return manageParticipantStatus;
        }

        public ManageParticipantStatus RemoveParticipantFromStudyDB(int partID, int studyID)
        {
            ManageParticipantStatus manageParticipantStatus = new ManageParticipantStatus();
            Studyparticipant studPart = _context.Studyparticipant.FirstOrDefault(x => x.IdParticipant == partID && x.IdStudy == studyID);
            if (studPart != null)
            {
                //Participant is enrolled in study
                _context.Studyparticipant.Remove(studPart);
                _context.SaveChanges();
                manageParticipantStatus.success = true;
            }
            else
            {
                //Participant is not enrolled in study
                manageParticipantStatus.errormessage = "Participant is not enrolled in study";
                manageParticipantStatus.success = false;
            }
            return manageParticipantStatus;

        }

        public List<Participant> getListParticipants(int studyid)
        {
            List<Participant> listp = new List<Participant>();
            List<int> particpantids = _context.Studyparticipant.Where(x => x.IdStudy == studyid).ToList()
                .Select(partid => partid.IdParticipant).ToList();
            foreach (var id in particpantids)
            {
               listp.Add(_context.Participant.FirstOrDefault(part => part.IdParticipant ==id));
            }
            return listp;
        }
    }
}