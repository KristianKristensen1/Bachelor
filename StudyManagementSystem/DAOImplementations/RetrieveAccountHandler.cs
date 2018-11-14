using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BachelorBackEnd;
using StudyManagementSystem.DAOInterfaces;

namespace StudyManagementSystem.DAOImplementations
{
    public class RetrieveAccountHandler: IRetrieveAccountHandler
    {
        private bachelordbContext _context;
        public RetrieveAccountHandler(bachelordbContext context)
        {
            _context = context;
        }

        public DbStatus VerifyResearcherDB(string email)
        {
            DbStatus status = new DbStatus();
            Researcher researcher = _context.Researcher.FirstOrDefault(part => part.Email == email);
            if (researcher != null)
            {
                //Successfull Retrive
                status.success = true;
                status.researcher = researcher;
            }
            else
            {
                //No participant with this email exists in database
                status.errormessage = "No researcher with this email exists";
            }
            return status;
        }

        public DbStatus VerifyParticipantDB(string email)
        {
            DbStatus status = new DbStatus();
            Participant participant = _context.Participant.FirstOrDefault(part => part.Email == email);
            if (participant != null)
            {
                //Successfull Retrive
                status.success = true;
                status.participant = participant;              
            }
            else
            {
                //No participant with this email exists in database
                status.errormessage = "No participant with this email exists";
            }
            return status;
        }
    }
}
