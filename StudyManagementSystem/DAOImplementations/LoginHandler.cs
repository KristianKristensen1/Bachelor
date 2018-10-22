using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudyManagementSystem.Models;


namespace BachelorBackEnd
{
    public class LoginHandler : LoginEntity, ILoginHandler
    {
        private bachelordbContext _context;

        public LoginHandler()
        {

        }

        public LoginHandler(bachelordbContext context)
        {
            _context = context;
        }

        public LoginHandler LoginParticipantDB(string email, string password)
        {
            var ParticipantHandler = new LoginHandler();
            Participant participant = _context.Participant.FirstOrDefault(part => part.Email == email);
            if (participant != null)
                {
                    if (participant.Password == password)
                    {
                        //Successfull login
                        ParticipantHandler.LoginStatus.IsSuccess = true;
                        ParticipantHandler.LoginStatus.participant = participant;
                    }
                    else
                    {
                        //Wrong password
                        ParticipantHandler.LoginStatus.ErrorMessage = "Wrong password";
                    }
                }
                else
                {
                    //No participant with this email exists in database
                    ParticipantHandler.LoginStatus.ErrorMessage = "No participant with this email exists";
                }
         
            return ParticipantHandler;
        }

        public LoginHandler LoginResearcherDB(string email, string password)
        {
            var ResearcherHandler = new LoginHandler();
            Researcher researcher = _context.Researcher.FirstOrDefault(part => part.Email == email);
            if (researcher != null)
                {
                    if (researcher.Password == password)
                    {
                        //Successfull login
                        ResearcherHandler.LoginStatus.IsSuccess = true;
                        ResearcherHandler.LoginStatus.researcher = researcher;
                    }
                    else
                    {
                        //Wrong password
                        ResearcherHandler.LoginStatus.ErrorMessage = "Wrong password";
                    }
                }
                else
                {
                    //No researcher with this email exists in database
                    ResearcherHandler.LoginStatus.ErrorMessage = "No researcher with this email exists";
                }
            
            return ResearcherHandler;
        }
    }
}
