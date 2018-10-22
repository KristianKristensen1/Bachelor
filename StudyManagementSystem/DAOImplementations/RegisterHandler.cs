using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachelorBackEnd
{
    public class RegisterHandler : IRegisterHandler
    {
        public bool RegisterParticipantDB(Participant participant)
        {
            using (bachelordbContext DBmodel = new bachelordbContext())
            {
                if(DBmodel.Participant.Any(part => part.Email == participant.Email))
                {
                    //Email allready exists
                    return false;
                }
                else
                {
                    DBmodel.Participant.Add(participant);
                    DBmodel.SaveChanges();
                    return true;
                }
            }
        }

        public bool RegisterResearcherDB(Researcher researcher)
        {
            using (bachelordbContext DBmodel = new bachelordbContext())
            {
                if (DBmodel.Participant.Any(res => res.Email == researcher.Email))
                {
                    //Email allready exists
                    return false;
                }
                else
                {
                    DBmodel.Researcher.Add(researcher);
                    DBmodel.SaveChanges();
                    return true;
                }
            }
        }

        public void VerifyResearcherDB(Researcher researcher)
        {
            throw new NotImplementedException();
        }
    }
}
