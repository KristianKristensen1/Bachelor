using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace BachelorBackEnd
{
    public class LoginHandler : ILoginHandler
    {
        private bachelordbContext _context;
        public LoginHandler(bachelordbContext context)
        {
            _context = context;
        }

        public DbStatus LoginParticipantDB(string email, string password)
        {
            DbStatus status = new DbStatus();
            Participant participant = _context.Participant.FirstOrDefault(part => part.Email == email);
            if (participant != null)
                {
                    if (participant.Password == password)
                    {
                    //Successfull login
                    status.success = true;
                    status.participant = participant;
                    }
                    else
                    {
                    //Wrong password
                    status.errormessage = "Wrong password";
                    }
                }
                else
                {
                //No participant with this email exists in database
                status.errormessage = "No participant with this email exists";
                }
         
            return status;
        }

        public DbStatus LoginResearcherDB(string email, string password)
        {
            DbStatus status = new DbStatus();
            Researcher researcher = _context.Researcher.FirstOrDefault(res => res.Email == email);
            if (researcher != null)
                {
                    if (researcher.Password == password)
                    {
                    //Successfull login
                    status.success = true;
                    status.researcher = researcher;
                    }
                    else
                    {
                    //Wrong password
                    status.success = false;
                    status.errormessage = "Wrong password";
                    }
                }
                else
                {
                //No researcher with this email exists in database
                status.errormessage = "No researcher with this email exists";
                }
            
            return status;
        }
    }
}
