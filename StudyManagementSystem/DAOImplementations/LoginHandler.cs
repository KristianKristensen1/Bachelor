using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachelorBackEnd
{
    public class LoginHandler : LoginEntity, ILoginHandler
    {
        public LoginHandler LoginParticipantDB(string email, string password)
        {
            var ParticipantHandler = new LoginHandler();
            using (bachelordbContext DBmodel = new bachelordbContext())
            {
                Participant participant = DBmodel.Participant.FirstOrDefault(part => part.Email == email);
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
            }
            return ParticipantHandler;
        }

        public LoginHandler LoginResearcherDB(string email, string password)
        {
            var ResearcherHandler = new LoginHandler();
            using (bachelordbContext DBmodel = new bachelordbContext())
            {
                Researcher researcher = DBmodel.Researcher.FirstOrDefault(part => part.Email == email);
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
            }
            return ResearcherHandler;
        }
    }
}
