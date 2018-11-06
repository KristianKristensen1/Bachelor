
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachelorBackEnd
{
    public class UserHandler : IUserHandler
    {
        bachelordbContext _context;
        public UserHandler(bachelordbContext context)
        {
            _context = context;
        }
        public Participant getParticipant(int id)
        {

            Participant participant = _context.Participant.FirstOrDefault(part => part.IdParticipant == id);
            return participant;

        }

        public Researcher getResearcher(int id)
        {

            Researcher researcher = _context.Researcher.FirstOrDefault(res => res.IdResearcher == id);
            return researcher;

        }

        public List<Researcher> getUnverifiedResearchersDB()
        {
            List<Researcher> researchers = _context.Researcher.Where(res => res.Isverified == false).ToList();
            return researchers;
        }


        public List<Researcher> getAllResearchersDB()
        {
            List<Researcher> researchers = _context.Researcher.Where(res => res.Isverified == true).ToList();
            return researchers;
        }

        public ManageParticipantStatus VerifyResearcher(int resID)
        {
            Researcher researcher = _context.Researcher.FirstOrDefault(res => res.IdResearcher == resID);
            ManageParticipantStatus manageParticipantStatus = new ManageParticipantStatus();
            if (researcher != null)
            {
                if (researcher.Isverified == false)
                {
                    researcher.Isverified = true;
                    _context.Update(researcher);
                    _context.SaveChanges();
                    manageParticipantStatus.success = true;
                }
                else
                {
                    manageParticipantStatus.success = false;
                    manageParticipantStatus.errormessage = "Researcher is all ready verified";
                    //Researcher was all ready verified
                }
            }
            else
            {
                //Researcher does not exists
                manageParticipantStatus.success = false;
                manageParticipantStatus.errormessage = "Researcher with this ID does not exists";
            }
            return manageParticipantStatus;

        }

        public ManageParticipantStatus UnverifyResearcher(int resID)
        {
            Researcher researcher = _context.Researcher.FirstOrDefault(res => res.IdResearcher == resID);
            ManageParticipantStatus manageParticipantStatus = new ManageParticipantStatus();
            if (researcher != null)
            {
                if (researcher.Isverified == true)
                {
                    researcher.Isverified = false;
                    _context.Update(researcher);
                    _context.SaveChanges();
                    manageParticipantStatus.success = true;
                }
                else
                {
                    //Researcher was not verified
                    manageParticipantStatus.success = false;
                    manageParticipantStatus.errormessage = "Researcher is not verified";
                }
            }
            else
            {
                //Researcher does not exists
                manageParticipantStatus.success = false;
                manageParticipantStatus.errormessage = "Researcher with this ID does not exists";
            }
            return manageParticipantStatus;
        }
    }
}
