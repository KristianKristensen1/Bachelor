using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BachelorBackEnd;
using StudyManagementSystem.DAOImplementations;

namespace StudyManagementSystem.DAOInterfaces
{
    public interface IRetrieveAccountHandler
    {
        DbStatus VerifyResearcherDB(string email);

        DbStatus VerifyParticipantDB(string email);

    }
}
