using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BachelorBackEnd;
using StudyManagementSystem.DAOInterfaces;

namespace StudyManagementSystem.DAOImplementations
{
    public class RetrieveAccountHandler: LoginEntity, IRetrieveAccountHandler
    {
        private bachelordbContext _context;

        public RetrieveAccountHandler()
        {

        }
        public RetrieveAccountHandler(bachelordbContext context)
        {
            _context = context;
        }
        public RetrieveAccountHandler VerifyResearcherDB(string email)
        {
            var retrieveAccountHandler = new RetrieveAccountHandler();
            Researcher researcher = _context.Researcher.FirstOrDefault(part => part.Email == email);
            if (researcher != null)
            {

                //Successfull Retrive
                retrieveAccountHandler.LoginStatus.IsSuccess = true;
                retrieveAccountHandler.LoginStatus.researcher = researcher;

            }
            else
            {
                //No participant with this email exists in database
                retrieveAccountHandler.LoginStatus.ErrorMessage = "No researcher with this email exists";
            }

            return retrieveAccountHandler;
        }

        public RetrieveAccountHandler VerifyParticipantDB(string email)
        {
            var retrieveAccountHandler = new RetrieveAccountHandler();
            Participant participant = _context.Participant.FirstOrDefault(part => part.Email == email);
            if (participant != null)
            {
                
                    //Successfull Retrive
                    retrieveAccountHandler.LoginStatus.IsSuccess = true;
                    retrieveAccountHandler.LoginStatus.participant = participant;
              
            }
            else
            {
                //No participant with this email exists in database
                retrieveAccountHandler.LoginStatus.ErrorMessage = "No participant with this email exists";
            }

            return retrieveAccountHandler;
        }
    }
}
