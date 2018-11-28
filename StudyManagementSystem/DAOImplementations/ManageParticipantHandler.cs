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
                manageParticipantStatus.errormessage = "Participant is already enrolled instudy.";
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

        public List<Participant> GetAllEligibalParticipants(Inclusioncriteria criteria, int studyId)
        {
           
            List<Participant> participants = new List<Participant>();
            List<int> PartIDs;
            if (_context.Study != null && _context.Inclusioncriteria != null)
            {
                if((criteria.Male == false && criteria.Female == false))
                {
                    // does nothing since there are not participants with no gender.
                }
                else
                {
                    if (criteria.English)
                    {
                        //Sorterings-Algorithm!
                        PartIDs = _context.Participant.Where(part =>
                                //Sorts by age
                                GetAge(part.Age) > criteria.MinAge && GetAge(part.Age) < criteria.MaxAge &&
                                //Sorts by gender


                                (criteria.Male == part.Gender ||
                                criteria.Female != part.Gender) &&
                            //Sort by languge         
                            part.English == criteria.English).ToList().Select(partID => partID.IdParticipant).ToList();
                    }
                    else
                    { //Sorterings-Algorithm!
                        PartIDs = _context.Participant.Where(part =>
                                //Sorts by age
                                GetAge(part.Age) > criteria.MinAge && GetAge(part.Age) < criteria.MaxAge &&
                                //Sorts by gender
                                (criteria.Male == part.Gender ||
                                 criteria.Female != part.Gender)).ToList().Select(partID => partID.IdParticipant).ToList();

                    }



                    // Get all participant that are inrolled in this study
                    List<int> EnrolledPartIDs = _context.Studyparticipant.Where(x => x.IdStudy == studyId)
                        .Select(partID => partID.IdParticipant).ToList();
                    foreach (var id in EnrolledPartIDs)
                    {
                        try
                        {
                            PartIDs.Remove(id);
                        }
                        catch (Exception e)
                        {

                            throw;
                        }
                    }

                    //List of participants to return

                    foreach (var id in PartIDs)
                    {
                        participants.Add(_context.Participant.FirstOrDefault(parts => parts.IdParticipant == id));

                    }
                }
               

                
            }
            if (participants == null)
                participants = new List<Participant>();
            return participants;
        }
        public static int GetAge(DateTime birthday)
        {
            DateTime now = DateTime.Today;
            int age = now.Year - birthday.Year;
            if (now < birthday.AddYears(age)) age--;

            return age;
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
