using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachelorBackEnd
{
    public interface IRegisterHandler
    {
        void RegisterParticipantDB(Participant participant);

        void RegisterResearcherDB(Researcher researcher);

        void VerifyResearcherDB(Researcher researcher);
    }
}
