
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachelorBackEnd
{
    public interface IUserHandler
    {
        Participant getParticipant(int id);

        Researcher getResearcher(int id);
    }
}
