using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudyManagementSystem.Models;

namespace BachelorBackEnd
{
    public class RegisterHandler : IRegisterHandler
    {
        private bachelordbContext _context;
        public RegisterHandler()
        {

        }

        public RegisterHandler(bachelordbContext context)
        {
            _context = context;
        }
        public bool RegisterParticipantDB(Participant participant, out string ErrorMessage)
        {
           //if(_context==null)
           //    _context = new bachelordbContext();

                if(_context.Participant.Any(part => part.Email == participant.Email))
                {
                    //Email allready exists
                    ErrorMessage = "Email allready exists in database";
                    return false;
                }
                else
                {
                    _context.Participant.Add(participant);
                    _context.SaveChanges();
                    ErrorMessage = "No problems";
                    return true;
                }
            
        }

        public bool RegisterResearcherDB(Researcher researcher, out string ErrorMessage)
        {
           
                if (_context.Researcher.Any(res => res.Email == researcher.Email))
                {
                    //Email allready exists
                    ErrorMessage = "Email allready exists in database";
                    return false;
                }
                else
                {
                    _context.Researcher.Add(researcher);
                    _context.SaveChanges();
                    ErrorMessage = "No problems";
                    return true;
                }
            
        }

        public void VerifyResearcherDB(Researcher researcher)
        {
            throw new NotImplementedException();
        }
    }
}
