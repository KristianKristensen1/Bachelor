
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachelorBackEnd
{
    public interface IUserHandler
    {
        Participant GetParticipantDB(int id);

        Researcher GetResearcherDB(int id);

        List<Researcher> GetUnverifiedResearchersDB();

        List<Researcher> GetAllVerifiedResearchersDB();

        DbStatus VerifyResearcherDB(int resID);

        DbStatus UnverifyResearcherDB(int resID);
    }
}
