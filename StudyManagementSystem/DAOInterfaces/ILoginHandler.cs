using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachelorBackEnd
{
    public interface ILoginHandler
    {
        DbStatus LoginParticipantDB(string email, string password);

        DbStatus LoginResearcherDB(string email, string password);

    }
}
