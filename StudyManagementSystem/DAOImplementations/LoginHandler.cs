using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachelorBackEnd
{
    class LoginHandler : BaseEntity, ILoginHandler
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
                        ParticipantHandler.OperationStatus.IsSuccess = true;
                        ParticipantHandler.OperationStatus.participant = participant;
                    }
                    else
                    {
                        //Wrong password
                        ParticipantHandler.OperationStatus.ErrorMessage = "Wrong password";
                    }
                }
                else
                {
                    //No participant with this email exists in database
                    ParticipantHandler.OperationStatus.ErrorMessage = "No participant with this email exists";
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
                        ResearcherHandler.OperationStatus.IsSuccess = true;
                        ResearcherHandler.OperationStatus.researcher = researcher;
                    }
                    else
                    {
                        //Wrong password
                        ResearcherHandler.OperationStatus.ErrorMessage = "Wrong password";
                    }
                }
                else
                {
                    //No researcher with this email exists in database
                    ResearcherHandler.OperationStatus.ErrorMessage = "No researcher with this email exists";
                }
            }
            return ResearcherHandler;
        }
    }
}
