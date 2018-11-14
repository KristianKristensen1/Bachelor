using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachelorBackEnd
{
    public class ManageProfileHandler : IManageProfileHandler
    {
        bachelordbContext _context;
        public ManageProfileHandler(bachelordbContext context)
        {
            _context = context;
        }
        public DbStatus ChangePasswordResearcherDB(Researcher researcher, string oldPassword)
        {
            Researcher oldResearcher = _context.Researcher.FirstOrDefault(res => res.IdResearcher == researcher.IdResearcher);
            DbStatus status = new DbStatus();

            if (oldResearcher.Password == oldPassword)
            {
                oldResearcher.Password = researcher.Password;
                _context.Researcher.Update(oldResearcher);
                _context.SaveChanges();
                status.success = true;
            }
            else
            {
                status.success = false;
                status.errormessage = "The old password was incorrect. Please try again";
            }
            return status;
        }

        public DbStatus ChangePasswordParticipantDB(Participant participant, string oldPassword)
        {
            Participant oldParticipant = _context.Participant.FirstOrDefault(part => part.IdParticipant == participant.IdParticipant);
            DbStatus status = new DbStatus();

            if (oldParticipant.Password == oldPassword)
            {
                oldParticipant.Password = participant.Password;
                _context.Participant.Update(oldParticipant);
                _context.SaveChanges();
                status.success = true;
            }
            else
            {
                status.success = false;
                status.errormessage = "The old password was incorrect. Please try again";
            }
            return status;
        }

        public bool ChangeProfileResearcherDB(Researcher researcher)
        {
            try
            {
                Researcher oldResearcher = _context.Researcher.FirstOrDefault(res => res.IdResearcher == researcher.IdResearcher);
                oldResearcher.Email = researcher.Email;
                oldResearcher.FirstName = researcher.FirstName;
                oldResearcher.LastName = researcher.LastName;
                _context.Researcher.Update(oldResearcher);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        public bool ChangeProfileParticipantDB(Participant participant)
        {
            try
            {
                Participant oldParticipant = _context.Participant.FirstOrDefault(part => part.IdParticipant == participant.IdParticipant);
                oldParticipant.Email = participant.Email;

                _context.Participant.Update(oldParticipant);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }
    }
}
