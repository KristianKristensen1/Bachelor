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
                //The old password matches, and will be changed to new one
                oldResearcher.Password = researcher.Password;
                _context.Researcher.Update(oldResearcher);
                _context.SaveChanges();
                status.success = true;
            }
            else
            {
                //Old password did not match. Password will not be changed
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
                //Old password matches and password will be changed.
                oldParticipant.Password = participant.Password;
                _context.Participant.Update(oldParticipant);
                _context.SaveChanges();
                status.success = true;
            }
            else
            {
                //Old password did not match. Password will not be changed. 
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
                oldParticipant.English = participant.English;

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

        public void DeleteAccountParticipantDB(int partID)
        {
            Participant dbparticipant = _context.Participant.FirstOrDefault(part => part.IdParticipant == partID);
            _context.Participant.Remove(dbparticipant);
            _context.SaveChanges();
        }

        public void DeleteAcoountResearcherDB(Researcher researcher)
        {
            Researcher dbResearcher = _context.Researcher.FirstOrDefault(res => res.IdResearcher == researcher.IdResearcher);
            _context.Researcher.Remove(dbResearcher);
            _context.SaveChanges();
        }
    }
}
