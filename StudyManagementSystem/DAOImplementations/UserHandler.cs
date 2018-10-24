
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachelorBackEnd
{
    public class UserHandler : IUserHandler
    {
        public Participant getParticipant(int id)
        {
            using (bachelordbContext DBmodel = new bachelordbContext())
            {
                Participant participant = DBmodel.Participant.FirstOrDefault(part => part.IdParticipant == id);
                return participant;
            }
        }

        public Researcher getResearcher(int id)
        {
            using (bachelordbContext DBmodel = new bachelordbContext())
            {
                Researcher researcher = DBmodel.Researcher.FirstOrDefault(res => res.IdResearcher == id);
                return researcher;
            }
        }
    }
}
