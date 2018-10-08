using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachelorBackEnd
{
    class LoginHandler : ILoginHandler
    {
        public void LoginParticipantDB(string email, string password)
        {
            using (bachelordbContext DBmodel = new bachelordbContext())
            {
                Participant participant = DBmodel.Participant.FirstOrDefault(part => part.Email == email);
                if(participant != null)
                {
                    if(participant.Password == password)
                    {
                        //Successfull login
                    }
                    else
                    {
                        //Wrong password
                    }
                }
                else
                {
                    //No participant with this email exists in database
                }
            }
        }

        public void LoginResearcherDB(string email, string password)
        {
            using (bachelordbContext DBmodel = new bachelordbContext())
            {
                Researcher researcher = DBmodel.Researcher.FirstOrDefault(part => part.Email == email);
                if (researcher != null)
                {
                    if (researcher.Password == password)
                    {
                        //Successfull login
                    }
                    else
                    {
                        //Wrong password
                    }
                }
                else
                {
                    //No researcher with this email exists in database
                }
            }
        }
    }
}
