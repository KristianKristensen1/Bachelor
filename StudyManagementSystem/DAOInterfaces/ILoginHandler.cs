using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachelorBackEnd
{
    public interface ILoginHandler
    {
        LoginHandler LoginParticipantDB(string email, string password);

        LoginHandler LoginResearcherDB(string email, string password);

    }
}
