using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachelorBackEnd
{
    public class RegisterHandler : IRegisterHandler
    {
        public void RegisterParticipantDB(Participant participant)
        {
            using (bachelordbContext DBmodel = new bachelordbContext())
            {
                if(DBmodel.Participant.Any(part => part.Email == participant.Email))
                {
                    //Email allready exists
                }
                DBmodel.Participant.Add(participant);
                DBmodel.SaveChanges();
            }
        }

        public void RegisterResearcherDB(Researcher researcher)
        {
            using (bachelordbContext DBmodel = new bachelordbContext())
            {
                if (DBmodel.Participant.Any(part => part.Email == researcher.Email))
                {
                    //Email allready exists
                }
                DBmodel.Researcher.Add(researcher);
                DBmodel.SaveChanges();
            }
        }

        public void VerifyResearcherDB(Researcher researcher)
        {
            throw new NotImplementedException();
        }
    }
}
