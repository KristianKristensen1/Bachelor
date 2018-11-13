using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachelorBackEnd
{
    public class ManageParticipantHandler : IManageParticipantHandler
    {
        bachelordbContext _context;
        public ManageParticipantHandler(bachelordbContext context)
        {
            _context = context;
        }

        public DbStatus AddParticipantToStudyDB(int partID, int studyID)
        {
            DbStatus manageParticipantStatus = new DbStatus();
            Studyparticipant studpart = _context.Studyparticipant.FirstOrDefault(x => x.IdStudy == studyID && x.IdParticipant == partID);
            if (studpart == null)
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

        public DbStatus RemoveParticipantFromStudyDB(int partID, int studyID)
        {
            DbStatus manageParticipantStatus = new DbStatus();
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

        public List<Participant> GetParticipantsInStudyDB(int studyID)
        {
            List<Participant> participants = new List<Participant>();

            List<int> PartIDs = _context.Studyparticipant.Where(x => x.IdStudy == studyID).Select(partID => partID.IdParticipant).ToList();

            foreach (var id in PartIDs)
            {
                participants.Add(_context.Participant.FirstOrDefault(part => part.IdParticipant == id));

            }
            return participants;
        }

        public DbStatus GetParticipantEmailDB(int partID)
        {
            DbStatus manageParticipantStatus = new DbStatus();
            Participant participant = _context.Participant.FirstOrDefault(part => part.IdParticipant == partID);

            if (participant != null)
            {
                manageParticipantStatus.success = true;
                manageParticipantStatus.participantEmail = participant.Email;
            }
            else
            {
                manageParticipantStatus.success = false;
                manageParticipantStatus.errormessage = "No participant with this ID exists";
            }
            return manageParticipantStatus;
        }
    }
}
