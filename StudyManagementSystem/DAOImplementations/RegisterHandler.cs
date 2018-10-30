using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BachelorBackEnd
{
    public class RegisterHandler : IRegisterHandler
    {
        private bachelordbContext _context;


        public RegisterHandler(bachelordbContext context)
        {
            _context = context;
        }

        public bool RegisterParticipantDB(Participant participant)
        {
            if (_context.Participant.Any(part => part.Email == participant.Email))
            {
                //Email allready exists
                return false;
            }
            else
            {
                _context.Participant.Add(participant);
                _context.SaveChanges();
                return true;
            }
        }


        public bool RegisterResearcherDB(Researcher researcher)
        {
            if (_context.Researcher.Any(res => res.Email == researcher.Email))
            {
                //Email allready exists
                return false;
            }
            else
            {
                _context.Researcher.Add(researcher);
                _context.SaveChanges();
                return true;

            }
        }

        public void VerifyResearcherDB(Researcher researcher)
        {
            throw new NotImplementedException();
        }
    }
}
