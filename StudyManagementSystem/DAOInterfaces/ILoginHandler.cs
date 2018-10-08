using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachelorBackEnd
{
    interface ILoginHandler
    {
        void LoginParticipantDB(string email, string password);

        void LoginResearcherDB(string email, string password);

    }
}
