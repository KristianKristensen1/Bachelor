
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
        public Participant GetParticipantDB(int id)
        {
            Participant participant = _context.Participant.FirstOrDefault(part => part.IdParticipant == id);
            return participant;
        }

        public Researcher GetResearcherDB(int id)
        {
            Researcher researcher = _context.Researcher.FirstOrDefault(res => res.IdResearcher == id);
            return researcher;
        }

        public List<Researcher> GetUnverifiedResearchersDB()
        {
            List<Researcher> researchers = _context.Researcher.Where(res => res.Isverified == false).ToList();
            return researchers;
        }

        public List<Researcher> GetAllVerifiedResearchersDB()
        {
            List<Researcher> researchers = _context.Researcher.Where(res => res.Isverified == true).ToList();
            return researchers;
        }

        public DbStatus VerifyResearcherDB(int resID)
        {
            Researcher researcher = _context.Researcher.FirstOrDefault(res => res.IdResearcher == resID);
            DbStatus verifyResearcherStatus = new DbStatus();
            if (researcher != null)
            {
                if (researcher.Isverified == false)
                {
                    //Verify researcher
                    researcher.Isverified = true;
                    _context.Update(researcher);
                    _context.SaveChanges();
                    verifyResearcherStatus.success = true;
                }
                else
                {
                    //Researcher was all ready verified
                    verifyResearcherStatus.success = false;
                    verifyResearcherStatus.errormessage = "Researcher is all ready verified";
                }
            }
            else
            {
                //Researcher does not exists
                verifyResearcherStatus.success = false;
                verifyResearcherStatus.errormessage = "Researcher with this ID does not exists";
            }
            return verifyResearcherStatus;
        }

        public DbStatus UnverifyResearcherDB(int resID)
        {
            Researcher researcher = _context.Researcher.FirstOrDefault(res => res.IdResearcher == resID);
            DbStatus UnverifyResearcherStatus = new DbStatus();
            if (researcher != null)
            {
                if (researcher.Isverified == true)
                {
                    //Unverify researcher
                    researcher.Isverified = false;
                    _context.Update(researcher);
                    _context.SaveChanges();
                    UnverifyResearcherStatus.success = true;
                }
                else
                {
                    //Researcher was not verified
                    UnverifyResearcherStatus.success = false;
                    UnverifyResearcherStatus.errormessage = "Researcher is not verified";
                }
            }
            else
            {
                //Researcher does not exists
                UnverifyResearcherStatus.success = false;
                UnverifyResearcherStatus.errormessage = "Researcher with this ID does not exists";
            }
            return UnverifyResearcherStatus;
        }
    }

}
